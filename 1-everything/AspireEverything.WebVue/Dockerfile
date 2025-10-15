# build "server" image
FROM node:alpine AS react-build

WORKDIR /src
COPY package.json .
RUN npm install
COPY . .
RUN npm run build


# production "server" image
FROM nginx:alpine AS react

COPY --from=react-build /src/dist /usr/share/nginx/html
COPY nginx.conf /etc/nginx/nginx.conf
EXPOSE 80
# Nginx already has a CMD
