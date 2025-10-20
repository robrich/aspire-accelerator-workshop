# build "server" image
FROM node:alpine AS build

WORKDIR /src
COPY package.json .
RUN npm install
COPY . .
RUN npm run build


# production "server" image
FROM nginx:alpine

WORKDIR /usr/share/nginx/html
EXPOSE 80

COPY --from=build /src/dist .
COPY nginx.conf /etc/nginx/conf.d/default.conf

# Nginx already has a CMD
