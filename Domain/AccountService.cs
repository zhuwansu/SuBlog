/**********************************************

SafeitemName   : AccountService.cs
CLRVersion     : 4.0.30319.42000
Machine        : ZWS
Overview       : 
Author         : zws
Time           : 2020/8/29 16:10:13
Version        : V1.0.0.0

**********************************************/


using DapperDb.Repos;
using DbEntities;
namespace Domain
{
    public class AccountService : IService
    {
        public async System.Threading.Tasks.Task<Account> GetAsync()
        {
            return await DapperDb.RS.Repo<AccountRepo>().GetAsync();
        }
    }
}
