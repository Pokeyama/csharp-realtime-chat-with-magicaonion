using ChatClient;
using Xunit;

namespace ChatTest;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        var args = new ChatArg(new []{"--userName=test"});
        Assert.Equal("test", args.userName);
    }
    
    [Fact]
    public void Test2()
    {
        var args = new ChatArg(new []{"--userName=test"});
        Assert.Equal("test", args.roomName);
    }
    
    [Fact]
    public void Test3()
    {
        var args = new ChatArg(new []{"--userName=test","--roomName=linqroom"});
        Assert.Equal("linqroom", args.roomName);
    }
}