import React from 'react';
import { createRoot } from 'react-dom/client';
import { initializeTelemetry } from './tracing';
import App from './App';

const env = import.meta.env;
const otlpEndpoint = env.VITE_OTEL_EXPORTER_OTLP_ENDPOINT;
const headers = env.VITE_OTEL_EXPORTER_OTLP_HEADERS;
const resourceAttributes = env.VITE_OTEL_RESOURCE_ATTRIBUTES; // NAME_A=VAL_A,NAME_B=VAL_B
const serviceName = env.VITE_OTEL_SERVICE_NAME;

initializeTelemetry({otlpEndpoint, headers, resourceAttributes, serviceName});

const container = document.getElementById('app');
if (!container) {
  throw new Error('Root element #app not found');
}
const root = createRoot(container);
root.render(
  <React.StrictMode>
    <App />
  </React.StrictMode>
);
