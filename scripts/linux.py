from subprocess import PIPE, Popen
import time, datetime

title = ''
root_check = ''

while True:
    time.sleep(5)
    root = Popen(['xprop', '-root'],  stdout=PIPE)

    if root.stdout != root_check:
        root_check = root.stdout

        for i in root.stdout:
            if '_NET_ACTIVE_WINDOW(WINDOW):' in i:
                id_ = i.split()[4]
                id_w = Popen(['xprop', '-id', id_], stdout=PIPE)

        for j in id_w.stdout:
            if 'WM_ICON_NAME(STRING)' in j:
                #if title != j.split()[2:]:
                title = j.split()[2:]
                print "%s %s" % (datetime.datetime.now()," ".join(title))
