FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:729
EXPOSE 729

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY Surging.ApiGateway/Surging.ApiGateway.csproj Surging.ApiGateway/
COPY Surging.Core/Surging.Core.CPlatform/Surging.Core.CPlatform.csproj Surging.Core/Surging.Core.CPlatform/
COPY Surging.Core/Surging.Core.ServiceHosting/Surging.Core.ServiceHosting.csproj Surging.Core/Surging.Core.ServiceHosting/
COPY Surging.Core/Surging.Core.Codec.MessagePack/Surging.Core.Codec.MessagePack.csproj Surging.Core/Surging.Core.Codec.MessagePack/
COPY Surging.Core/Surging.Core.Caching/Surging.Core.Caching.csproj Surging.Core/Surging.Core.Caching/
COPY Surging.Core/Surging.Core.ProxyGenerator/Surging.Core.ProxyGenerator.csproj Surging.Core/Surging.Core.ProxyGenerator/
COPY Surging.Core/Surging.Core.Zookeeper/Surging.Core.Zookeeper.csproj Surging.Core/Surging.Core.Zookeeper/
COPY Surging.Core/Surging.Core.Common/Surging.Core.Common.csproj Surging.Core/Surging.Core.Common/
COPY Surging.Core/Surging.Core.System/Surging.Core.System.csproj Surging.Core/Surging.Core.System/
COPY Surging.Core/Surging.Core.Codec.ProtoBuffer/Surging.Core.Codec.ProtoBuffer.csproj Surging.Core/Surging.Core.Codec.ProtoBuffer/
COPY Surging.Core/Surging.Core.ApiGateWay/Surging.Core.ApiGateWay.csproj Surging.Core/Surging.Core.ApiGateWay/
COPY Surging.Core/Surging.Core.DotNetty/Surging.Core.DotNetty.csproj Surging.Core/Surging.Core.DotNetty/
COPY Surging.Core/Surging.Core.Consul/Surging.Core.Consul.csproj Surging.Core/Surging.Core.Consul/
RUN dotnet restore Surging.ApiGateway/Surging.ApiGateway.csproj
COPY . .
WORKDIR /src/Surging.ApiGateway
RUN dotnet build Surging.ApiGateway.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish Surging.ApiGateway.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Surging.ApiGateway.dll"]
