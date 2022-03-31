package main

/*
#include <stdlib.h>
*/
import "C"
import (
	"encoding/json"
	"fmt"

	"github.com/fullstorydev/grpcurl"
)

func main() {
}

type service struct {
	name string
}

//Invoke :
//export Invoke
func Invoke(importPath *C.char, protoFileName *C.char, methodName *C.char) *C.char {
	path := C.GoString(importPath)
	file := C.GoString(protoFileName)
	method := C.GoString(methodName)
	return C.CString(fmt.Sprintf("Invoked with: %s, %s, %s", path, file, method))
}

//ListMethods :
//export ListMethods
func ListMethods(importPath *C.char, protoFileName *C.char, svc *C.char) *C.char {
	paths := make([]string, 2)
	paths[0] = C.GoString(importPath)
	service := C.GoString(svc)

	//file := "greet.proto"
	file := C.GoString(protoFileName)
	src, err := grpcurl.DescriptorSourceFromProtoFiles(paths, file)
	if err != nil {
		return C.CString("err generating descriptor sources: " + err.Error())
	}

	methods, err := grpcurl.ListMethods(src, service)

	// todo: handle err properly
	return toJson(methods)
}

//ListServices :
//export ListServices
func ListServices(importPath *C.char, protoFileName *C.char) *C.char {
	paths := make([]string, 2)
	paths[0] = C.GoString(importPath)

	file := C.GoString(protoFileName)
	src, err := grpcurl.DescriptorSourceFromProtoFiles(paths, file)
	if err != nil {
		return C.CString("err generating descriptor sources: " + err.Error())
	}

	// todo: handle err properly
	svcs, err := grpcurl.ListServices(src)

	return toJson(svcs)
}

func toJson(array []string) *C.char {
	if len(array) > 0 {
		bytes, err := json.Marshal(array)
		if err == nil {
			return C.CString(string(bytes))
		}

		return C.CString(err.Error())
	}

	return C.CString("")
}
