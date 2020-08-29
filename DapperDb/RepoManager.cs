/**********************************************

SafeitemName   : RepoManager.cs
CLRVersion     : 4.0.30319.42000
Machine        : ZWS
Overview       : 
Author         : zws
Time           : 2020/8/29 14:38:46
Version        : V1.0.0.0

**********************************************/

using DbEntities;

namespace DapperDb
{
    public sealed class RF : RepoResolver { }

    public class RepoResolver
    {
        /// <summary>
        /// get repo from entity
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IRepo ResolveRepo<T>() where T : IEntity
        {
            return default;
        }
    }

}
