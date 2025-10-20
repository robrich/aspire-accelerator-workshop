import { defineConfig } from 'vite';
import react from '@vitejs/plugin-react';
import { fileURLToPath, URL } from 'node:url';


// Get variables injected from Aspire:
const port = process.env.PORT ? parseInt(process.env.PORT) : 3000;

const frameworkApi = process.env.services__frameworkapi__http__0;
const voteGet = process.env.services__funcVoteGet__http__0;
const voteScore = process.env.services__funcVoteScore__http__0;

// Shim the OTEL env vars to vars Vite will proxy into the browser
if (process.env.OTEL_EXPORTER_OTLP_ENDPOINT) {
  process.env.VITE_OTEL_EXPORTER_OTLP_ENDPOINT = process.env.OTEL_EXPORTER_OTLP_ENDPOINT;
}
if (process.env.OTEL_EXPORTER_OTLP_HEADERS) {
  process.env.VITE_OTEL_EXPORTER_OTLP_HEADERS = process.env.OTEL_EXPORTER_OTLP_HEADERS;
}
if (process.env.OTEL_RESOURCE_ATTRIBUTES) {
  process.env.VITE_OTEL_RESOURCE_ATTRIBUTES = process.env.OTEL_RESOURCE_ATTRIBUTES;
}
if (process.env.OTEL_SERVICE_NAME) {
  process.env.VITE_OTEL_SERVICE_NAME = process.env.OTEL_SERVICE_NAME;
}

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
  plugins: [react()],
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
