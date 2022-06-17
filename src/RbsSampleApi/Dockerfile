FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY ["RbsSampleApi/src/RbsSampleApi/RbsSampleApi.csproj", "./RbsSampleApi/src/RbsSampleApi/"]
COPY ["SharedKernel/SharedKernel.csproj", "./SharedKernel/"]
RUN dotnet restore "./RbsSampleApi/src/RbsSampleApi/RbsSampleApi.csproj"

# Copy everything else and build
COPY . ./
RUN dotnet build "RbsSampleApi/src/RbsSampleApi/RbsSampleApi.csproj" -c Release -o /app/build

FROM build-env AS publish
RUN dotnet publish "RbsSampleApi/src/RbsSampleApi/RbsSampleApi.csproj" -c Release -o /app/out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=publish /app/out .

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "/app/RbsSampleApi.dll"]
