ENV_FILE="wwwroot/src/common/env.js"
ENV_LOCALHOST_FILE="wwwroot/src/common/env-localhost.js"

rm $ENV_FILE

cp $ENV_LOCALHOST_FILE $ENV_FILE

./WebUI.exe --urls="http://localhost:5207;https://localhost:7207"
