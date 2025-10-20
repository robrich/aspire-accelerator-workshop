import { /*ConsoleSpanExporter,*/ SimpleSpanProcessor } from '@opentelemetry/sdk-trace-base';
import { WebTracerProvider } from '@opentelemetry/sdk-trace-web';
import { resourceFromAttributes } from '@opentelemetry/resources';
import { ATTR_SERVICE_NAME } from '@opentelemetry/semantic-conventions';
import { ZoneContextManager } from '@opentelemetry/context-zone';
import { registerInstrumentations } from '@opentelemetry/instrumentation';
import { getWebAutoInstrumentations } from '@opentelemetry/auto-instrumentations-web';
import { OTLPTraceExporter } from '@opentelemetry/exporter-trace-otlp-proto';


let provider: WebTracerProvider | undefined = undefined;

export interface TelemetryArgs {
  otlpEndpoint: string;
  headers: string;
  resourceAttributes: string;
  serviceName: string;
}

export function initializeTelemetry(args: TelemetryArgs): WebTracerProvider | undefined {
  if (!args?.otlpEndpoint) {
    return provider; // OpenTelemetry is not enabled
  }

  console.log(`Initializing OpenTelemetry Tracing: ${JSON.stringify(args, null, 2)}`);

  const otlpOptions = {
    url: `${args.otlpEndpoint}/v1/traces`,
    headers: parseDelimitedValues(args?.headers),
  };

  const attributes = parseDelimitedValues(args.resourceAttributes);
  if (args.serviceName) {
    attributes[ATTR_SERVICE_NAME] = args.serviceName;
  }

  provider = new WebTracerProvider({
    resource: resourceFromAttributes(attributes),
    spanProcessors: [
      new SimpleSpanProcessor(new OTLPTraceExporter(otlpOptions))
      // new SimpleSpanProcessor(new ConsoleSpanExporter()) // too noisy?
    ]
  });

  provider.register({
    contextManager: new ZoneContextManager()
  });

  registerInstrumentations({
    instrumentations: [
      getWebAutoInstrumentations(), // load documentLoad, fetch, userInteraction, xmlHttpRequest
    ]
  });

  return provider;
}

function parseDelimitedValues(s: string | undefined): Record<string, string> {
  const headers = s?.split(',') || []; // Split by comma, ASSUME: commas in keys or values are encoded
  const o: Record<string, string> = {};

  headers.forEach((header) => {
    const [key, value] = header.split('='); // Split by equal sign
    if (key && value) {
      o[key.trim()] = value.trim(); // Add to the object, trimming spaces
    }
  });

  return o;
}
