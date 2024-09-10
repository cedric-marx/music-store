# Virtual Network to deploy resources into
resource "azurerm_virtual_network" "default" {
  name                = "VN${var.location_number}${var.name}${var.unique_number}-${var.environment}"
  location            = azurerm_resource_group.default.location
  resource_group_name = azurerm_resource_group.default.name
  address_space       = [var.vnet_address_space]
}

# Subnets
resource "azurerm_subnet" "aks" {
  name                 = "SN${var.location_number}${var.name}${var.unique_number}-${var.environment}-aks"
  resource_group_name  = azurerm_resource_group.default.name
  address_prefixes     = [var.vnet_aks_subnet_space]
  virtual_network_name = azurerm_virtual_network.default.name
}

resource "azurerm_subnet" "ingress" {
  name                 = "SN${var.location_number}${var.name}${var.unique_number}-${var.environment}-ingress"
  resource_group_name  = azurerm_resource_group.default.name
  virtual_network_name = azurerm_virtual_network.default.name
  address_prefixes     = [var.vnet_ingress_subnet_space]
}

resource "azurerm_subnet" "gateway" {
  name                 = "SN${var.location_number}${var.name}${var.unique_number}-${var.environment}-gateway"
  resource_group_name  = azurerm_resource_group.default.name
  virtual_network_name = azurerm_virtual_network.default.name
  address_prefixes     = [var.vnet_gateway_subnet_space]
}

resource "azurerm_subnet" "postgres" {
  name                 = "SN${var.location_number}${var.name}${var.unique_number}-${var.environment}-postgres"
  resource_group_name  = azurerm_resource_group.default.name
  virtual_network_name = azurerm_virtual_network.default.name
  address_prefixes     = [var.vnet_postgres_subnet_space]
  # private_endpoint_network_policies_enabled = true - NOT SUPPORTED IN CHEAPEST POSTGRES OPTION
}

# Network security groups
resource "azurerm_network_security_group" "aks" {
  name                = "NG${var.location_number}${var.name}${var.unique_number}-aks"
  location            = azurerm_resource_group.default.location
  resource_group_name = azurerm_resource_group.default.name
}

resource "azurerm_network_security_group" "ingress" {
  name                = "NG${var.location_number}${var.name}${var.unique_number}-ingress"
  location            = azurerm_resource_group.default.location
  resource_group_name = azurerm_resource_group.default.name
}

resource "azurerm_network_security_group" "postgres" {
  name                = "NG${var.location_number}${var.name}${var.unique_number}-postgres"
  location            = azurerm_resource_group.default.location
  resource_group_name = azurerm_resource_group.default.name
}

resource "azurerm_network_security_group" "gateway" {
  name                = "NG${var.location_number}${var.name}${var.unique_number}-gateway"
  location            = azurerm_resource_group.default.location
  resource_group_name = azurerm_resource_group.default.name

  security_rule {
    name                       = "NR${var.location_number}${var.name}${var.unique_number}-gateway-inbound-sr"
    description                = "This rule is needed for application gateway probes to work"
    protocol                   = "*"
    source_port_range          = "*"
    destination_port_range     = "65200-65535"
    source_address_prefix      = "*"
    destination_address_prefix = "*"
    access                     = "Allow"
    priority                   = 100
    direction                  = "Inbound"
  }

  security_rule {
    name                       = "NR${var.location_number}${var.name}${var.unique_number}-gateway-inbound-http"
    description                = "This rule is needed for public http access to the gateway"
    protocol                   = "Tcp"
    source_port_range          = "*"
    destination_port_range     = "80"
    source_address_prefix      = "*"
    destination_address_prefix = var.vnet_gateway_subnet_space
    access                     = "Allow"
    priority                   = 110
    direction                  = "Inbound"
  }

  security_rule {
    name                       = "NR${var.location_number}${var.name}${var.unique_number}-gateway-inbound-https"
    description                = "This rule is needed for public https access to the gateway"
    protocol                   = "Tcp"
    source_port_range          = "*"
    destination_port_range     = "443"
    source_address_prefix      = "*"
    destination_address_prefix = var.vnet_gateway_subnet_space
    access                     = "Allow"
    priority                   = 120
    direction                  = "Inbound"
  }
}

# Network security group associations
resource "azurerm_subnet_network_security_group_association" "aks" {
  subnet_id                 = azurerm_subnet.aks.id
  network_security_group_id = azurerm_network_security_group.aks.id
}

resource "azurerm_subnet_network_security_group_association" "ingress" {
  subnet_id                 = azurerm_subnet.ingress.id
  network_security_group_id = azurerm_network_security_group.ingress.id
}

resource "azurerm_subnet_network_security_group_association" "gateway" {
  subnet_id                 = azurerm_subnet.gateway.id
  network_security_group_id = azurerm_network_security_group.gateway.id
}

resource "azurerm_subnet_network_security_group_association" "postgres" {
  subnet_id                 = azurerm_subnet.postgres.id
  network_security_group_id = azurerm_network_security_group.postgres.id
}

# NO SUPPORT FOR PRIVATE DATABASES IN CHEAPEST POSTGRES OPTION
# resource "azurerm_private_dns_zone" "postgres" {
#   name                = "${var.location_number}${var.name}${var.unique_number}-postgres-pdz.postgres.database.azure.com"
#   resource_group_name = azurerm_resource_group.default.name
#   depends_on          = [azurerm_subnet_network_security_group_association.postgres]
# }

# resource "azurerm_private_dns_zone_virtual_network_link" "postgres" {
#   name                  = "${var.location_number}${var.name}${var.unique_number}-pdzvnetlink.com"
#   private_dns_zone_name = azurerm_private_dns_zone.postgres.name
#   virtual_network_id    = azurerm_virtual_network.default.id
#   resource_group_name   = azurerm_resource_group.default.name
# }

# resource "azurerm_private_endpoint" "pgdb_primary" {
#   name                = "${var.location_number}${var.name}${var.unique_number}-postgres-private-endpoint"
#   location            = azurerm_resource_group.default.location
#   resource_group_name = azurerm_resource_group.default.name
#   subnet_id           = azurerm_subnet.postgres.id

#   private_dns_zone_group {
#     name                 = "${var.location_number}${var.name}${var.unique_number}-postgres-private-db"
#     private_dns_zone_ids = [azurerm_private_dns_zone.postgres.id]
#   }

#   private_service_connection {
#     name                           = "${var.location_number}${var.name}${var.unique_number}-postgres-psconn"
#     private_connection_resource_id = azurerm_postgresql_server.default.id
#     subresource_names              = ["postgresqlServer"]
#     is_manual_connection           = false
#   }
# }

locals {
  domain_name_label              = "${var.name}-${var.environment}"
  gateway_name                   = "GW${var.location_number}${var.name}${var.unique_number}-${var.environment}"
  gateway_ip_name                = "GWIP${var.location_number}${var.name}${var.unique_number}-${var.environment}"
  gateway_ip_config_name         = "${var.name}-gateway-ipconfig"
  frontend_port_name             = "${var.name}-gateway-feport"
  frontend_ip_configuration_name = "${var.name}-gateway-feip"
  backend_address_pool_name      = "${var.name}-gateway-bepool"
  http_setting_name              = "${var.name}-gateway-http"
  listener_name                  = "${var.name}-gateway-lstn"
  ssl_name                       = "${var.name}-gateway-ssl"
  url_path_map_name              = "${var.name}-gateway-urlpath"
  url_path_map_rule_name         = "${var.name}-gateway-urlrule"
  request_routing_rule_name      = "${var.name}-gateway-router"
}

resource "azurerm_public_ip" "gateway" {
  name                = "${local.gateway_ip_name}"
  resource_group_name = azurerm_resource_group.default.name
  location            = azurerm_resource_group.default.location
  domain_name_label   = "${local.domain_name_label}"
  allocation_method   = "Static"
  sku                 = "Standard"
}

resource "azurerm_application_gateway" "gateway" {
  name                = local.gateway_name
  resource_group_name = azurerm_resource_group.default.name
  location            = azurerm_resource_group.default.location

  sku {
    name     = "WAF_v2"
    tier     = "WAF_v2"
    capacity = var.gateway_instance_count
  }

  waf_configuration {
    enabled          = true
    firewall_mode    = "Detection"
    rule_set_version = "3.1"
  }

  gateway_ip_configuration {
    name      = local.gateway_ip_config_name
    subnet_id = azurerm_subnet.gateway.id
  }

  frontend_port {
    name = "${local.frontend_port_name}-http"
    port = 80
  }

  frontend_port {
    name = "${local.frontend_port_name}-https"
    port = 443
  }

  frontend_ip_configuration {
    name                 = "${local.frontend_ip_configuration_name}"
    public_ip_address_id = azurerm_public_ip.gateway.id
  }

  backend_address_pool {
    name         = local.backend_address_pool_name
    ip_addresses = [var.ingress_load_balancer_ip]
  }

  backend_http_settings {
    name                  = local.http_setting_name
    cookie_based_affinity = "Disabled"
    port                  = 80
    protocol              = "Http"
    request_timeout       = 1
  }

  http_listener {
    name                           = "${local.listener_name}-http"
    frontend_ip_configuration_name = local.frontend_ip_configuration_name
    frontend_port_name             = "${local.frontend_port_name}-http"
    protocol                       = "Http"
  }

  request_routing_rule {
    name               = "${local.request_routing_rule_name}-http"
    rule_type          = "PathBasedRouting"
    http_listener_name = "${local.listener_name}-http"
    url_path_map_name  = local.url_path_map_name
    priority           = 100
  }

  url_path_map {
    name                               = local.url_path_map_name
    default_backend_address_pool_name  = local.backend_address_pool_name
    default_backend_http_settings_name = local.http_setting_name

    path_rule {
      name                       = local.url_path_map_rule_name
      backend_address_pool_name  = local.backend_address_pool_name
      backend_http_settings_name = local.http_setting_name
      paths = [
        "/*"
      ]
    }
  }
}