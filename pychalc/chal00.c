#include <Python.h>

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
    {"run",  run, METH_NOARGS, "chal00.run : void -> int"},
    {} /* Sentinel */
};

PyMODINIT_FUNC
initchal00(void)
{
    (void) Py_InitModule("chal00", meth_tab);
}
