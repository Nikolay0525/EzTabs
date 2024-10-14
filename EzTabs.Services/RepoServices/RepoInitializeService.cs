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
        public static async Task<RepoImplementation<T>> InitializeRepoAsync<T>(RepoImplementation<T>? userRepository = null) where T : class
        {
            if (userRepository is null) return await RepoImplementation<T>.CreateRepoAsync();
            else return userRepository;
        }
    }
}
