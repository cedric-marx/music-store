resource "azurerm_kubernetes_cluster" "default" {
  name                              = "AKS${var.location_number}${var.name}${var.unique_number}-${var.environment}"
  location                          = azurerm_resource_group.default.location
  resource_group_name               = azurerm_resource_group.default.name
  dns_prefix                        = "${var.name}-aks-${var.environment}"
  kubernetes_version                = var.kubernetes_version
  role_based_access_control_enabled = true

  default_node_pool {
    name                 = "default"
    node_count           = var.node_count
    vm_size              = var.node_type
    os_disk_size_gb      = 30
    vnet_subnet_id       = azurerm_subnet.aks.id
    orchestrator_version = var.kubernetes_version
    max_pods             = var.max_pods
  }

  identity {
    type = "SystemAssigned"
  }

  network_profile {
    # When network_plugin is set to azure - the vnet_subnet_id field in the default_node_pool block must be set and pod_cidr must not be set.
    network_plugin = "azure"

    # `docker_bridge_cidr`, `dns_service_ip` and `service_cidr` should all be empty or all should be set
    docker_bridge_cidr = var.vnet_aks_docker_bridge_cidr
    dns_service_ip     = var.vnet_aks_dns_service_ip
    service_cidr       = var.vnet_aks_service_cidr
  }

  oms_agent {
    log_analytics_workspace_id = azurerm_log_analytics_workspace.default.id
  }
}
