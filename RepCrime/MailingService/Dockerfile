#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["MailingService/MailingService.csproj", "MailingService/"]
COPY ["EventBus.Messaging/EventBus.Messaging.csproj", "EventBus.Messaging/"]
COPY ["CommonItems/CommonItems.csproj", "CommonItems/"]
RUN dotnet restore "MailingService/MailingService.csproj"
COPY . .
WORKDIR "/src/MailingService"
RUN dotnet build "MailingService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MailingService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MailingService.dll"]