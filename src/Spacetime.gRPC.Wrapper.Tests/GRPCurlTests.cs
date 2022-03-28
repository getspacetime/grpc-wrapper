using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace Spacetime.gRPC.Wrapper.Tests
{
    public class GRPCurlTests
    {
        private readonly ITestOutputHelper output;
        const string ImportPath = "C:\\Users\\Cody\\source\\repos\\Spacetime.gRPC\\Spacetime.gRPC\\Protos";
        const string ProtoFile = "greet.proto";
        public GRPCurlTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void ListServices()
        {
            var cmd = new GRPCurl();
            var actual = cmd.ListServices(ImportPath, ProtoFile);
            actual.Should().BeEquivalentTo("found services: greet.Greeter");
            output.WriteLine(actual);
        }

        [Fact]
        public void ListMethods()
        {
            var cmd = new GRPCurl();
            var actual = cmd.ListMethods(ImportPath, ProtoFile);
            actual.Should().BeEquivalentTo("found services: greet.Greeterfound methods: greet.Greeter.SayHello");
            output.WriteLine(actual);
        }

        [Fact]
        public void Invoke()
        {
            var cmd = new GRPCurl();
            var actual = cmd.Invoke(ImportPath, ProtoFile, "foo");
            const string expected = $"Invoked with: {ImportPath}, {ProtoFile}, foo";
            actual.Should().BeEquivalentTo(expected);
            output.WriteLine(actual);
        }
    }
}