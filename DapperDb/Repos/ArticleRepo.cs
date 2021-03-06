/**********************************************

SafeitemName   : ArticleRepo.cs
CLRVersion     : 4.0.30319.42000
Machine        : ZWS
Overview       : 
Author         : zws
Time           : 2020/8/29 15:30:48
Version        : V1.0.0.0

**********************************************/


using DbEntities;

namespace DapperDb.Repos
{
    public class ArticleRepo : BaseRepo<Article>
    {
        public void CusTest()
        {
            (this as IReader<Article>).GetAsync();//base
            _ = (this as IRepo<Article>).GetAsync();//def
            _ = (this as IRepo).GetEntityAsync<Article>();//cus
        }
    }
}
