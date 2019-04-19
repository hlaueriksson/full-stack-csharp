dotnet publish -c Release
del .\docs /s /f /q
xcopy .\src\FullStack.Web\bin\Release\netstandard2.0\publish\FullStack.Web\dist .\docs /s
