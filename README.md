# Music Store
The purpose of this project is to add a complex design project to my portfolio.
## Description
Music Store is a .NET application that includes the following functionality:
* Microservices that have a CRUD repository that communicate with PostgreSQL: Orders and Products.
* Microservices communicate over a bus using RabbitMQ, with the help of the MassTransit NuGet Package.
* Both microservices have their own API with endpoints related to the microservice. The Ocelot API Gateway acts as a proxy to connect these API's together. In live environments, Ocelot uses the Kubernetes provider as a Service Discovery Provider.
* The Web API Gateway is specifically tailored for the web frontend that calls this API. If another frontend needs to be added, we would create another API Gateway with the specific calls required for that frontend.
* A local Docker Compose to make things easier for the development team. Services and applications run in a way that is closer to the production environment than local runs.

## TODO
This project is a showcase of the techniques I have become comfortable with over the years. However, due to the limited time I had to complete this project, some things are missing. Below is a list of important things I would have done if I had more time:
* Expand the repositories to CRUD with the specification pattern. Currently, only the necessary functions have been implemented.
* Configure authentication with Identity Server. This can be a time-consuming task, and I intentionally skipped this part due to the limited time I had. I am aware that authentication is crucial and the application is not production-ready without it. A few steps that would have been taken if we had to implement IDS:
  * Make sure the frontend makes use of tokens and refresh tokens.
  * Configure our Ocelot to make use of authenticated endpoints.
  * Create a microservice for IDS that communicates over the bus with other services.
* Expand the Order to contain multiple products and add fields to the order such as pricing, name, etc. This would enable us to create an invoice for the order and expand the project with a mailing microservice, for example.
## Getting Started

### Dependencies

1. Docker installation: https://docs.docker.com/engine/install/

### Executing program

1. Run the docker-compose project in the solution.

## Authors

* Cedric Marx