CPPFLAGS= -I/usr/include/python2.4
CFLAGS= -Wall -Wextra -g -O3 -shared -fPIC

sources=$(wildcard chal*.c)
sharedlibs=$(patsubst %.c,%.so,$(sources))

.PHONY : all clean

all : $(sharedlibs)
	

chal%.so : chal%.c
	$(CC) $(CPPFLAGS) $(CFLAGS) $< $(LDFLAGS) -o $@

clean :
	rm -f *~ $(sharedlibs)
