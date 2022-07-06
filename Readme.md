# TokenGeneratorAPI

## Starting
This sample contains a API able to generate a Token and use it to validate access to others APIs.

## Swagger
The TokenGeneratorAPI project includes a Swagger to allow you launch requests and see the API documentation.

## Postman collection
Import the Postman if you prefer use this tools.

## DockerFile
The project also include a Dockerfile if you have Docker installed in your system you can create a continer using

```
docker build -t token-gen-api .
```

```
docker run -d -p 5001:443 -p 5000:80 --env "ASPNETCORE_ENVIRONMENT=Development" --env "ASPNETCORE_URLS=https://+:443;http://+:80" --name token-gen-api token-gen-api
```