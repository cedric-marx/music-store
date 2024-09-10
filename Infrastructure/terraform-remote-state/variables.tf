// Naming conventions
variable "type" {
  type        = string
  description = "Type of deployment."
  default     = "tfstate"
}

variable "prefix" {
  type        = string
  description = "Unique prefix of the deployment."
  default     = "ro"
}

variable "instance" {
  type        = string
  description = "Unique instance name of the deployment."
  default     = "ck"
}

variable "name" {
  type        = string
  description = "Unique name of the deployment."
  default     = "metadata"
}

variable "location_code" {
  type        = string
  description = "Naming convention location code."
  default     = "we"
}

variable "location" {
  type        = string
  description = "Location of the Azure resource group."
  default     = "westeurope"
}
