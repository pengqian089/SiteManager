namespace SiteManager.Web.Models;

public class ResultInfo
{
    public ResultInfo() { }

    public ResultInfo(bool success)
    {
        Success = success;
    }

    public ResultInfo(dynamic data) : this(true)
    {
        Data = data;
    }

    public ResultInfo(string message) : this(false)
    {
        Msg = message;
    }

    public ResultInfo(string message, bool success)
    {
        Msg = message;
        Success = success;
    }

    public ResultInfo(string message, dynamic data) : this(true)
    {
        Msg = message;
        Data = data;
    }

    /// <summary>
    /// 是否成功
    /// </summary>
    public bool Success { get; set; }

    /// <summary>
    /// 消息
    /// </summary>
    public string Msg { get; set; }

    /// <summary>
    /// 数据
    /// </summary>
    public dynamic Data { get; set; }
}