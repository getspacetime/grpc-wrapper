using System.Runtime.InteropServices;

namespace Spacetime.gRPC.Wrapper
{
    internal class GoFunctions
    {

        [DllImport("main", EntryPoint = "Invoke", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        extern internal static IntPtr Invoke(byte[] importPath, byte[] protoFileName, byte[] method);

        [DllImport("main", EntryPoint = "ListServices", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        extern internal static IntPtr ListServices(byte[] importPath, byte[] protoFileName);

        [DllImport("main", EntryPoint = "ListMethods", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        extern internal static IntPtr ListMethods(byte[] importPath, byte[] protoFileName);
    }
}
