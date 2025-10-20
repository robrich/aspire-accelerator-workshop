import { createApp } from 'vue';
import { initializeTelemetry } from './tracing';
import App from './App.vue';

const env = import.meta.env;
const otlpEndpoint = env.VITE_OTEL_EXPORTER_OTLP_ENDPOINT;
const headers = env.VITE_OTEL_EXPORTER_OTLP_HEADERS;
const resourceAttributes = env.VITE_OTEL_RESOURCE_ATTRIBUTES; // NAME_A=VAL_A,NAME_B=VAL_B
const serviceName = env.VITE_OTEL_SERVICE_NAME;

initializeTelemetry({otlpEndpoint, headers, resourceAttributes, serviceName});

createApp(App).mount('#app');
