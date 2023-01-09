FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

# Copy the main source project files
COPY ./*/*.csproj ./
RUN for file in $(ls *.csproj); do mkdir -p ./${file%.*}/ && mv $file ./${file%.*}/; done
RUN dotnet restore "MusicStore.Api.Gateways.Web/MusicStore.Api.Gateways.Web.csproj" --configfile nuget.config
COPY . .
WORKDIR "/src/MusicStore.Api.Gateways.Web"
RUN dotnet build "MusicStore.Api.Gateways.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MusicStore.Api.Gateways.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MusicStore.Api.Gateways.Web.dll"]