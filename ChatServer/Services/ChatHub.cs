using ChatServer.Shared;
using MagicOnion.Server.Hubs;

namespace ChatServer.Services;

/// <summary>
/// ChatHub
/// </summary>
public class ChatHub : StreamingHubBase<IChatHub,IChatHubReceiver>,IChatHub
{
    /// <summary>
    /// ルーム
    /// </summary>
    private IGroup room;
    
    /// <summary>
    /// user
    /// </summary>
    private User self;
    
    /// <summary>
    /// メモリ
    /// </summary>
    private IInMemoryStorage<User> _storage;

    /// <summary>
    /// 入室
    /// </summary>
    /// <param name="roomName"></param>
    /// <param name="userName"></param>
    /// <param name="comment"></param>
    /// <returns></returns>
    public async Task<User[]> JoinAsync(string roomName, string userName, string comment)
    {
        // 自分のインスタンスを作る
        self = new User()
        {
            UserName = userName,
            Comment = comment
        };
        
        Console.WriteLine($"{self.UserName}が{roomName}に入室しました。");

        // roomNameをキーにしてグループに参加 なかったら作成
        (room, _storage) = await Group.AddAsync(roomName, self);
        
        // 入室したことをルームにいる全員にブロードキャスト
        Broadcast(room).OnJoin(self);

        return _storage.AllValues.ToArray();
    }

    /// <summary>
    /// 退室
    /// </summary>
    public async Task LeaveAsync()
    {
        // roomから自分を削除
        await room.RemoveAsync(this.Context);
        // 退室をブロードキャスト
        Broadcast(room).OnLeave(self);
    }

    /// <summary>
    /// 書き込み
    /// </summary>
    /// <param name="comment"></param>
    public async Task WriteAsync(string comment)
    {
        if (comment != "")
        {
            self.Comment = comment;
            // 書き込みをブロードキャスト
            Broadcast(room).OnWrite(self);
        }
    }

    /// <summary>
    /// 切断
    /// </summary>
    /// <returns></returns>
    protected override ValueTask OnDisconnected()
    {
        return CompletedTask;
    }
}