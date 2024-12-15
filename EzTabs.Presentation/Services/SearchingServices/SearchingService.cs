using EzTabs.Data;
using EzTabs.Data.Domain;
using EzTabs.Data.Repository;

using EzTabs.Presentation.Services.DomainServices;
using EzTabs.Presentation.ViewModels.MainControlsViewModels.Enums;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

namespace EzTabs.Presentation.Services.SearchingServices
{
    public class SearchingService
    {
        private EzTabsContext _context;

        public SearchingService(EzTabsContext context)
        {
            _context = context;
        }

        public async Task<(List<Comment>, int)> ShowComments(int amount, int currentPage, SortByOption sortByOption)
        {
            IQueryable<Comment> comments = _context!.Set<Comment>();

            Guid savedTabId = TabService.SavedTab!.Id;
            comments = comments.Where(c => c.TabId == savedTabId && c.ParentCommentId == Guid.Empty);

            comments = sortByOption switch
            {
                SortByOption.Rating => comments.OrderByDescending(c => c.Likes),
                SortByOption.Newest => comments.OrderByDescending(c => c.DateOfCreation),
                _ => comments 
            };

            int totalComments = await comments.CountAsync();

            List<Comment> pagedComments = await comments
                .AsNoTracking()
                .Skip(currentPage * amount)
                .Take(amount)
                .Select(c => new Comment
                {
                    Id = c.Id,
                    UserId = c.UserId,
                    TabId = c.TabId,
                    Text = c.Text,
                    Likes = c.Likes,
                    ParentCommentId = c.ParentCommentId,
                    DateOfCreation = c.DateOfCreation
                })
                .ToListAsync();

            return (pagedComments, totalComments);
        }

        public async Task<(List<Tab>, int)> SearchTabs(int amount, int currentPage, string searchText, SearchByOption searchByOption, SortByOption sortByOption, string authorName)
        {
            IQueryable<Tab> tabs = _context!.Set<Tab>();

            if (searchByOption == SearchByOption.SongAuthor)
            {
                User? user = await _context.Set<User>().FirstOrDefaultAsync(u => u.Name == authorName);
                if (user != null)
                {
                    tabs = tabs.Where(t => t.AuthorId == user.Id);
                }
            }

            tabs = tabs.Where(t => EF.Functions.Like(t.Band + " " + t.Title, $"%{searchText}%"));

            tabs = sortByOption switch
            {
                SortByOption.Popularity => tabs.OrderByDescending(t => t.Views),
                SortByOption.Rating => tabs.OrderByDescending(t => t.Rating),
                SortByOption.Newest => tabs.OrderByDescending(t => t.DateOfCreation),
                _ => tabs
            };

            int totalRecords = await tabs.CountAsync();

            List<Tab> pagedTabs = await tabs
                .AsNoTracking()
                .Skip(currentPage * amount)
                .Take(amount)
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
                    AuthorId = tab.AuthorId,
                    Views = tab.Views,
                    Rating = tab.Rating
                })
                .ToListAsync();

            return (pagedTabs, totalRecords);
        }
    }
}
