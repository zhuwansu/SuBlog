/*******************************************************
 * 
 * 作者：朱皖苏
 * 创建日期：20200829
 * 说明：此文件只包含一个类，具体内容见类型注释。
 * 运行环境：.NET Stanard 2.0
 * 版本号：1.0.0
 * 
 * 历史记录：
 * 创建文件 朱皖苏 20200829 13:57
 * 
*******************************************************/

using ApiModels;
using Domain;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Sublog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        public async Task<IActionResult> GetAsync()
        {
            var account = await new AccountService().GetAsync();
            return Result.Ok(account);
        }
    }
}
