:: set the path to scanner and path to results file. path to project is the path this batch file is located at
set "pathToProject=%~dp0"
set "pathToScanner=c:\users\antero360\source\tools\security\horusec\horusec.exe"
set "pathToResults=%pathToProject%\testing-results.json"

:: call command prompt to execute call to scanner 
cmd.exe /k "%pathToScanner% start -p %pathToProject% --output-format json --json-output-file %pathToResults% --disable-docker='true'"
