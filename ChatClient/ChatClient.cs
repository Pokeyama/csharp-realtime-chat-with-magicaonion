using Grpc.Net.Client;

namespace ChatClient;

/// <summary>
/// ChatClient
/// </summary>
public class ChatClient
{
    /// <summary>
    /// ユーザー名
    /// </summary>
    private string? _userName { get; set; }

    /// <summary>
    /// ルーム名
    /// </summary>
    private string? _roomName { get; set; }
    
    /// <summary>
    /// gRPCチャンネル作成
    /// </summary>
    private readonly GrpcChannel _channel = GrpcChannel.ForAddress("http://localhost:5000");

    /// <summary>
    /// クライアントの基盤
    /// </summary>
    private readonly ChatHubClient _client = new ChatHubClient();

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="userName"></param>
    /// <param name="roomName"></param>
    public ChatClient(string? userName, string? roomName)
    {
        _userName = userName;
        _roomName = roomName;
        // _roomName = "sampleRoom";
    }

    /// <summary>
    /// 入室
    /// </summary>
    public async Task Start()
    {
        await this._client.ConnectAsync(this._channel, this._roomName, this._userName);
    }

    /// <summary>
    /// チャット投稿
    /// </summary>
    /// <param name="comment"></param>
    public async Task Write(string comment)
    {
        await _client.WriteAsync(comment);
    }

    /// <summary>
    /// 色々解放
    /// </summary>
    public async Task OnDestroy()
    {
        await this._client.LeaveAsync();
        await this._client.DisposeAsync();
        await this._channel.ShutdownAsync();
    }
}