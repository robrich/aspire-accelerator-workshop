#!/bin/sh
set -ex

IMAGE_LABEL="latest"
ACR_URL="robrich.azurecr.io"

# Build web frontends
cd AspireEverything.WebBlazor
docker build -t "${ACR_URL}/aspire-blazor:${IMAGE_LABEL}" .
cd ..

cd AspireEverything.WebReact
docker build -t "${ACR_URL}/aspire-react:${IMAGE_LABEL}" .
cd ..

cd AspireEverything.WebVue
docker build -t "${ACR_URL}/aspire-vue:${IMAGE_LABEL}" .
cd ..

# Build the solution
docker build --tag build --target build .

# Run unit tests
docker build --target export-test-results --output type=local,dest=results .

# Run integration tests
# FRAGILE: ASSUME: postgres and redis are running and postgres db is initialized
# docker run -e ConnectionStrings__voting="..." -e ConnectionStrings__cache="..." -v ./results:/src/results test-integration

# Build the containers we want to publish
docker build -t "${ACR_URL}/aspire-frameworkapi:${IMAGE_LABEL}" --target frameworkapi .
docker build -t "${ACR_URL}/aspire-func-voteget:${IMAGE_LABEL}" --target func-voteget .
docker build -t "${ACR_URL}/aspire-func-votescore:${IMAGE_LABEL}" --target func-votescore .

# Push the containers
docker push "${ACR_URL}/aspire-blazor:latest"
docker push "${ACR_URL}/aspire-react:${IMAGE_LABEL}"
docker push "${ACR_URL}/aspire-vue:${IMAGE_LABEL}"
docker push "${ACR_URL}/aspire-frameworkapi:${IMAGE_LABEL}"
docker push "${ACR_URL}/aspire-func-voteget:${IMAGE_LABEL}"
docker push "${ACR_URL}/aspire-func-votescore:${IMAGE_LABEL}"

# Uncomment your chosen deployment strategy:

# Run the k8s deployment
# helm template k8s --set imageLabel=${IMAGE_LABEL} --set acrUrl=${ACR_URL} --set aksUrl=${AKS_URL} | kubectl apply -f -

# Run the docker compose deployment
# docker compose up -d
