FROM microsoft/aspnetcore:2.0 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /src
COPY PortfolioMyriam.sln ./
COPY PortfolioMyriam/PortfolioMyriam.csproj PortfolioMyriam/
RUN dotnet restore -nowarn:msb3202,nu1503
COPY . .
WORKDIR /src/PortfolioMyriam
RUN dotnet build -c Release -o /app

FROM build AS publish
RUN dotnet publish -c Release -o /app -r linux-x64

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENV ASPNETCORE_URLS http://+:5000
ENTRYPOINT ["dotnet", "PortfolioMyriam.dll"]
