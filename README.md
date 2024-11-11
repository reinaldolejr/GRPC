# gRPC Communication Demo Project

## Overview
This project demonstrates client-server communication using gRPC (Google Remote Procedure Call). The implementation showcases real-time data streaming and remote procedure calls between a client and server.

## Features
- Bidirectional gRPC communication
- Multiple service implementations:
  - Greeter Service
  - Time Service
  - Stock Service
- Protocol Buffers (protobuf) for message serialization

## Technology Stack
- .NET 8.0
- gRPC
- Protocol Buffers
- C#

## Project Structure
- `grpc-server/`: Server-side implementation
  - Service definitions
  - gRPC service implementations
  - Protocol buffer message definitions
## Service Implementation Details

### Greeter Service
- Implements a simple request-response pattern
- Client sends a name in the request
- Server responds with a personalized greeting
- Perfect for testing basic connectivity

### Time Service
- Handles client time requests with unique identifiers
- Returns server timestamp with client ID
- Useful for time synchronization and client tracking
- Demonstrates unary RPC calls

### Stock Service
- Implements server-side streaming for real-time stock updates
- Client subscribes to stock symbols
- Server pushes continuous price updates
- Shows efficient real-time data delivery
- Great example of server streaming capabilities

## Running the Application

1. Start the Server:
   - Navigate to server directory and build
   - Run the gRPC server to start listening
   - Server initializes all services (Greeter, Time, Stock)

2. Launch the Client:
   - Build and run the client application
   - Client connects to server automatically
   - Ready to make service calls

3. Communication Flow:
   - Client initiates requests using generated gRPC stubs
   - Server processes requests through service implementations
   - Responses stream back to client based on service type
   - Maintains persistent connection for streaming services

## Running the Project

### Start the Server
- First, clone and start the gRPC server:
```bash:terminal
git clone https://github.com/reinaldolejr/GRPC.git
```

- in the terminal, navigate to the server directory and build the project:
```
cd grpc-server
```

- build the project:
```
dotnet build
```

- run the project:
```
dotnet run --project GrpcService/GrpcService.csproj 
```

### Start the Client

- In a new terminal, navigate to the client directory and build the project:
```
cd grpc-client
```

- build the project:
```
dotnet build
```

- run the project:
```
dotnet run
```