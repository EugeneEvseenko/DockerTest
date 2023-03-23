FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["DockerTest/DockerTest.csproj", "DockerTest/"]
RUN dotnet restore "DockerTest/DockerTest.csproj"
COPY . .
WORKDIR "/src/DockerTest"
RUN dotnet build "DockerTest.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DockerTest.csproj" -c Release -o /app/publish

# We don't need the standalone Chromium
ENV PUPPETEER_SKIP_CHROMIUM_DOWNLOAD true
RUN apt-get update && apt-get install gnupg wget -y
RUN apt-get install -y \
    fonts-liberation \
    gconf-service \
    libappindicator1 \
    libasound2 \
    libatk1.0-0 \
    libcairo2 \
    libcups2 \
    libfontconfig1 \
    libgbm-dev \
    libgdk-pixbuf2.0-0 \
    libgtk-3-0 \
    libicu-dev \
    libjpeg-dev \
    libnspr4 \
    libnss3 \
    libpango-1.0-0 \
    libpangocairo-1.0-0 \
    libpng-dev \
    libx11-6 \
    libx11-xcb1 \
    libxcb1 \
    libxcomposite1 \
    libxcursor1 \
    libxdamage1 \
    libxext6 \
    libxfixes3 \
    libxi6 \
    libxrandr2 \
    libxrender1 \
    libxss1 \
    libxtst6 \
    xdg-utils 
    
RUN wget https://dl.google.com/linux/direct/google-chrome-stable_current_amd64.deb
RUN apt-get install -y ./google-chrome-stable_current_amd64.deb
RUN rm google-chrome-stable_current_amd64.deb 

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DockerTest.dll"]
