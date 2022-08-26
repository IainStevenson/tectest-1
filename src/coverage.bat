SETLOCAL
set current_dir=%CD%
set VSTest=C:\Program Files\Microsoft Visual Studio\2022\Professional\Common7\IDE\CommonExtensions\Microsoft\TestWindow\VSTest.Console.exe
set NUGETFolder=%USERPROFILE%\.nuget\packages
SET Cover=%NUGETFolder%\OpenCover\4.7.1221\tools\OpenCover.Console.exe
SET report=%NUGETFolder%\ReportGenerator\5.1.6\tools\net5.0\ReportGenerator.exe

SET target=%current_dir%\tectest1.Api.Domain.Tests\bin\Debug\net6.0\tectest1.Api.Domain.tests.dll
SET coverfile=%current_dir%\CoverageResults.xml
SET coverreport=%current_dir%\CoverageReport
SET filter="+[tectest1.Api.Domain*]* -[tectest1.Api.Domain.Tests*]* -[System.Runtime.CompilerServices*]*"

dotnet build tectest1.sln 

%Cover% -target:"%VSTest%" -targetargs:%target% -output:%coverfile% -register:user -filter:%filter% -excludebyattribute:*.ExcludeFromCodeCoverage*


%report% -reports:%coverfile% -targetdir:%coverreport%

start %coverreport%\index.html
ENDLOCAL