## Setup

1. Configure the terraform remote tfstate file on Azure, README.md in terraform-remote-state folder

2. Initialize terraform with correct backend config to use the remote tfstate file on Azure
    * ```terraform init -backend-config "resource_group_name=$RESOURCE_GROUP_NAME" -backend-config "storage_account_name=$STORAGE_ACCOUNT_NAME" -backend-config "container_name=$CONTAINER_NAME"```

3. Run `terraform plan`
4. Run `terraform apply`