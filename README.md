# NumbersToWords
Converting an inputted number to its wordful counterpart

## Preqs
- .Net 8.0

---

## Build Using Visual Studio
1. Open the project in Visual Studio
2. Ensure the build configuration is in **Release** 
3. Navigate to 'Build >> Build Solution' or pressing **Ctrl + B** to build 

## Build via Developer Powershell
1. Open the project in Visual Studio and open the 'Developer Powershell' by navigating to 'Tools >> Command Line >> Developer Powershell'
2. 'cd' into 'NumbersToWords' directory 
3. Enter 'dotnet restore' command 
4. Enter 'dotnet build -c 'Release'' to build

## Host locally
1. Open the project in Visual Studio
2. Open the 'Developer Powershell' by navigating to 'Tools >> Command Line >> Developer Powershell'
3. Enter 'dotnet run -c 'Release' to start hosting the program

## The application will run at:
http://localhost:5000

## App Usage
1. Open homepage
2. Enter a numerical value in the input field
3. Press the 'Submit' button
4. API returns the translated value from the inputted numerical value


## Optional Host via IIS 
## Enable IIS 
If you haven't already enabled ISS then follow this guide
1. Press 'Windows Key' and open the 'Windows Features' application
2. If not selected already, select 'Internet Information Services' and ensure that 'Web Management Tools' & 'World Wide Web Services' are selected under it
3. Press 'Ok'

## Steps to Host via IIS 
[ASP.NET Core 10.0 Windows Hosting Bundle](https://dotnet.microsoft.com/en-us/download/dotnet/thank-you/runtime-aspnetcore-10.0.1-windows-hosting-bundle-installer)



