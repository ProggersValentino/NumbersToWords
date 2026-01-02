# NumbersToWords
Translating a numerical value given to its word form

## Prerequisites
- .Net 8.0
- Visual Studio 2022 17.14.13 or higher
- ASP.NET and web development workload 

---

## Build Using Visual Studio
1. Open the project in Visual Studio
2. Ensure the build configuration is in **Release** 
3. Navigate to 'Build >> Build Solution' or pressing **Ctrl + B** to build 

## Build via Developer Powershell
1. Open the project in Visual Studio and open the 'Developer Powershell' by navigating to 'Tools >> Command Line >> Developer Powershell'
2. 'cd' into the 'NumbersToWords' project directory so you should be in the **directory path: '\NumbersToWords-main\NumbersToWords-main\NumbersToWords'** 
3. Enter 'dotnet restore' command 
4. Enter 'dotnet build -c 'Release'' to build

## Host locally
1. Open the project in Visual Studio
2. Open the 'Developer Powershell' by navigating to 'Tools >> Command Line >> Developer Powershell'
3. Enter 'dotnet run -c 'Release'' to start hosting the program

## The application will run at:
http://localhost:5000

## App Usage
1. Open homepage
2. Enter a numerical value in the input field
3. Press the 'Translate' button
4. API returns the translated value from the inputted numerical value

## Troubleshooting

**Error**: Couldn't find a project to run; **Solution**: Ensure you have cd into the **directory path: '\NumbersToWords-main\NumbersToWords-main\NumbersToWords'** 
