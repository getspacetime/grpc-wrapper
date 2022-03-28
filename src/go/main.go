package main

/*
#include <stdlib.h>
*/
import "C"
import (
	"github.com/fullstorydev/grpcurl"
)

func main() {
}

//Test :
//export Test
func Test(importPath *C.char, protoFileName *C.char) *C.char {
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
