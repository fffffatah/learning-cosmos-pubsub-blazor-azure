# Getting Started

This is a simple pubsub demo API with the following endpoints
1. send/to/all [Send to all clients in the hub]
2. send/to/user [Send to specific user and save the message in Cosmos DB]
3. send/to/group [Send to a group of connected clients]
4. get/access/uri [Get the websocket access URI for a user]
5. /eventhandler/ [Notify hub on user connect, custom user events can be added as well (Note: POST request only. Also, expose this endpoint with Ngrok while running in localhost)]

## Client App

The client app is a simple console app that takes an access URI on first run and keeps consuming the messages.