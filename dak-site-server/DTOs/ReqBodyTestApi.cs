using System.ComponentModel;

namespace DakSite.DTOs.TestApi
{
    public class ReqBodyGetDateTimeNowFromPost
    {
        [Description("ID")]
        public int id { get; set; }

        [Description("名称")]
        public string name { get; set; } = string.Empty;
    }

    public class ReqBodyAddTestEntity
    {
        [Description("名称")]
        public string name { get; set; } = string.Empty;
    }
}
