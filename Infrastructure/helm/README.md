# Prerequisites
1. [Docker](https://www.docker.com/), container runtime.
2. [Helm](https://github.com/helm/helm/releases), Kubernetes package manager (through Helm Charts).
3. [Helmfile](https://github.com/roboll/helmfile), Helm chart deployment management.
4. [Kubectl](https://github.com/kubernetes/kubectl/releases), Kubernetes command-line tool.
5. [Kubectx](https://github.com/ahmetb/kubectx/releases), Kubernetes context checker and switcher.

# Deploying
Add/upgrade all components to/in the cluster.
```shell
helmfile -f helmfile.yaml sync
```

# Helpful commands
```shell
# View all deployed Helm charts on a Kubernetes cluster.
helm list
# Get all current deployments on a Kubernetes cluster.
kubectl get deployments
# List all images in an Azure Container Registry.
az acr repository list --name $REGISTRY_NAME --output table
```
