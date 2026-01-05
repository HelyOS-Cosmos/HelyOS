# HelyOS
## The OS based on Cosmos User Kit (C#)
Hely Operating System writed on C#. It's a shell OS with his script language DB2 (DeBi 2.0)
Commands:
help -arg         unk                    reinstall
acpi              null                   mnt
list <arg>        wf <path> <content>    cd
ls <dir>          cat <path>             slp
touch <name>      shd                    hlt
mkdir <name>      reboot                 trigger
rm <name> -arg    su
echo <text> -arg  usrinfo               

And some debug commands calling from "dbg", like:
@usr $ dbg
@pc-helyos # help
help          cd
db2           makescript
run
testscript
writeeof
@pc-helyos # testvga
...

Commands and his work:

Help - showing the command list.
acpi - Switching ACPI state.
list <dev> (disk / partitions) - Showing all disks or partitions
ls <dir> (or without) - Showing files in directory
touch <name> <size> (size: optional *in bytes*): Make file with name and size (in bytes)
mkdir <name> - make directory with name
rm <name> -arg (-d or not) - remove file or directory (-d for dir's)
echo <text> - print to screen
unk / null - trigger NULL_REFERENCE_ERROR
wf <path\to\file> <text content>: write text to file
cat <path>: read from file
shd - shutdown system
reboot - restart system
su - enable super user
usrinfo - user information (username, password, state)
reinstall - reinstall HelyOS
mnt - mount all (Don't use pls)
cd <dir> - chose directory
slp <time ms> - wait time in ms
hlt - halt cpu (stop)
trigger <code> - trigger error with code

db2 syntax:
ps "text" - print to screen
cls - clear screen
cf - create file
cd - create dir
wait - wait time ms
dd - delete directory
df - delete file
syschk - check system
fschk - check filesystem
fslist - listing of files
erase - erase directory
format - format drive
rinst - reinstall
snf / snd - set name file / dir
p - pause (any key to continue)
shd / rbt - power
ls - listing


Debug commands:
db2 - run command from db2
run - run file
testscript - run 0:\system\test.db2
writeeof - write to file
cd - choice dir
makescript - make script / file
vgatest - test VGA Graphics

All right reserved
