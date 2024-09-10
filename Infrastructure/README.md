# Music Store Infrastructure

## TODO
This project is a showcase of the techniques I have become comfortable with over the years. However, due to the limited time I had to complete this project, some things are missing. Below is a list of important things I would have done if I had more time:
* Managing multiple Terraform environments using a tool such as Terragrunt.
* Deploying multiple environments on Azure, most of which is already set up. However, I am limited by the number of Azure credits I can use, so I have not pursued this option.
* Using a Postgres Flexible server instead of the regular Azure Postgres server to make scaling and load balancing easier with an Azure service, but this option was not chosen due to cost and limited credits.
* Using a vault like Keepass to store files, such as secret.tfvars for Terraform sensitive variables. I have not implemented this as it is just an example case.
* Creating a Postgres private endpoint and disabling the public IP of the Postgres server on Azure. Unfortunately, this is not possible with the cheapest option of this resource, so I have commented out the code and disabled it.
* Helmfile deployment with the helmfile-deploy helm chart, which uses the appdeploy and appconduits helm charts to create a k8s deployment. I would have used it in a release pipeline on Azure with the correct environment, but time constraints did not allow for it.
* Test automation, which ensures code is functioning correctly, was not implemented in this project due to time constraints. It takes a lot of time to set up and maintain test infrastructure, write test cases and integrate them into the development process. Since this project is a simple example case, test automation is not a priority but it is highly recommended in a real-world scenario.
## Getting Started

### Dependencies

Check the implementation specific README files

## Authors

* Cedric Marx