FROM microsoft/dotnet:2.1-runtime AS base
WORKDIR /app
EXPOSE 98

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY Suriging.Identity/Surging.Identity.Server/Surging.Identity.Server.csproj Suriging.Identity/Surging.Identity.Server/
COPY Surging.Core/Surging.Core.CPlatform/Surging.Core.CPlatform.csproj Surging.Core/Surging.Core.CPlatform/
COPY Surging.Core/Surging.Core.ServiceHosting/Surging.Core.ServiceHosting.csproj Surging.Core/Surging.Core.ServiceHosting/
COPY Suriging.Identity/Surging.Identity.ModuleService/Surging.Identity.ModuleService.csproj Suriging.Identity/Surging.Identity.ModuleService/
COPY Suriging.Identity/Surging.Identity.Core/Surging.Identity.Core.csproj Suriging.Identity/Surging.Identity.Core/
COPY Surging.Core/Surging.Core.ProxyGenerator/Surging.Core.ProxyGenerator.csproj Surging.Core/Surging.Core.ProxyGenerator/
COPY Suriging.Identity/Surging.Identity.IModuleService/Surging.Identity.IModuleService.csproj Suriging.Identity/Surging.Identity.IModuleService/
COPY Surging.Core/Surging.Core.System/Surging.Core.System.csproj Surging.Core/Surging.Core.System/
COPY Surging.Core/Surging.Core.Caching/Surging.Core.Caching.csproj Surging.Core/Surging.Core.Caching/
COPY Surging.Core/Surging.Core.Common/Surging.Core.Common.csproj Surging.Core/Surging.Core.Common/
COPY Suriging.Identity/Surging.Identity.Database/Surging.Identity.Database.csproj Suriging.Identity/Surging.Identity.Database/
COPY Surging.Core/Surging.Core.EventBusRabbitMQ/Surging.Core.EventBusRabbitMQ.csproj Surging.Core/Surging.Core.EventBusRabbitMQ/
COPY Surging.Core/Surging.Core.NLog/Surging.Core.Nlog.csproj Surging.Core/Surging.Core.NLog/
COPY Surging.Core/Surging.Core.DotNetty/Surging.Core.DotNetty.csproj Surging.Core/Surging.Core.DotNetty/
COPY Surging.Core/Surging.Core.Codec.MessagePack/Surging.Core.Codec.MessagePack.csproj Surging.Core/Surging.Core.Codec.MessagePack/
COPY Surging.Core/Surging.Core.Consul/Surging.Core.Consul.csproj Surging.Core/Surging.Core.Consul/
RUN dotnet restore Suriging.Identity/Surging.Identity.Server/Surging.Identity.Server.csproj
COPY . .
WORKDIR /src/Suriging.Identity/Surging.Identity.Server
RUN dotnet build Surging.Identity.Server.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish Surging.Identity.Server.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Surging.Identity.Server.dll"]
