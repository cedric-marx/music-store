resource "azurerm_postgresql_server" "default" {
  name                             = "${var.location_number}${var.name}${var.unique_number}-${var.environment}-postgres"
  location                         = azurerm_resource_group.default.location
  resource_group_name              = azurerm_resource_group.default.name
  sku_name                         = "B_Gen5_1"
  storage_mb                       = 5120
  backup_retention_days            = 7
  geo_redundant_backup_enabled     = false
  auto_grow_enabled                = false
  administrator_login              = var.postgres_username
  administrator_login_password     = var.postgres_password
  version                          = "11"
  ssl_enforcement_enabled          = false
  ssl_minimal_tls_version_enforced = "TLSEnforcementDisabled"
  # public_network_access_enabled = false # Cheapest option sadly does not support this option for this example.
}

resource "azurerm_postgresql_firewall_rule" "azure" {
  name                = "azure"
  resource_group_name = azurerm_resource_group.default.name
  server_name         = azurerm_postgresql_server.default.name
  start_ip_address    = "0.0.0.0"
  end_ip_address      = "255.255.255.255"
}

resource "azurerm_postgresql_database" "products" {
  name                = "DB${var.location_number}${var.name}${var.unique_number}-${var.environment}-products"
  resource_group_name = azurerm_resource_group.default.name
  server_name         = azurerm_postgresql_server.default.name
  collation           = "en_US.UTF8"
  charset             = "UTF8"
}

resource "azurerm_postgresql_database" "orders" {
  name                = "DB${var.location_number}${var.name}${var.unique_number}-${var.environment}-orders"
  resource_group_name = azurerm_resource_group.default.name
  server_name         = azurerm_postgresql_server.default.name
  collation           = "en_US.UTF8"
  charset             = "UTF8"
}
