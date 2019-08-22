# DirectoryDesigner
Creates folder structures from a file

#Instructions
1)Populate the Top Level Folder Location box with the directory you wish to create your file structure.
2)Populate the Directory Outline box with an outline file.
3)Create your directories!

#Notes
The Directory Outline should be a .txt file organized with tabs
EX
NewFolder
	NewFolder
	NewFolder(1)
NewFolder(1)
	NewFolder
	NewFolder(1)

If you attempt to create a file structure somewhere you do not have permission to, the program may throw an error.

This program will not allow the creation of directories with invalid characters.  These characters include: <, >, *, /, \, ?, |
If you attempt to create a file with these characters, the program will offer to replace all invalid characters with underscores.

This program will not allow the creation of files where the outline has invalid indentation.  This ensures that you create the files exactly where you want them to be.

If the program requires you to fix your outline folder, it is recommended to start at the top.  An error at the beginning may cause errors below!

