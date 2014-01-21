#!/usr/bin/env python

from subprocess import PIPE, Popen
import time, datetime, re

def get_active_window_title():
    state = str(datetime.datetime.now())
    root = Popen(['xprop', '-root', '_NET_ACTIVE_WINDOW'], stdout=PIPE)

    for line in root.stdout:
        m = re.search('^_NET_ACTIVE_WINDOW.* ([\w]+)$', line)
        if m != None:
            id_ = m.group(1)
            id_w = Popen(['xprop', '-id', id_, 'WM_NAME'], stdout=PIPE)
            name_w = Popen(['xprop', '-id', id_, 'WM_CLASS'], stdout=PIPE)
            break
    if name_w != None:
        for line in name_w.stdout:
            match = re.match("WM_CLASS\(\w+\) = (?P<name>.+)$", line)
            if match != None:
                state = state + " " + match.group('name')

    if id_w != None:
        for line in id_w.stdout:
            match = re.match("WM_NAME\(\w+\) = (?P<name>.+)$", line)
            if match != None:
                state += " " + match.group('name')
    return state

while True:
    time.sleep(5)
    print get_active_window_title()
