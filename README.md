# ArchiveLibrary
Library that relies upon SevenZipLib

## Archive ##
Static class that exposes methods to manage compressed (7zip) files.

### Compress ###
Compress file(s) into an archive.  Accepts a path to the files to be added to the archive, a file mask for filtering the files (regular Windows filename filter), and an FQPN for the resulting archive file.

### Extract ###
Extract files from an archive.  Accepts an FQPN to the archive file and a path to where the files should be extracted.  Existing files in the destination are overwritten.
