import { defineConfig } from 'vite';
import vue from '@vitejs/plugin-vue';
import { fileURLToPath, URL } from 'node:url';


// Get variables injected from Aspire:
const port = process.env.PORT ? parseInt(process.env.PORT) : 3000;

const frameworkApi = process.env.services__frameworkapi__http__0;
const voteGet = process.env.services__funcVoteGet__http__0;
const voteScore = process.env.services__funcVoteScore__http__0;

// TODO: also grab OTEL variables

console.log('Using port and proxying to', {
  port,
  '/api/framework': frameworkApi,
  '/api/vote/get': voteGet,
  '/api/vote/score': voteScore
});

const proxy: Record<string, { target: string; changeOrigin: boolean }> = {};
if (frameworkApi) {
  proxy['/api/framework'] = {
    target: frameworkApi,
    changeOrigin: true
  };
}
if (voteGet) {
  proxy['/api/vote/get'] = {
    target: voteGet,
    changeOrigin: true
  };
}
if (voteScore) {
  proxy['/api/vote/score'] = {
    target: voteScore,
    changeOrigin: true
  };
}

// https://vitejs.dev/config/
export default defineConfig({
  plugins: [vue()],
  resolve: {
    alias: {
      '@': fileURLToPath(new URL('./src', import.meta.url))
    }
  },
  server: {
    port,
    proxy,
    allowedHosts: ['localhost', 'host.docker.internal']
  }
});
