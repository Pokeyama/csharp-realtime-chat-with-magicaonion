using ChatClient;

// エントリーポイント
// 引数解析
var arg = new ChatArg(args);
if (arg.CheckArg() != 0)
{
    Console.WriteLine("引数が不正です");
    return 1;
}

// ソケットの作成
var hubClient = new ChatClient.ChatClient(arg.userName,arg.roomName);

// 入室
await hubClient.Start();

// exitと入力されるまでループ
while (true)
{
    await Task.Delay(1000);
    var str = "";
    if (Console.KeyAvailable)
    {
        str = Console.ReadLine() ?? "";
        // exitと入力されたらすべて解放
        if (str is "exit")
        {
            // メモリ解放 切断
            await hubClient.OnDestroy();
            break;
        }
    }
    // チャットをルーム内にブロードキャスト
    await hubClient.Write(str);
}

return 0;

