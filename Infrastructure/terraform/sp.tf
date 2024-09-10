resource "azuread_application" "default" {
  display_name = azurerm_kubernetes_cluster.default.name
  owners       = [data.azuread_client_config.current.object_id]
}

resource "azuread_application_password" "default" {
  application_object_id = azuread_application.default.object_id
}

resource "azuread_service_principal" "default" {
  application_id = azuread_application.default.application_id
  owners         = [data.azuread_client_config.current.object_id]
}

resource "azurerm_role_assignment" "contributor" {
  scope                = data.azurerm_subscription.current.id
  role_definition_name = "Contributor"
  principal_id         = azuread_service_principal.default.object_id
}