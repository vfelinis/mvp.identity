#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
#COPY *.sln .
#COPY src/mvp.identity/*.csproj ./mvp.identity/
#RUN dotnet restore
#COPY src/mvp.identity/. ./mvp.identity/
#WORKDIR /dist/mvp.identity
#RUN dotnet publish -c Release -o out /app/publish

COPY ["src/mvp.identity/mvp.identity.csproj", "src/mvp.identity/"]
RUN dotnet restore "src/mvp.identity/mvp.identity.csproj"
COPY . .
WORKDIR "/src/src/mvp.identity"
RUN dotnet build "mvp.identity.csproj" -c Release -o /app/build
FROM build AS publish
RUN dotnet publish "mvp.identity.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "mvp.identity.dll"]