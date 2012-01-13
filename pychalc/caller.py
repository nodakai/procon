#! /usr/bin/env python

import sys

def main(args):
    if len(args) == 0:
        print >>sys.stderr, "Wrong # of arguments: %r" % args
        print >>sys.stderr, "USAGE: caller.py 01 etc."
        sys.exit(10)

    mod_name = "chal" + args[0]
    mod = __import__(mod_name)
    print "%s.run(%r) => %s" % (mod_name, args[1:], mod.run(*args[1:]))

if __name__ == '__main__':
    main(sys.argv[1:])
