# Getting Started

This is a simple pubsub demo API with the following endpoints

1. get/access/uri [Get the websocket access URI for a user]
2. send/to/all [Send to all clients in the hub]
3. send/to/user [Send to specific user and save the message in Cosmos DB]
4. send/to/group [Send to a group of connected clients]
5. add/user/to/group [Add user to a group]
6. add/connection/to/group [Add connection to a group using connection ID]
7. /eventhandler/ [Notify hub on user connect, custom user events can be added as well (Note: POST request only. Also, expose this endpoint with Ngrok while running in localhost)]

## Client App

The client app is a simple console app that takes an access URI on first run and keeps consuming the messages.

## Azure Web PubSub Groups

AddUserToGroup is actually a quick way to add current connections for this user to the group, which means, if a connection for userA connects after the AddUserToGroup(GroupA) call, this new connection is not joined to GroupA automatically. So actually the user can have a situation where some connections for userA belong to groupA while other connections for userA are not. [Read More](https://github.com/Azure/azure-webpubsub/issues/325)

To add a single connection that belongs to a user, use AddConnectionToGroup/Async.