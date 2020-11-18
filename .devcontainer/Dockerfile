FROM debian:buster-slim

WORKDIR /tmp

# Install Dependencies
RUN apt-get update \
    && apt-get install -y \
       wget \
       python3-pip \
       apt-transport-https \
       ca-certificates \
       curl \
       gnupg-agent \
       software-properties-common \
    && rm -rf /var/lib/apt/lists/*

# Add Docker's GPG key
RUN curl -fsSL https://download.docker.com/linux/debian/gpg | apt-key add -

# Add Docker's repo
RUN add-apt-repository "deb [arch=amd64] https://download.docker.com/linux/debian $(lsb_release -cs) stable"


# Install Docker
RUN apt-get update
RUN apt-get install -y docker-ce docker-ce-cli containerd.io

# Install Microsoft Package Repositories
RUN wget -q https://packages.microsoft.com/config/ubuntu/19.04/packages-microsoft-prod.deb && \
    dpkg -i packages-microsoft-prod.deb && \
    apt-get update

# Install .NET Core 3.1
RUN apt-get install -y apt-transport-https && \
    apt-get install -y dotnet-sdk-3.1

# Install the Azure Functions Core Tools
RUN apt-get install azure-functions-core-tools-3

# Install IoTEdgeDev
RUN pip3 install --upgrade iotedgehubdev