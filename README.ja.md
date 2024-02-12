## MagicOnionを使ったリアルタイムチャット
### 使い方

### サーバー側の起動

    $cd SampleOnionServer
    $dotnet run

### クライアントの起動

    $cd SampleOnionClient
    $dotnet run --userName=hoge --roomName=hogeRoom

|key|value|required|
|:--|:----|:-------|
|--userName|ユーザー名|○|
|--roomName|ルーム名|-|

* userNameは必須です。参加するユーザー名がルームごとに一意になるようにしてください。
* roomNameは任意です。入力したルームがあればそこへ入れます。未入力の場合は共通のルームに入ります。

クライアント側は1ターミナルごとに1ユーザーなので、複数ターミナルを立ち上げて試してください。

### swagger
サーバー立ち上げ後にlocalhost:5001/swagger/にアクセス
