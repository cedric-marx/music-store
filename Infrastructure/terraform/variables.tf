// Naming conventions
variable "name" {
  type        = string
  description = "Name of the deployment."
  default     = "rsms"
}

variable "location_number" {
  type        = string
  description = "Naming convention location number."
  default     = "20"
}

variable "unique_number" {
  type        = string
  description = "Naming convention unique number."
  default     = "100"
}

variable "environment" {
  type        = string
  description = "Name of the environment"
  default     = "prod"
}

// Resource information
variable "location" {
  type        = string
  description = "Deployment location."
  default     = "westeurope"
}

variable "secondary_location" {
  type        = string
  description = "Secondary deployment location for (automatic) failover and multi region setup."
  default     = "northeurope"
}

// Node type information
variable "node_count" {
  type        = string
  description = "The number of K8S nodes to provision."
  default     = 1
}

variable "node_type" {
  type        = string
  description = "The size of each node."
  default     = "Standard_B2s"
}

variable "kubernetes_version" {
  type        = string
  description = "Version of Kubernetes specified when creating the AKS managed cluster. If not specified, the latest recommended version will be used at provisioning time (but won't auto-upgrade)."
  default     = "1.24.6"
}

variable "max_pods" {
  type        = string
  description = "The maximum number of pods that can run on each agent. Changing this forces a new resource to be created"
  default     = "60"
}

// Network information
// 10.151.0.0/16 is 10.151.0.0-10.151.255.254 for the entire vNET

// 10.151.0.0/17 is 10.151.0.0-10.151.127.254 for the AKS cluster (because the network plugin type is Azure, this includes the pod_cidr)

// 10.151.128.0/17 is 10.151.128.0-10.151.255.254 for everything outside the cluster

// 10.151.128.0/18 is 10.151.128.0-10.151.191.254 for the service_cidr
// 10.151.192.0/24 is for the ingress
// 10.151.193.0/24 is for the gateway
// 10.151.194.0/24 is for postgres

// 10.151.195.0 - 10.151.255.254 is available

// 172.17.0.1/16 is for the Docker bridge, which is isolated to each node

variable "vnet_address_space" {
  type        = string
  description = "Address space for the vnet"
  default     = "10.151.0.0/16"
}

variable "vnet_aks_subnet_space" {
  type        = string
  description = "Address space for the AKS subnet"
  default     = "10.151.0.0/17"
}

variable "vnet_aks_service_cidr" {
  type        = string
  description = "The Network Range used by the Kubernetes service. Changing this forces a new resource to be created. This range should not be used by any network element on or connected to this VNet. Service address CIDR must be smaller than /12."
  default     = "10.151.128.0/18"
}
variable "vnet_aks_dns_service_ip" {
  type        = string
  description = "IP address within the Kubernetes service address range that will be used by cluster service discovery (kube-dns). Changing this forces a new resource to be created."
  default     = "10.151.128.10"
}

variable "vnet_ingress_subnet_space" {
  type        = string
  description = "Address space for the ingress subnet"
  default     = "10.151.192.0/24"
}

variable "ingress_load_balancer_ip" {
  type        = string
  description = "Address for the ingress controller load balancer"
  default     = "10.151.192.10"
}

variable "vnet_gateway_subnet_space" {
  type        = string
  description = "Address space for the gateway subnet"
  default     = "10.151.193.0/24"
}

variable "gateway_instance_count" {
  type        = string
  description = "The number of application gateways to deploy"
  default     = "1"
}

variable "vnet_postgres_subnet_space" {
  type        = string
  description = "Address space for the postgres subnet"
  default     = "10.151.194.0/24"
}

variable "vnet_aks_docker_bridge_cidr" {
  type        = string
  description = "IP address (in CIDR notation) used as the Docker bridge IP address on nodes. Changing this forces a new resource to be created."
  default     = "172.17.0.1/16"
}

// Postgres
variable "postgres_username" {
  type        = string
  description = "Username for the PostgreSQL server"
  default     = "postgres"
}

variable "postgres_password" {
  type        = string
  description = "Password for the PostgreSQL server"
  default     = "IWannaBeARockstar123@!?"
  sensitive   = false // Should be set to true if this was a real production application, default value shouldn't be the real value.
}