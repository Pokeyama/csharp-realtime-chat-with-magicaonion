using MagicOnion;
using MessagePack;

namespace ChatServer.Shared;

/// <summary>
/// サーバーが叩かれたときにブロードキャストするメソッドを定義する
/// </summary>
public interface IChatHubReceiver
{
    void OnJoin(User user);
    void OnLeave(User user);
    void OnWrite(User user);
}

/// <summary>
/// クライアントが実行するサーバー側のメソッドを定義
/// </summary>
public interface IChatHub : IStreamingHub<IChatHub, IChatHubReceiver>
{
    Task<User[]> JoinAsync(string roomName, string userName, string comment);
    Task LeaveAsync();
    Task WriteAsync(string comment);
}

/// <summary>
/// User
/// </summary>
[MessagePackObject]
public class User
{
    [Key(0)]
    public string UserName { get; set; }
    
    [Key(1)]
    public string Comment { get; set; }
}