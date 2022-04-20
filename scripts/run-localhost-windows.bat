set ENV_FILE="wwwroot\src\common\env.js"
set ENV_LOCALHOST_FILE="wwwroot\src\common\env-localhost.js"

del %ENV_FILE%

copy %ENV_LOCALHOST_FILE% %ENV_FILE%

start "" "https://localhost:7207"

WebUI.exe --urls="http://localhost:5207;https://localhost:7207"
