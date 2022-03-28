using System.Runtime.InteropServices;
using System.Text;

namespace Spacetime.gRPC.CommandLine
{
    public class Cmd
    {
        [DllImport("main", EntryPoint = "Sum")]
        extern static int Sum(int a, int b);

        [DllImport("main", EntryPoint = "Test", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        extern static IntPtr Test(byte[] importPath, byte[] protoFileName);
        public void Execute()
        {
            try
            {
                var importPath = "C:\\Users\\Cody\\source\\repos\\Spacetime.gRPC\\Spacetime.gRPC\\Protos";
                var importPathAsBytes = Encoding.UTF8.GetBytes(importPath);

                var protoFile = "greet.proto";
                var protoFileAsBytes = Encoding.UTF8.GetBytes(protoFile);

                var foo = Test(importPathAsBytes, protoFileAsBytes);
                var str = Marshal.PtrToStringUTF8(foo);
                Console.WriteLine(str);
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}