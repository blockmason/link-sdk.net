FROM debian:stretch

ENV DOTNET_CLI_TELEMETRY_OPTOUT=1

RUN set -o nounset -o errexit;\
  apt-get update;\
  apt-get upgrade -y;\
  apt-get install apt-transport-https curl gnupg zlibc zlib1g zlib1g-dev -y;\
  curl -sSL https://packages.microsoft.com/keys/microsoft.asc | gpg --dearmor > /etc/apt/trusted.gpg.d/microsoft.gpg;\
  curl -sSL https://packages.microsoft.com/config/debian/9/prod.list > /etc/apt/sources.list.d/microsoft.list;\
  chown root:root /etc/apt/trusted.gpg.d/microsoft.gpg /etc/apt/sources.list.d/microsoft.list;\
  chmod 0444 /etc/apt/trusted.gpg.d/microsoft.gpg /etc/apt/sources.list.d/microsoft.list;\
  apt-get update;\
  apt-get install dotnet-sdk-2.2 -y;\
  apt-get autoremove -y;\
  rm -vfR /var/lib/apt/lists/*;\
  addgroup --system --gid 1337 link;\
  adduser --system --gid 1337 --uid 1337 --home /opt/link --gecos '' --shell /bin/bash --disabled-login link;

COPY . /opt/link

RUN set -o nounset -o errexit;\
  chmod -fR ug+rwX,o-rwx /opt/link;\
  chown -fR link:link /opt/link;

USER link

WORKDIR /opt/link

RUN dotnet publish --configuration Release Link/Link.csproj
RUN dotnet publish --configuration Release Link.Tests/Link.Tests.csproj
RUN dotnet test --configuration Release Link.Tests/Link.Tests.csproj
