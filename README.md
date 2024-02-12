## Chat application using MagicOnion
### Server

    $cd SampleOnionServer
    $dotnet run

### Client

    $cd SampleOnionClient
    $dotnet run --userName=hoge --roomName=hogeRoom

|key|value|required|
|:--|:----|:-------|
|--userName|ユーザー名|○|
|--roomName|ルーム名|-|

* userName is required. Please make sure that the user name to be joined is unique for each room.
* roomName is optional. If there is a room you have entered, you will be entered there. If you do not enter a room name, you will be placed in a common room.

The client side is one user per terminal, so please launch multiple terminals to try it out.

### swagger
Access localhost:5001/swagger/ after server startup