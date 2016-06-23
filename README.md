# KompDiag
Remote Windows Diagnostics Software

=-=-=-=-=
About
=-=-=-=-=

The purpose of this software is to provide a means for a remote technician to support an end user without being physically present.  Since this might mean that the end user might not be that familiar with software, an effort has been made to streamline the interface a great deal.  The use case will almost always be:

1.  Run KompDiag.exe
2.  Press 'Create Report'.
3.  Send generated link to technician.

Generally speaking, you will want to run this software 'as admin', since pulling minidumps and SMART data will almost always require that elevated access.

The code here does not include the creds to actually upload via FTP (This is required functionality and the software will just fail to work if you attempt to run this out of VS.)  If you'd like to use this software, the most recent compiled version can be acquired here:  http://www.kautzman.com/software/kompdiag.exe

Questions, comments and bug reports should go to mkautzm@gmail.com

=-=-=-=-=
About the codebase
=-=-=-=-=

There are two special considerations when looking at the code:

- I lost the source once and had to decompile it to regain said source.  As a result, the spacing is inconsistent and you will likely run into some very generic variable names.  There are a lot of oddities in the code as a result of this unfortunately.
- This was rebranded from 'Sysinfo' to 'KompDiag' some time ago and remenants of Sysinfo still remain.  Don't get confused!
