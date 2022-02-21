#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["LawEnforcementService/LawEnforcementAPI/LawEnforcementAPI.csproj", "LawEnforcementService/LawEnforcementAPI/"]
COPY ["CrimeAPI/CrimeService.csproj", "CrimeAPI/"]
COPY ["CommonItems/CommonItems.csproj", "CommonItems/"]
COPY ["EventBus.Messaging/EventBus.Messaging.csproj", "EventBus.Messaging/"]
COPY ["LawEnforcementService/LawEnforcement.Infrastructure/LawEnforcement.Infrastructure.csproj", "LawEnforcementService/LawEnforcement.Infrastructure/"]
COPY ["LawEnforcementService/LawEnforcement.Application/LawEnforcement.Application.csproj", "LawEnforcementService/LawEnforcement.Application/"]
COPY ["LawEnforcementService/LawEnforcement.Domain/LawEnforcement.Domain.csproj", "LawEnforcementService/LawEnforcement.Domain/"]
RUN dotnet restore "LawEnforcementService/LawEnforcementAPI/LawEnforcementAPI.csproj"
COPY . .
WORKDIR "/src/LawEnforcementService/LawEnforcementAPI"
RUN dotnet build "LawEnforcementAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LawEnforcementAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LawEnforcementAPI.dll"]