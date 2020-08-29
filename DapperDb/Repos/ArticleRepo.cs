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
            (this as IReader<Article>).Get();//base
            (this as IRepo<Article>).Get();//def
            (this as IRepo).GetEntity<Article>();//cus
        }
    }
}
