using EzTabs.Data;
using EzTabs.Data.Domain;
using EzTabs.Data.Repository;
using EzTabs.Presentation.Services.ContextServices;
using EzTabs.Presentation.Services.DomainServices;
using Microsoft.EntityFrameworkCore;

namespace EzTabs.Presentation.Services.SearchingServices
{
    public class SearchingService
    {
        private IContextFactoryService _contextFactoryService;
        private EzTabsContext _context;
        private Task _initializeTask;

        public SearchingService(IContextFactoryService contextFactoryService)
        {
            _contextFactoryService = contextFactoryService;
            _initializeTask = Task.Run(async () =>
            {
                _context = await contextFactoryService.CreateAsync();
            });
        }

        private async Task EnsureContextCreated()
        {
            if (_context == null) await _initializeTask;
        }

        public async Task<List<Tab>> SearchTabs(double height, int currentPage, string searchText, string? authorName = null)
        {
            await EnsureContextCreated();

            int amountOftabsToSend = (int)(height / 40) + 1;

            IQueryable<Tab> tabs = _context!.Set<Tab>();

            if (authorName != null)
            {
                IQueryable<User> users = _context.Set<User>();
                User? user = users.ToList().Find(u => u.Name == authorName);
                if (user != null) tabs = tabs.Where(t => t.AuthorId == user.Id);
            }
            tabs = tabs.Where(t => EF.Functions.Like(t.Band + t.Title, $"%{searchText}%"));

            return tabs
                .AsNoTracking()
                .Skip(currentPage * amountOftabsToSend)
                .Take(amountOftabsToSend)
                .Select(tab => new Tab
                {
                    Id = tab.Id,
                    Title = tab.Title,
                    Band = tab.Band,
                    Genre = tab.Genre,
                    Description = tab.Description,
                    DateOfCreation = tab.DateOfCreation,
                    BitsPerMinute = tab.BitsPerMinute,
                    Key = tab.Key,
                    AuthorId = tab.AuthorId
                })
                .ToList();
        }
    }
}
