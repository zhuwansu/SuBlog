/**********************************************

SafeitemName   : RepoManager.cs
CLRVersion     : 4.0.30319.42000
Machine        : ZWS
Overview       : 
Author         : zws
Time           : 2020/8/29 14:38:46
Version        : V1.0.0.0

**********************************************/

using DapperDb.Repos;
using DbEntities;
using System.ComponentModel;

namespace DapperDb
{
    public static class RS
    {
        public static RepoResolver RepoInstatnce { get; set; } = new RepoResolver();
        public static IRepo Entity<T>() where T : IEntity
        {
            return RepoInstatnce.FindRepo<T>();
        }

        public static T Repo<T>() where T : IRepo,new()
        {
            return RepoInstatnce.ResoveRepo<T>();
        }

    }

    public class RepoResolver
    {
        /// <summary>
        /// get repo from entity
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IRepo FindRepo<T>() where T : IEntity
        {
            return new DefaultRepo<T>();
        }

        public T ResoveRepo<T>() where T : IRepo, new()
        {
            return new T();
        }
    }

}
