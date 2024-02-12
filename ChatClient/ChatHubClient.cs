using Grpc.Core;
using MagicOnion.Client;
using ChatClient.Shared;

namespace ChatClient;

/// <summary>
/// ChatHubClient
/// クライアント側の実装
/// </summary>
public class ChatHubClient : IChatHubReceiver
{
    /// <summary>
    /// Userのリスト
    /// </summary>
    private List<User> _users = new List<User>();

    /// <summary>
    /// サーバー側メソッド
    /// </summary>
    private IChatHub client;

    /// <summary>
    /// 入室
    /// </summary>
    /// <param name="grpcChannel"></param>
    /// <param name="roomName"></param>
    /// <param name="userName"></param>
    public async Task ConnectAsync(ChannelBase grpcChannel, string? roomName, string? userName)
    {
        // サーバーとの通信を作る
        this.client = await StreamingHubClient.ConnectAsync<IChatHub, IChatHubReceiver>(grpcChannel, this);

        // 入室する
        var roomUsers = await client.JoinAsync(roomName, userName, "はじめまして");

        // 自分以外のユーザーをメモリに格納する
        foreach (var user in roomUsers)
        {
            if (user.UserName != userName)
            {
                (this as IChatHubReceiver).OnJoin(user);
            }
        }
    }

    /// <summary>
    /// 退室
    /// </summary>
    /// <returns></returns>
    public Task LeaveAsync()
    {
        return client.LeaveAsync();
    }

    /// <summary>
    /// 投稿
    /// </summary>
    /// <param name="comment"></param>
    /// <returns></returns>
    public Task WriteAsync(string comment)
    {
        return client.WriteAsync(comment);
    }

    /// <summary>
    /// メモリ解放
    /// </summary>
    /// <returns></returns>
    public Task DisposeAsync()
    {
        return client.DisposeAsync();
    }

    /// <summary>
    /// 切断
    /// </summary>
    /// <returns></returns>
    public Task WaitForDisconnect()
    {
        return client.WaitForDisconnect();
    }

    /// <summary>
    /// 入室時
    /// </summary>
    /// <param name="user"></param>
    void IChatHubReceiver.OnJoin(User user)
    {
        Console.WriteLine($"{user.UserName}さんが入室しました。");
        Console.WriteLine(user.Comment);
    }

    /// <summary>
    /// 退出時
    /// </summary>
    /// <param name="user"></param>
    /// <exception cref="NotImplementedException"></exception>
    void IChatHubReceiver.OnLeave(User user)
    {
        Console.WriteLine($"{user.UserName}さんが退室しました。");
    }

    /// <summary>
    /// 発言
    /// </summary>
    /// <param name="user"></param>
    /// <exception cref="NotImplementedException"></exception>
    void IChatHubReceiver.OnWrite(User user)
    {
        Console.WriteLine($"{user.UserName} : {user.Comment}");
    }
}