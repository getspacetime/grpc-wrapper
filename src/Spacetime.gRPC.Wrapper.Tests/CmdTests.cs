using Xunit;

namespace Spacetime.gRPC.CommandLine.Tests
{
    public class CmdTests
    {
        [Fact]
        public void Test1()
        {
            var cmd = new Cmd();
            cmd.Execute();
        }
    }
}