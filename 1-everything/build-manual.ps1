$ErrorActionPreference = "Stop"

$IMAGE_LABEL = "latest"
$ACR_URL = "robrich.azurecr.io"

cd AspireEverything.WebBlazor
docker build -t $ACR_URL/aspire-blazor:$IMAGE_LABEL .
cd ..

cd AspireEverything.WebReact
docker build -t $ACR_URL/aspire-react:$IMAGE_LABEL .
cd ..

cd AspireEverything.WebVue
docker build -t $ACR_URL/aspire-vue:$IMAGE_LABEL .
cd ..

# build the solution
docker build --tag build --target build .

# run unit tests
docker build --target export-test-results --output type=local,dest=results .

# run integration tests
# FRAGILE: ASSUME: postgres and redis are running and postgres db is initialized
#docker run -e ConnectionStrings__voting="..." -e ConnectionStrings__cache="..." -v ./results:/src/results test-integration

# build the containers we want to publish
docker build -t $ACR_URL/aspire-frameworkapi:$IMAGE_LABEL --target frameworkapi .
docker build -t $ACR_URL/aspire-func-voteget:$IMAGE_LABEL --target func-voteget .
docker build -t $ACR_URL/aspire-func-votescore:$IMAGE_LABEL --target func-votescore .

# push the containers
docker push $ACR_URL/aspire-blazor:latest
docker push $ACR_URL/aspire-react:$IMAGE_LABEL
docker push $ACR_URL/aspire-vue:$IMAGE_LABEL
docker push $ACR_URL/aspire-frameworkapi:$IMAGE_LABEL
docker push $ACR_URL/aspire-func-voteget:$IMAGE_LABEL
docker push $ACR_URL/aspire-func-votescore:$IMAGE_LABEL

# Uncomment your chosen deployment strategy:

# run the k8s deployment
#helm template k8s --set imageLabel=$IMAGE_LABEL --set acrUrl=$ACR_URL --set aksUrl=$AKS_URL | kubectl apply -f -

# run the docker compose deployment
#docker compose up -d
