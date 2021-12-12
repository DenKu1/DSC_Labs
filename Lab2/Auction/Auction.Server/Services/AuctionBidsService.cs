using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Grpc.Core;

using Auction.Server.Models;

namespace Auction.Server.Services
{
    public class AuctionBidsService: AuctionBids.AuctionBidsBase
    {
        int currentBidder;

        readonly List<Bidder> bidders;
        readonly Item currentAuctionItem;
        readonly EventWaitHandle waitNextBidRound;

        public AuctionBidsService()
        {
            currentAuctionItem = new Item
                {
                    Name = "Apple",
                    Price = 25
                };
            bidders = new();
            currentBidder = 0;
            waitNextBidRound = new(true, EventResetMode.ManualReset);
        }

        public override async Task RegisterBidder(RegisterBidderRequest request, IServerStreamWriter<UpdateItemStatusReply> responseStream, ServerCallContext context)
        {
            var bidder = await RegisterNewBidder(request, responseStream);

            while (true)
            {
                var nextBidder = GetNextBidder();

                if (nextBidder == bidder)
                    await SendBidderTurnUpdate(bidder, responseStream);
                else
                    await SendOtherBidderTurnUpdate(nextBidder, responseStream);
            }
        }

        public override Task<RaiseBidReply> RaiseBid(RaiseBidRequest request, ServerCallContext context)
        {
            var bidder = bidders.First(bidder => bidder.Name == request.BidderName);

            if (bidder.CanRaiseBid == false)
                return Task.FromResult(new RaiseBidReply { UserMessage = "Error: Your time passed and you cannot raise the bid" });

            currentAuctionItem.Price *= 2;
            currentAuctionItem.WinningBidder = bidder.Name;

            return Task.FromResult(new RaiseBidReply { UserMessage = "Your bid played!\r\n" + GetCurrentAuctionItemInfo() });
        }

        Bidder GetNextBidder()
        {
            waitNextBidRound.WaitOne();

            return bidders[currentBidder];
        }

        async Task<Bidder> RegisterNewBidder(RegisterBidderRequest request, IServerStreamWriter<UpdateItemStatusReply> responseStream)
        {
            var reply = new UpdateItemStatusReply();

            var bidderName = request.BidderName;

            var bidder = new Bidder { Name = bidderName };

            bidders.Add(bidder);

            reply.UserMessage = $"Bidder {bidderName} successfully registered for auction item {currentAuctionItem.Name}.\r\n" +
                                $"There are also {bidders.Count - 1} other bidders on this lot.\r\n";

            await responseStream.WriteAsync(reply);

            return bidder;
        }

        async Task SendBidderTurnUpdate(Bidder bidder, IServerStreamWriter<UpdateItemStatusReply> responseStream)
        {
            var reply = new UpdateItemStatusReply
            {
                Status = 1,
                UserMessage = $"Your turn to place bid. You have 30 seconds.\r\nPress ENTER to double current bet.\r\n" + GetCurrentAuctionItemInfo()
            };

            await responseStream.WriteAsync(reply);

            waitNextBidRound.Reset();

            bidder.CanRaiseBid = true;
            Thread.Sleep(30_000);
            bidder.CanRaiseBid = false;

            MoveToNextBidder();

            waitNextBidRound.Set();
        }

        async Task SendOtherBidderTurnUpdate(Bidder nextBidder, IServerStreamWriter<UpdateItemStatusReply> responseStream)
        {
            var reply = new UpdateItemStatusReply
            {
                Status = 0,
                UserMessage = $"It is other bidder {nextBidder.Name} time to decide to place bid.\r\n" + GetCurrentAuctionItemInfo()
            };

            await responseStream.WriteAsync(reply);
        }

        void MoveToNextBidder()
        {
            currentBidder++;

            if (currentBidder == bidders.Count)
                currentBidder = 0;
        }

        string GetCurrentAuctionItemInfo()
        {
            return $"Item name: {currentAuctionItem.Name}\r\n" +
                   $"Item price: {currentAuctionItem.Price}\r\n" +
                   $"Item winning bidder: {currentAuctionItem.WinningBidder}\r\n";
        }
    }
}
