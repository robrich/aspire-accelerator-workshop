# Aspire Everything (React)

Converted from Vue 3 to React 18 with Vite + TypeScript.

## Scripts

- dev: start Vite dev server
- build: type check and build for production
- preview: preview the production build

## Run locally

```pwsh
npm install
npm run dev
```

If port 3000 is in use, Vite selects another port (see terminal output for the URL).

## Build and preview

```pwsh
npm run build
npm run preview
```

## Notes

- API proxy targets are read from env vars by `vite.config.ts`:
	- services__frameworkapi__http__0
	- services__voteget__http__0
	- services__votescore__http__0
- Source is now React (`.tsx` in `src/`). Vue SFCs are no longer used.
