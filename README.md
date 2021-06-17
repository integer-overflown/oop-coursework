# Course Work on OOP
## Code name 'eveRead'
An application that enables user to add, remove and maintain their books, e.g. home library.

Made with love and **Avalonia**

## Build
To produce and run .NET dll file:
```
cd %SOURCE%
dotnet build CourseWork
dotnet run --project CourseWork
```
Hereinafter SOURCE is supposed to be the path to the solution root (where .sln is located)

Platform-specific binary can be assembled by:
```
dotnet publish CourseWork -r <RID> --self-contained false
```
Replace RID with your target platform's runtime identifier.
Common RIDs include:
- win10-x64 / win10-x86 - Windows 10 (64 and 32 bits respectively)
- win7-x64 / win7-x86 - Windows 7
- linux-x64 - most Linux distributions, including as Ubuntu, Fedora, CentOS, Debian and derivatives