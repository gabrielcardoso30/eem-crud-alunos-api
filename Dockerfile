FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

ENV ASPNETCORE_ENVIRONMENT=Production
ENV TZ=America/Bahia
RUN ln -snf /usr/share/zoneinfo/$TZ /etc/localtime && echo $TZ > /etc/timezone

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["src/Web/", "Web/"]
COPY ["src/Core/", "Core/"]
COPY ["src/Infra/", "Infra/"]
RUN dotnet restore "Web/Web.csproj"
COPY . .
WORKDIR "/src/Web"
RUN dotnet build Web.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish "Web.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .

RUN apt-get update
RUN apt-get install wget libgdiplus -y
RUN wget -P /app https://github.com/rdvojmoc/DinkToPdf/raw/master/v0.12.4/64%20bit/libwkhtmltox.dll
RUN wget -P /app https://github.com/rdvojmoc/DinkToPdf/raw/master/v0.12.4/64%20bit/libwkhtmltox.dylib
RUN wget -P /app https://github.com/rdvojmoc/DinkToPdf/raw/master/v0.12.4/64%20bit/libwkhtmltox.so

ENTRYPOINT ["dotnet", "Web.dll"]