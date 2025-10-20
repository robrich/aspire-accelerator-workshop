Vue.js front-end to the Voting app
==================================

An example frontend written in Vue.js 3 and TypeScript.

Note: All front-ends are the identical UX.


Run the sample
--------------

```sh
npm install
npm run dev
```

Browse to http://localhost:3000/

Alternatively, run the entire sample through Visual Studio or VS Code by launching the AspireEverything.AppHost as the startup project.


About the sample
----------------

This app uses Vue.js, Vite, and TypeScript.

Vite is a great TypeScript development server, and also serves as a reverse-proxy to the backend APIs.

In the vite.config.ts, we read the environment variables from the Aspire AppHost and setup the reverse proxy:

| Environment Variable | URL | Proxy to |
| -------------------- | --- | -------- |
| services__frameworkapi__http__0 | /api/framework | AspireEverything.FrameworkApi |
| services__voteget__http__0 | /api/vote/get | AspireEverything.VoteGet |
| services__votescore__http__0 | /api/vote/score | AspireEverything.ViteScore |
