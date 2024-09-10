provider "azurerm" {
  features {}
}

resource "azurerm_resource_group" "tfstate" {
  // rswems-tfstate
  name     = "${var.prefix}${var.location_code}${var.instance}-${var.type}"
  location = var.location
}

resource "azurerm_storage_account" "tfstate" {
  // rswemstfstate
  name                            = "${var.prefix}${var.location_code}${var.instance}${var.type}"
  resource_group_name             = azurerm_resource_group.tfstate.name
  location                        = azurerm_resource_group.tfstate.location
  account_tier                    = "Standard"
  account_replication_type        = "LRS"
  allow_nested_items_to_be_public = true
}

resource "azurerm_storage_container" "tfstate" {
  // metadata-tfstate
  name                  = "${var.name}-${var.type}"
  storage_account_name  = azurerm_storage_account.tfstate.name
  container_access_type = "blob"
}
