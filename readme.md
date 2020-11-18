# Azure Functions to Blog Storage running on IoT Edge

This repo contains a sample project that highlights using an Azure Function to upload a file to Azure Blob Storage running locall on IoT Edge

## Components

#### Azure Function

This project contains a single Azure Function hosted inside a container running on IoT Edge.  This functions project and Edge module definition are available at `modules/EdgeBlobUploadFunction`.  The function leverages an [HTTP Trigger](https://docs.microsoft.com/en-us/azure/azure-functions/functions-bindings-http-webhook-trigger?tabs=csharp) to recieve a file, and uses parameter binding to extract a file name from the path provided.  The function also leverages a [Blob Storage output Binding](https://docs.microsoft.com/en-us/azure/azure-functions/functions-bindings-storage-blob-output?tabs=csharp) to efficiently handle connecting to the local Blob Storage module and copying the file over.


#### Azure Blob Storage

This project also contains a module definition for [Azure Blob Storage on IoT Edge](https://docs.microsoft.com/en-us/azure/iot-edge/how-to-store-data-blob?view=iotedge-2018-06).  This provides a local container running a Blob Storage compatible API and will handle auto-tiering of data up to a connected cloud provider.  This provides a great way to handle accepting files for upload while *offline* and moving them up to the cloud as appropraite.

## Development

This project leverages a [VSCode DevContainer](https://code.visualstudio.com/docs/remote/containers) to provide a full environment for everything needed to build, simulate, and deploy this solution.  No dependencies other than Docker and VSCode are needed to run this project.