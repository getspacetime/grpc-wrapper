go build --buildmode=c-shared -ldflags="-s -w"  -o main.dll main.go
.\copy.ps1