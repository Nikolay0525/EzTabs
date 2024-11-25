using EzTabs.Data;

namespace EzTabs.Presentation.Services.ContextServices
{
    public interface IContextFactoryService
    {
        Task<EzTabsContext> CreateAsync();
    }
}