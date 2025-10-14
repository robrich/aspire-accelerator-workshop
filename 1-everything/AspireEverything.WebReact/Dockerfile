# build "server" image
FROM node:alpine AS react-build

WORKDIR /src
COPY AspireEverything.WebReact/package.json ./
RUN npm install
COPY AspireEverything.WebReact/ ./
RUN npm run build


# production "server" image
FROM nginx:alpine AS react

COPY --from=react-build /src/dist /usr/share/nginx/html
COPY AspireEverything.WebReact/nginx.conf /etc/nginx/nginx.conf
EXPOSE 80
# Nginx already has a CMD
