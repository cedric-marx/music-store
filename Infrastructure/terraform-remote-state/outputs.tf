output "resource_group_name" {
  value       = azurerm_resource_group.tfstate.name
  description = "The name of the resource group containing the storage account."
}

output "storage_account_name" {
  value       = azurerm_storage_account.tfstate.name
  description = "The name of the storage account created."
}

output "storage_container_name" {
  value       = azurerm_storage_container.tfstate.name
  description = "The name of the storage container created."
}
