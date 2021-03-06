// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: Protos/auction.proto
// </auto-generated>
#pragma warning disable 0414, 1591
#region Designer generated code

using grpc = global::Grpc.Core;

namespace Auction.Server {
  public static partial class AuctionBids
  {
    static readonly string __ServiceName = "AuctionBids.AuctionBids";

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static void __Helper_SerializeMessage(global::Google.Protobuf.IMessage message, grpc::SerializationContext context)
    {
      #if !GRPC_DISABLE_PROTOBUF_BUFFER_SERIALIZATION
      if (message is global::Google.Protobuf.IBufferMessage)
      {
        context.SetPayloadLength(message.CalculateSize());
        global::Google.Protobuf.MessageExtensions.WriteTo(message, context.GetBufferWriter());
        context.Complete();
        return;
      }
      #endif
      context.Complete(global::Google.Protobuf.MessageExtensions.ToByteArray(message));
    }

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static class __Helper_MessageCache<T>
    {
      public static readonly bool IsBufferMessage = global::System.Reflection.IntrospectionExtensions.GetTypeInfo(typeof(global::Google.Protobuf.IBufferMessage)).IsAssignableFrom(typeof(T));
    }

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static T __Helper_DeserializeMessage<T>(grpc::DeserializationContext context, global::Google.Protobuf.MessageParser<T> parser) where T : global::Google.Protobuf.IMessage<T>
    {
      #if !GRPC_DISABLE_PROTOBUF_BUFFER_SERIALIZATION
      if (__Helper_MessageCache<T>.IsBufferMessage)
      {
        return parser.ParseFrom(context.PayloadAsReadOnlySequence());
      }
      #endif
      return parser.ParseFrom(context.PayloadAsNewBuffer());
    }

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::Auction.Server.RegisterBidderRequest> __Marshaller_AuctionBids_RegisterBidderRequest = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::Auction.Server.RegisterBidderRequest.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::Auction.Server.UpdateItemStatusReply> __Marshaller_AuctionBids_UpdateItemStatusReply = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::Auction.Server.UpdateItemStatusReply.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::Auction.Server.RaiseBidRequest> __Marshaller_AuctionBids_RaiseBidRequest = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::Auction.Server.RaiseBidRequest.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::Auction.Server.RaiseBidReply> __Marshaller_AuctionBids_RaiseBidReply = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::Auction.Server.RaiseBidReply.Parser));

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::Auction.Server.RegisterBidderRequest, global::Auction.Server.UpdateItemStatusReply> __Method_RegisterBidder = new grpc::Method<global::Auction.Server.RegisterBidderRequest, global::Auction.Server.UpdateItemStatusReply>(
        grpc::MethodType.ServerStreaming,
        __ServiceName,
        "RegisterBidder",
        __Marshaller_AuctionBids_RegisterBidderRequest,
        __Marshaller_AuctionBids_UpdateItemStatusReply);

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::Auction.Server.RaiseBidRequest, global::Auction.Server.RaiseBidReply> __Method_RaiseBid = new grpc::Method<global::Auction.Server.RaiseBidRequest, global::Auction.Server.RaiseBidReply>(
        grpc::MethodType.Unary,
        __ServiceName,
        "RaiseBid",
        __Marshaller_AuctionBids_RaiseBidRequest,
        __Marshaller_AuctionBids_RaiseBidReply);

    /// <summary>Service descriptor</summary>
    public static global::Google.Protobuf.Reflection.ServiceDescriptor Descriptor
    {
      get { return global::Auction.Server.AuctionReflection.Descriptor.Services[0]; }
    }

    /// <summary>Client for AuctionBids</summary>
    public partial class AuctionBidsClient : grpc::ClientBase<AuctionBidsClient>
    {
      /// <summary>Creates a new client for AuctionBids</summary>
      /// <param name="channel">The channel to use to make remote calls.</param>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public AuctionBidsClient(grpc::ChannelBase channel) : base(channel)
      {
      }
      /// <summary>Creates a new client for AuctionBids that uses a custom <c>CallInvoker</c>.</summary>
      /// <param name="callInvoker">The callInvoker to use to make remote calls.</param>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public AuctionBidsClient(grpc::CallInvoker callInvoker) : base(callInvoker)
      {
      }
      /// <summary>Protected parameterless constructor to allow creation of test doubles.</summary>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      protected AuctionBidsClient() : base()
      {
      }
      /// <summary>Protected constructor to allow creation of configured clients.</summary>
      /// <param name="configuration">The client configuration.</param>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      protected AuctionBidsClient(ClientBaseConfiguration configuration) : base(configuration)
      {
      }

      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncServerStreamingCall<global::Auction.Server.UpdateItemStatusReply> RegisterBidder(global::Auction.Server.RegisterBidderRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return RegisterBidder(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncServerStreamingCall<global::Auction.Server.UpdateItemStatusReply> RegisterBidder(global::Auction.Server.RegisterBidderRequest request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncServerStreamingCall(__Method_RegisterBidder, null, options, request);
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::Auction.Server.RaiseBidReply RaiseBid(global::Auction.Server.RaiseBidRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return RaiseBid(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::Auction.Server.RaiseBidReply RaiseBid(global::Auction.Server.RaiseBidRequest request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_RaiseBid, null, options, request);
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::Auction.Server.RaiseBidReply> RaiseBidAsync(global::Auction.Server.RaiseBidRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return RaiseBidAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::Auction.Server.RaiseBidReply> RaiseBidAsync(global::Auction.Server.RaiseBidRequest request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_RaiseBid, null, options, request);
      }
      /// <summary>Creates a new instance of client from given <c>ClientBaseConfiguration</c>.</summary>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      protected override AuctionBidsClient NewInstance(ClientBaseConfiguration configuration)
      {
        return new AuctionBidsClient(configuration);
      }
    }

  }
}
#endregion
