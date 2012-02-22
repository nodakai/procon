PYTHON_VER= 2.4
CPPFLAGS= -I/usr/include/python$(PYTHON_VER)
CFLAGS= -Wall -Wextra -g -O3 -shared -fPIC

sources=$(wildcard chal*.c)
sharedlibs=$(patsubst %.c,%.so,$(sources))

.PHONY : all clean

all : $(sharedlibs)

chal%.so : chal%.c
	$(CC) $(CPPFLAGS) $(CFLAGS) $< $(LDFLAGS) -o $@

clean :
	rm -f *~ $(sharedlibs)
