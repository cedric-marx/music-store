# Store Terraform state in Azure Storage

Steps on how to save and share tfstate on Azure with Terraform.

## Setup

1. Install the Azure CLI

**Windows using Powershell (Administrator)**

```shell
$ProgressPreference = 'SilentlyContinue'; Invoke-WebRequest -Uri https://aka.ms/installazurecliwindows -OutFile .\AzureCLI.msi; Start-Process msiexec.exe -Wait -ArgumentList '/I AzureCLI.msi /quiet'; rm .\AzureCLI.msi
```

Restart Powershell after installation.

**Linux**

```shell
curl -sL https://aka.ms/InstallAzureCLIDeb | sudo bash
```

2. Login using the Azure CLI

```shell
az login
```

3. Verify your current account.

```shell
az account show
```

If it doesn't match the directory you want, use:

```shell
az account list
```

Select the correct one by using:

```shell
az account set --subscription $SUBSCRIPTION_ID
```

4. Use Terraform to add the storage account to your Azure subscription.

```shell
terraform init
terraform plan
terraform apply
```

5. Retrieve the account key.

```shell
export ARM_ACCESS_KEY=$(az storage account keys list --resource-group $RESOURCE_GROUP_NAME --account-name $STORAGE_ACCOUNT_NAME --query '[0].value' -o tsv)
```

For example:

```shell
export ARM_ACCESS_KEY=$(az storage account keys list --resource-group amcpweep-tfstate --account-name amcpweeptfstate --query '[0].value' -o tsv)
```
