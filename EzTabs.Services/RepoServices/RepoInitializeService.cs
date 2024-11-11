using EzTabs.Data.Repository;
using EzTabs.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzTabs.Services.RepoServices
{
    public static class RepoInitializeService
    {
        public static async Task<RepoImplementation<T>> InitializeRepoAsync<T>() where T : class
        {
            return await RepoImplementation<T>.CreateRepoAsync();
        }
    }
}
