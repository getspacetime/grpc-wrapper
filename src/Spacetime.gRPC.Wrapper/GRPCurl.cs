using System.Runtime.InteropServices;
using System.Text;

namespace Spacetime.gRPC.Wrapper
{
    public class GRPCurl
    {
        public string Invoke(string importPath, string protoFileName, string method)
        {
            try
            {
                var importPathAsBytes = Encode(importPath);
                var protoFileAsBytes = Encode(protoFileName);
                var methodFileAsBytes = Encode(method);

                var foo = GoFunctions.Invoke(importPathAsBytes, protoFileAsBytes, methodFileAsBytes);
                var str = Marshal.PtrToStringUTF8(foo);
                return str ?? string.Empty;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public string ListServices(string importPath, string protoFileName)
        {
            try
            {
                var importPathAsBytes = Encode(importPath);
                var protoFileAsBytes = Encode(protoFileName);

                var foo = GoFunctions.ListServices(importPathAsBytes, protoFileAsBytes);
                var str = Marshal.PtrToStringUTF8(foo);
                return str ?? string.Empty;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string ListMethods(string importPath, string protoFileName)
        {
            try
            {
                var importPathAsBytes = Encode(importPath);
                var protoFileAsBytes = Encode(protoFileName);

                var foo = GoFunctions.ListMethods(importPathAsBytes, protoFileAsBytes);
                var str = Marshal.PtrToStringUTF8(foo);
                return str ?? string.Empty;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        private byte[] Encode(string s)
        {
            return Encoding.UTF8.GetBytes(s);
        }
    }
}