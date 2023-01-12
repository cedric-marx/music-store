FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

# Copy the main source project files
COPY ./*/*.csproj ./
RUN for file in $(ls *.csproj); do mkdir -p ./${file%.*}/ && mv $file ./${file%.*}/; done
RUN dotnet restore "MusicStore.Microservices.Orders.Api/MusicStore.Microservices.Orders.Api.csproj"
COPY . .
WORKDIR "/src/MusicStore.Microservices.Orders.Api"
RUN dotnet build "MusicStore.Microservices.Orders.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MusicStore.Microservices.Orders.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MusicStore.Microservices.Orders.Api.dll"]