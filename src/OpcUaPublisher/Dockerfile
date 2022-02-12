FROM mcr.microsoft.com/dotnet/core/runtime:2.2-stretch-slim AS base

ENV PROJ_ROOT=opcua-publisher
ENV PROJ_FILE=Microsoft.Azure.IIoT.Modules.OpcUa.Publisher.csproj

WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ${PROJ_FILE} ${PROJ_ROOT}/

RUN dotnet restore ${PROJ_ROOT}/${PROJ_FILE}
COPY . .
WORKDIR /src/${PROJ_ROOT}
RUN dotnet build ${PROJ_FILE} -c Release -o /app

FROM build AS publish
RUN dotnet publish ${PROJ_FILE} -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .

WORKDIR /appdata
ENTRYPOINT ["dotnet", "/app/opcpublisher.dll"]