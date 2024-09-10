terraform {
  required_providers {
    azurerm = {
      source  = "hashicorp/azurerm"
      version = ">=3.38.0"
    }

    azuread = {
      source  = "hashicorp/azuread"
      version = ">=2.31.0"
    }

    helm = {
      source  = "hashicorp/helm"
      version = ">=2.8.0"
    }
  }

  backend "azurerm" {
    key                  = "terraform.tfstate"
    resource_group_name  = "<This will be replaced by -backend-config>"
    storage_account_name = "<This will be replaced by -backend-config>"
    container_name       = "<This will be replaced by -backend-config>"
  }
}

provider "azurerm" {
  features {
    resource_group {
      prevent_deletion_if_contains_resources = false
    }
  }
}

resource "azurerm_resource_group" "default" {
  name     = "RG${var.location_number}${var.name}${var.unique_number}-${var.environment}"
  location = var.location
}

provider "azuread" {}

data "azurerm_subscription" "current" {}
data "azuread_client_config" "current" {}
