param (
  [string]$solution = "HexaEmployee.sln"
)

$outfile = ".\eng\docker\dockerfile"

# This script creates the $outfile file, with Dockerfile commands.
# To increase build speed by optimizing the use of docker build images cache.
# This script is only needed when adding or removing projects from the solution.
Set-Content -Path $outfile "FROM mcr.microsoft.com/dotnet/sdk:5.0-alpine AS dotnet_restore"
Add-Content -Path $outfile "WORKDIR /src"
Add-Content -Path $outfile "COPY ""$solution"" ""$solution""" 
Add-Content -Path $outfile "COPY NuGet.Config NuGet.Config"
Select-String -Path $solution -Pattern ', "(.*?\.csproj)"' | ForEach-Object { $_.Matches.Groups[1].Value.Replace("\", "/") } | Sort-Object | ForEach-Object { "COPY ""$_"" ""$_""" } | Out-File -FilePath $outfile -Append
Select-String -Path $solution -Pattern ', "(.*?\.dcproj)"' | ForEach-Object { $_.Matches.Groups[1].Value.Replace("\", "/") } | Sort-Object | ForEach-Object { "COPY ""$_"" ""$_""" } | Out-File -FilePath $outfile -Append
Add-Content -Path $outfile "RUN dotnet restore ""$solution"""
Add-Content -Path $outfile ""
Add-Content -Path $outfile "FROM dotnet_restore AS dotnet_publish"
Add-Content -Path $outfile "WORKDIR /src"
Add-Content -Path $outfile "COPY . ."
Add-Content -Path $outfile "RUN dotnet publish ""$solution"" -c Release -o /app"
Add-Content -Path $outfile ""
Add-Content -Path $outfile "FROM mcr.microsoft.com/dotnet/aspnet:5.0-alpine AS runtime"
Add-Content -Path $outfile "RUN apt update"
Add-Content -Path $outfile "RUN apt install curl -y"
Add-Content -Path $outfile "WORKDIR /app"
Add-Content -Path $outfile "COPY --from=dotnet_publish /app ."
Add-Content -Path $outfile "RUN mkdir -p /opt/datadog"
Add-Content -Path $outfile "RUN curl -L https://github.com/DataDog/dd-trace-dotnet/releases/download/v1.25.0/datadog-dotnet-apm-1.25.0.tar.gz \"
Add-Content -Path $outfile "    | tar xzf - -C /opt/datadog"
Add-Content -Path $outfile "EXPOSE 80"
Add-Content -Path $outfile "EXPOSE 443"
Add-Content -Path $outfile "ENTRYPOINT [""dotnet"", ""HexaEmployee.Api.dll""]"

# Get-Content $outfile