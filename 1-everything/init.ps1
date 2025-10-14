$ErrorActionPreference = "Stop"

echo "VoteGet Azure Function: copying local.settings.json into place"
cd AspireEverything.VoteGet
cp local.settings.json.example local.settings.json
cd ..

echo "VoteSet Azure Function: copying local.settings.json into place"
cd AspireEverything.VoteGet
cp local.settings.json.example local.settings.json
cd ..

echo "React frontend: npm install"
cd AspireEverything.WebReact
npm install
cd ..

echo "Vue.js frontend: npm install"
cd AspireEverything.WebVue
npm install
cd ..
