#include <Python.h>
#include "chalno.h"

#define CHAL_NO 01

static PyStringObject *shift2(const PyStringObject *orig)
{
    PyObject *item;
    PyStringObject *ret;
    int size = PyString_Size(orig);
    ret = PyString_FromStringAndSize(NULL, size);

    while (item = PyIter_Next(orig)) {
        Py_DECREF(item);
    }

    return ret;
}

static PyStringObject *trans_msg()
{
    static const char msg[] = "g fmnc wms bgblr rpylqjyrc gr zw fylb. rfyrq ufyr "
    "amknsrcpq ypc dmp. bmgle gr gl zw fylb gq glcddgagclr ylb rfyr'q ufw rfgq "
    "rcvr gq qm jmle. sqgle qrpgle.kyicrpylq() gq pcamkkclbcb. lmu ynnjw ml rfc spj.";

    return NULL;
}

static PyObject *
run(PyObject *self, PyObject *args)
{
    int i;
    PyObject *mod_string, *mod_string_dict, *maketrans;


    mod_string = PyImport_ImportModule("string");
    printf("%d\n", mod_string->ob_refcnt);

    mod_string_dict = PyModule_GetDict(mod_string);
    printf("%d\n", mod_string_dict->ob_refcnt);

    maketrans = PyDict_GetItemString(mod_string_dict, "maketrans");
    printf("%d\n", maketrans->ob_refcnt);

    Py_INCREF(maketrans);

    Py_DECREF(mod_string);

    return maketrans;
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
