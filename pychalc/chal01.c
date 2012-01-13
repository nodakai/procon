#include <Python.h>
#include "chalno.h"

#define CHAL_NO 01

static const char msg[] = "g fmnc wms bgblr rpylqjyrc gr zw fylb. rfyrq ufyr "
"amknsrcpq ypc dmp. bmgle gr gl zw fylb gq glcddgagclr ylb rfyr'q ufw rfgq "
"rcvr gq qm jmle. sqgle qrpgle.kyicrpylq() gq pcamkkclbcb. lmu ynnjw ml rfc spj.";

static PyObject *
run(PyObject *self, PyObject *args)
{
    int i;
    PyObject *two, *res;


    return res;
}

static PyMethodDef meth_tab[] = {
    {"run",  run, METH_NOARGS, "chal" CHAL_NO_S ".run : void -> str"},
    {} /* Sentinel */
};

PyMODINIT_FUNC
MAC_CONCAT(initchal, CHAL_NO)(void)
{
    (void) Py_InitModule("chal" CHAL_NO_S, meth_tab);
}
