# Music Store
Purpose of this project is to add a project to my portfolio with complex design decisions.

## Description

Music Store is a .NET application with following functionality:
* Product microservice that creates and reads products from the product database.
* Order microservice that creates and reads orders from the product database.
* Both microservices have their own API with endpoints related to the microservice, the Ocelot API Gateway acts as a proxy to connect those API's together.
* The Web API Gateway is specifically tailored for the web frontend that calls this API. If another frontend has to be added we create another API Gateway with specific calls needed for the frontend.
* Microservices communicate through bus messages with RabbitMQ.

## Getting Started

### Dependencies

* All you need is Docker

### Executing program

* Run Docker-Compose

## Authors

Cedric Marx