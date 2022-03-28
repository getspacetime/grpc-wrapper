# grpc-wrapper
Use grpcurl in C# applications

## PRE-RELEASE SOFTWARE
**This library is pre-release and should not, in any way, be used in a production system. There is a high potential of significant, breaking changes for the near future.**

## Installation
```
dotnet add package Spacetime.GRPCurl.Wrapper --version 1.0.0
```

## Building
Run the `build.ps1` script in the `go` folder; this will build the DLLs and copies them to the `Spacetime.gRPC.Wrapper\Costura{32|64}` directories.
```
cd src/go
build.ps1
```

The files are included as resources (`Build Action: Embedded resource`) and Fody/Fody.Costura takes care of the rest.

Functions you wish to invoke must be defined in `GoFunctions.cs`.