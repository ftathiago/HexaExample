git init
npm init --yes && npx husky-init && npm install && npm install lint-staged --save-dev && copy tools\pre-commit.sample .husky\pre-commit && copy tools\pre-push.sample .husky\pre-push
dotnet format
