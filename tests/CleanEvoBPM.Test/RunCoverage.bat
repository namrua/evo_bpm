dotnet test CleanEvoBPM.Test.csproj /p:CollectCoverage=true  /p:CoverletOutput=TestResults/ /p:CoverletOutputFormat=lcov"
dotnet "C:\Users\dungtranv\.nuget\packages\reportgenerator\4.7.1\tools\netcoreapp2.1\ReportGenerator.dll" "-reports:.\TestResults\coverage.info" "-targetdir:D:\Reports"