namespace ChatClient;

/// <summary>
/// ChatArg
/// </summary>
public class ChatArg
{
    
    /// <summary>
    /// USER_NAME
    /// </summary>
    private static readonly string USER_NAME = "--userName=";

    /// <summary>
    /// ユーザー名
    /// </summary>
    private static string? _userName ;

    /// <summary>
    /// _userNameのgetter
    /// </summary>
    public string? userName => _userName;

    
    /// <summary>
    /// ROOM_NAME
    /// </summary>
    private static readonly string ROOM_NAME = "--roomName=";

    /// <summary>
    /// ルーム名
    /// </summary>
    private static string? _roomName ;

    /// <summary>
    /// _roomNameのgetter
    /// </summary>
    public string? roomName => _roomName;

    /// <summary>
    /// コンストラクタ
    /// 引数解析
    /// </summary>
    /// <param name="args"></param>
    public ChatArg(string[] args)
    {
        _userName = args.FirstOrDefault(s => s.StartsWith(USER_NAME))?.Substring(USER_NAME.Length);
        _roomName = args.FirstOrDefault(s => s.StartsWith(ROOM_NAME))?.Substring(ROOM_NAME.Length) ?? "sampleRoom";
    }

    public int CheckArg()
    {
        return _userName == null ? 99 : 0;
    }
}