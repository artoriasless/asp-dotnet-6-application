using System.ComponentModel;

namespace DakSite.DTOs.TestApi
{
    [Description("/api/test/get-date-time-now 返回数据")]
    public class ResponseDataGetDateTimeNow
    {
        public int id { get; set; }
        public string name { get; set; } = string.Empty;
        public string now { get; set; } = string.Empty;
        public string addition { get; set; } = string.Empty;

        public ResponseDataGetDateTimeNow(int passedId, string passedName)
        {
            id = passedId;
            name = passedName;
            now = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }
    }
}
