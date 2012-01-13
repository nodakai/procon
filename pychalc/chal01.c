#include <Python.h>

static PyObject *
run(PyObject *self, PyObject *args)
{
    const char *command;
    int sts;

    if (!PyArg_ParseTuple(args, "s", &command))
        return NULL;
    sts = system(command);
    return Py_BuildValue("i", sts);
}

static PyMethodDef meth_tab[] = {
    {"run",  run, METH_VARARGS, "Execute a shell command."},
    {} /* Sentinel */
};

PyMODINIT_FUNC
initchal01(void)
{
    (void) Py_InitModule("chal01", meth_tab);
}
