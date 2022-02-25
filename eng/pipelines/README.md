# PIPELINE TEMPLATEs

## Based on brewtech pipelines

This pipeline purpose is provide code analysis and development/QA environments delivery.
Whenever you open a Pull Request to develop ou release/* branches, the pipeline will:

- Run Dependency Track analysis
- Run SonarQube analysis (with test running)
- Run Docker image build (without pull)

When you merge code at develop and release/* a new code validation will start and if it be successful, then a delivery process will be started, pulling e deploying a new docker imagem/deployment file at k8s.

## Variáveis

### templates/sonar-analysis.yml

- Sonar.ServiceConnection -> *(CodeQuality)*
- HexaEmployee.Sonar.ProjectKey -> *(HexaEmployee-CodeQuality)*
- HexaEmployee.Sonar.ProjectName -> *(HexaEmployee-CodeQuality)*
- HexaEmployee.Sonar.Duplication.Exclusions -> *(HexaEmployee-CodeQuality)*
- HexaEmployee.Sonar.Coverage.Exclusions -> *(HexaEmployee-CodeQuality)*

### templates/csharp-build.yaml

- HexaEmployee.Build.NugetPath -> *(HexaEmployee-CodeQuality)*

### templates/docker.yml

- HexaEmployee.Docker.ServiceConnection -> *(HexaEmployee-NonProd-Docker)*
- HexaEmployee.Docker.RepositoryName -> *(HexaEmployee-NonProd-Docker)*
- HexaEmployee.Docker.ImageTag -> *(HexaEmployee-NonProd-Docker)*

### templates/dtrack.yml

- DTrack.Url -> *(CodeQuality)*
- HexaEmployee.DTrack.ProjectKey -> *(HexaEmployee-CodeQuality)*
- HexaEmployee.DTrack.SolutionFileName -> *(HexaEmployee-CodeQuality)*
- HexaEmployee.DTrack.ApiKey -> *(HexaEmployee-CodeQuality)*

### templates/deploy.yml

- HexaEmployee.DeployFiles.Path -> *(HexaEmployee-NonProd-Deploy)*
- HexaEmployee.kube_azureContainerRegistry -> *(HexaEmployee-NonProd-Deploy)*
- HexaEmployee.kube_ServiceEndpoint -> *(HexaEmployee-NonProd-Deploy)*
- HexaEmployee.kube_namespace -> *(HexaEmployee-NonProd-Deploy)*
- HexaEmployee.kube_deploy_file -> *(HexaEmployee-NonProd-Deploy)*
- HexaEmployee.kube_deploy -> *(HexaEmployee-NonProd-Deploy)*
