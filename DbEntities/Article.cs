/**********************************************

SafeitemName   : Article.cs
CLRVersion     : 4.0.30319.42000
Machine        : ZWS
Overview       : 
Author         : zws
Time           : 2020/8/29 15:28:38
Version        : V1.0.0.0

**********************************************/

namespace DbEntities
{
    public class Article : IEntity
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public long Author { get; set; }
    }
}
