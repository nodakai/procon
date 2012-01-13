#include <Python.h>
#include "chalno.h"

#define CHAL_NO 00

static PyObject *
run(PyObject *self, PyObject *args)
{
    int i;
    PyObject *two, *res;

    two = PyInt_FromLong(2L);

    res = two;
    Py_INCREF(res);

    for (i = 1; i < 38; ++i) {
        PyObject *new = PyNumber_Multiply(res, two);
        Py_DECREF(res);
        res = new;
    }

    Py_DECREF(two);
    return res;
}

static PyMethodDef meth_tab[] = {
    {"run",  run, METH_NOARGS, "chal" CHAL_NO_S ".run : void -> int"},
    {} /* Sentinel */
};

PyMODINIT_FUNC
MAC_CONCAT(initchal, CHAL_NO)(void)
{
    (void) Py_InitModule("chal" CHAL_NO_S, meth_tab);
}
