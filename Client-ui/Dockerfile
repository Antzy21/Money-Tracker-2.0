FROM node:22 AS build
WORKDIR /src
EXPOSE 5173

COPY . .

RUN npm install

FROM build AS final
CMD ["npm", "run", "dev-docker"]