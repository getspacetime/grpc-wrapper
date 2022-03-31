using FluentAssertions;
using System.Collections.Generic;
using System.IO;
using Xunit;
using Xunit.Abstractions;

namespace Spacetime.gRPC.Wrapper.Tests
{
    public class GRPCurlTests
    {
        private readonly ITestOutputHelper output;
        string ImportPath = Path.Combine(Directory.GetCurrentDirectory(), "protos");
        const string SingleProtoFile = "greet.proto";
        const string MultiMethodProtoFile = "multipleMethods.proto";
        public GRPCurlTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void ListServices()
        {
            var cmd = new GRPCurl();
            var expected = new GrpcExplorerResult { Items = new List<GrpcEntity> { new GrpcEntity { Name = "greet.Greeter" } } };
            var actual = cmd.ListServices(ImportPath, SingleProtoFile);
            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void ListMethods()
        {
            var cmd = new GRPCurl();
            var expected = new GrpcExplorerResult { Items = new List<GrpcEntity> { new GrpcEntity { Name = "greet.Greeter.SayHello" } } };

            var actual = cmd.ListMethods(ImportPath, SingleProtoFile, "greet.Greeter");
            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void ListMethods_ShouldReturnMultipleMethods()
        {
            var cmd = new GRPCurl();
            var expected = new GrpcExplorerResult { Items = new List<GrpcEntity> { new GrpcEntity { Name = "greet.Greeter.SayHello" },
             new GrpcEntity { Name = "greet.Greeter.SayHello2" },
             new GrpcEntity { Name = "greet.Greeter.SayHello3" }} };

            var actual = cmd.ListMethods(ImportPath, MultiMethodProtoFile, "greet.Greeter");
            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void Invoke()
        {
            var cmd = new GRPCurl();
            var actual = cmd.Invoke(ImportPath, SingleProtoFile, "foo");
            actual.Error.Should().BeTrue();
        }
    }
}