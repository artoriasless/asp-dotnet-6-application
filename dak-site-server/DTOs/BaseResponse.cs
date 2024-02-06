using System.ComponentModel;

namespace DakSite.DTOs
{
    public class BaseResponse
    {
        [Description("请求结果是否成功")]
        public bool success { get; set; } = true;

        [Description("请求结果提示信息")]
        public string message { get; set; } = string.Empty;

        [Description("请求结果数据")]
        public object? data { get; set; } = null;
    }

    public class BaseResponse<T>
    {
        [Description("请求结果是否成功")]
        public bool success { get; set; } = true;

        [Description("请求结果提示信息")]
        public string message { get; set; } = string.Empty;

        [Description("请求结果数据")]
        public T? data { get; set; }
    }
}
