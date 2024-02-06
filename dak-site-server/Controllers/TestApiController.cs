using System.ComponentModel;
using DakSite.DTOs;
using DakSite.DTOs.TestApi;
using DakSite.Services;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace DakSite.Controllers
{
    /// <summary>
    /// 测试 Controller
    /// </summary>
    [Route("/api/test")]
    [OpenApiTag("TestApi", Description = "测试 Controller")]
    public class TestApiController : Controller
    {
        private readonly IConfiguration _config;
        private readonly TestService _testService;

        public TestApiController(IConfiguration config, TestService testService)
        {
            _config = config;
            _testService = testService;
        }

        [HttpGet]
        [Route("get-date-time-now")]
        [Description("测试 Get 接口")]
        // Get 类型的接口暂时没找到添加参数 description 的方法
        public BaseResponse<ResponseDataGetDateTimeNow> GetDateTimeNowFromGet(int id, string name)
        {
            string testDataPiece = _config["TestData:TestApi"] ?? "";
            ResponseDataGetDateTimeNow data = new(id, name) { addition = testDataPiece };

            return new BaseResponse<ResponseDataGetDateTimeNow>
            {
                success = true,
                message = "请求成功",
                data = data
            };
        }

        [HttpPost]
        [Route("get-date-time-now")]
        [Description("测试 Post 接口")]
        public BaseResponse<ResponseDataGetDateTimeNow> GetDateTimeNowFromPost(
            ReqBodyGetDateTimeNowFromPost reqBody
        )
        {
            string testDataPiece = _config["TestData:TestApi"] ?? "";
            ResponseDataGetDateTimeNow data =
                new(reqBody.id, reqBody.name) { addition = testDataPiece };

            return new BaseResponse<ResponseDataGetDateTimeNow>
            {
                success = true,
                message = "请求成功",
                data = data
            };
        }

        [HttpPost]
        [Route("add-test-entity")]
        [Description("添加测试实体")]
        public BaseResponse AddTestEntity(ReqBodyAddTestEntity reqBody)
        {
            BaseResponse result = new BaseResponse()
            {
                success = true,
                message = "添加成功",
                data = null
            };

            try
            {
                _testService.AddTestEntity(reqBody.name);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.message = ex.Message;
            }

            return result;
        }
    }
}
