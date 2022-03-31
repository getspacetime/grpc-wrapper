using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;

namespace Spacetime.gRPC.Wrapper
{
    public class GRPCurl
    {
        public GrpcInvocationResult Invoke(string importPath, string protoFileName, string method)
        {
            return new GrpcInvocationResult();
            //try
            //{
            //    var importPathAsBytes = Encode(importPath);
            //    var protoFileAsBytes = Encode(protoFileName);
            //    var methodFileAsBytes = Encode(method);

            //    var foo = GoFunctions.Invoke(importPathAsBytes, protoFileAsBytes, methodFileAsBytes);
            //    var str = Marshal.PtrToStringUTF8(foo);
            //    return str ?? string.Empty;
            //}
            //catch (Exception ex)
            //{
            //    return ex.Message;
            //}
        }
        public GrpcExplorerResult ListServices(string importPath, string protoFileName)
        {
            var result = new GrpcExplorerResult();

            try
            {
                var importPathAsBytes = Encode(importPath);
                var protoFileAsBytes = Encode(protoFileName);

                var foo = GoFunctions.ListServices(importPathAsBytes, protoFileAsBytes);
                var str = Marshal.PtrToStringUTF8(foo);

                if (str != null)
                {
                    var services = JsonSerializer.Deserialize<List<string>>(str);
                    if (services != null)
                    {
                        result.Items = services.Select(p => new GrpcEntity { Name = p });
                    }
                }
            }
            catch (Exception ex)
            {
                result.Error = true;
                result.ErrorDetails = ex.Message;
            }

            return result;
        }

        public GrpcExplorerResult ListMethods(string importPath, string protoFileName, string svc)
        {
            var result = new GrpcExplorerResult();

            try
            {
                var importPathAsBytes = Encode(importPath);
                var protoFileAsBytes = Encode(protoFileName);
                var svcAsBytes = Encode(svc);

                var foo = GoFunctions.ListMethods(importPathAsBytes, protoFileAsBytes, svcAsBytes);
                var str = Marshal.PtrToStringUTF8(foo);


                if (str != null)
                {
                    var services = JsonSerializer.Deserialize<List<string>>(str);
                    if (services != null)
                    {
                        result.Items = services.Select(p => new GrpcEntity { Name = p });
                    }
                }
            }
            catch (Exception ex)
            {
                result.Error = true;
                result.ErrorDetails = ex.Message;
            }

            return result;
        }

        private byte[] Encode(string s)
        {
            return Encoding.UTF8.GetBytes(s);
        }
    }

    public class GrpcExplorerResult
    {
        public IEnumerable<GrpcEntity> Items { get; set; } = new List<GrpcEntity>();
        public bool Error { get; set; }
        public string ErrorDetails { get; set; }
    }

    public class GrpcInvocationResult
    {
        public bool Error { get; set; } = true;
        public string ErrorDetails { get; set; } = "Not implemented";
    }

    public class GrpcEntity
    {
        public string Name { get; set; }
    }
}