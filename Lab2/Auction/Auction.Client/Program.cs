using System;
using System.Threading;
using System.Threading.Tasks;

using Grpc.Net.Client;

using Auction.Server;

namespace Auction.Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new AuctionBids.AuctionBidsClient(channel);

            Console.WriteLine("To register in auction type your name:");
            var bidderName = Console.ReadLine();

            using var call = client.RegisterBidder(new RegisterBidderRequest { BidderName = bidderName });

            while (await call.ResponseStream.MoveNext(CancellationToken.None))
            {
                var response = call.ResponseStream.Current;

                Console.WriteLine(response.UserMessage);

                if (response.Status == 1)
                {
                    if (ConsoleHelper.ReadKeyWithTimeout() == ConsoleKey.Enter)
                    {
                        var raiseBidResponse = await client.RaiseBidAsync(new RaiseBidRequest { BidderName = bidderName });

                        Console.WriteLine(raiseBidResponse.UserMessage);
                    }
                }
            }
        }
    }
}