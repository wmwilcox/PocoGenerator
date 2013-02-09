set resultdir=testResults
set projName=PocoGenerator
set packageDir=.\packages
set nunit=%packageDir%\NUnit.Runners.2.6.2\tools\nunit-console.exe
set nunit2report=%packageDir%\NUnit2Report.Console.Runner.1.0.0.0\NUnit2Report.Console.exe
set openCover=%packageDir%\OpenCover.4.0.804\OpenCover.Console.exe
set filter="+[%projName%*]* -[%projName%.Test*]*"
set reportGenerator=%packageDir%\ReportGenerator.1.7.1.0\ReportGenerator.exe
set testAssembly=.\%projName%.Tests\bin\Debug\%projName%.Tests.dll
set targetDir=.\%projName%.Tests\bin\Debug
set results=.\%resultdir%
set outputFile=%results%\coverageResults.xml
set nUnitArgs="/noshadow %testAssembly% /domain:single /xml:%results%\unit-test-results.xml"

mkdir %resultDir%

%opencover% -target:%nunit% -targetargs:%nUnitArgs% -output:%outputFile% -filter:%filter% -register:user
%reportGenerator% -reports:%outputFile% -reporttypes:Html -targetDir:%results%
%nunit2report% --fileset=%results%\unit-test-results.xml --todir=%results% --out=unit-test-result.html