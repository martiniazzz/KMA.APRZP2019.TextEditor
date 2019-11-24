# KMA.APRZP2019.TextEditor

## Description

This is a Text Editor Application. 
Each user separately logs in to the system. The user selects a text file and receives it for editing. After editing, user can save all
changes. Files are opened and saved in separate threads. Each user request is stored in the database. User can view the edit history.

## Run project

1. To run project you should have .NET Framework 4.7.2 installed on your computer
2. Clone or download project
3. Right click on solution -> Load project dependencies
4. Right click on solution -> Restore NuGet Packages
5. Right click on solution -> Properties -> Set EntityFrameworkWrapper as startup project
6. Tools -> NuGet Package Manager -> Package Manager Console
7. Set EntityFrameworkWrapper project as Default project in opened console
8. Run command: Update-Database -TargetMigration: UserModelChangeMigration
9. Right click on solution -> Properties -> Multiple startup projects -> Set TextEditorWebApp and TextEditor projects as start
10. Start project
11. Note : if project didn't start at first time, try rebuilding solution


##

<b> The project is being developed by Babelska Marta and Magur Ksenia </b>
