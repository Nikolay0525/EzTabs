using EzTabs.Data;
using EzTabs.Presentation.ViewModels.BaseViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzTabs.Presentation.Services.ContextServices
{
    public class ContextFactoryService : IContextFactoryService
    {
        private readonly Func<Type, EzTabsContext> _contextFactory;

        public ContextFactoryService(Func<Type, EzTabsContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<EzTabsContext> CreateAsync()
        {
            EzTabsContext context = _contextFactory.Invoke(typeof(EzTabsContext));
            await context.Database.MigrateAsync(); 
            return context;
        }
    }
}
