package main

/*
#include <stdlib.h>
*/
import "C"
import (
	"fmt"

	"github.com/fullstorydev/grpcurl"
)

func main() {
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
func ListMethods(importPath *C.char, protoFileName *C.char) *C.char {
	paths := make([]string, 2)
	paths[0] = C.GoString(importPath)

	//file := "greet.proto"
	file := C.GoString(protoFileName)
	src, err := grpcurl.DescriptorSourceFromProtoFiles(paths, file)
	if err != nil {
		return C.CString("err generating descriptor sources: " + err.Error())
	}

	svcs, err := grpcurl.ListServices(src)
	if err != nil {
		return C.CString("err listing services: " + err.Error())
	}

	if err != nil {
		return C.CString("err listing methods: " + err.Error())
	}

	if len(svcs) > 0 {
		svc := svcs[0]

		res := "found services: " + svc

		methods, err := grpcurl.ListMethods(src, svc)

		if err == nil && len(methods) > 0 {
			res = res + "found methods: " + methods[0]
		}

		if err != nil {
			res = res + "error getting methods: " + err.Error()
		}

		return C.CString(res)
	}

	str := C.CString("end of method")
	return str
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

	svcs, err := grpcurl.ListServices(src)
	if len(svcs) > 0 {
		svc := svcs[0]

		res := "found services: " + svc

		return C.CString(res)
	}

	str := C.CString("end of method")
	return str
}

//Sum :
//export Sum
func Sum(a, b int) int {
	return a + b
}
