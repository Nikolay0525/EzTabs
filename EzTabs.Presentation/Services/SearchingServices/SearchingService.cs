using EzTabs.Data;
using EzTabs.Data.Domain;
using EzTabs.Data.Repository;

using EzTabs.Presentation.Services.DomainServices;
using EzTabs.Presentation.ViewModels.MainControlsViewModels.Enums;
using Microsoft.EntityFrameworkCore;

namespace EzTabs.Presentation.Services.SearchingServices
{
    public class SearchingService
    {
        private EzTabsContext _context;

        public SearchingService(EzTabsContext context)
        {
            _context = context;
        }

        public List<Comment> ShowComments(double amount, int currentPage, SortByOption sortByOption)
        {
            int amountOfCommentsToSend = (int)amount + 1;

            IQueryable<Comment> comments = _context!.Set<Comment>();

            switch (sortByOption)
            {
                case SortByOption.Rating:
                    comments = comments.OrderBy(c => c.Text);
                    break;
                case SortByOption.Newest:
                    comments = comments.OrderByDescending(t => t.DateOfCreation);
                    break;
                default:
                    break;
            }

            return comments.AsNoTracking()
               .Skip(currentPage * amountOfCommentsToSend)
               .Take(amountOfCommentsToSend)
               .Select(c => new Comment
               {
                   Id = c.Id,
                   UserId = c.UserId,
                   TabId = c.TabId,
                   ParentCommentId = c.ParentCommentId,
                   Text = c.Text,
                   DateOfCreation = c.DateOfCreation
               })
               .Where(c => c.TabId == TabService.SavedTab!.Id)
               .ToList();
        }

        public List<Tab> SearchTabs(double height, int currentPage, string searchText, SearchByOption searchByOption, SortByOption sortByOption, string authorName)
        {
            int amountOftabsToSend = ((int)height / 40) + 1;

            IQueryable<Tab> tabs = _context!.Set<Tab>();

            if (searchByOption == SearchByOption.SongAuthor)
            {
                User? user = _context.Set<User>().FirstOrDefault(u => u.Name == authorName);
                if (user != null)
                {
                    tabs = tabs.Where(t => t.AuthorId == user.Id);
                }
            }

            switch (sortByOption)
            {
                case SortByOption.Popularity:
                    tabs = tabs.OrderByDescending(t => t.Views);
                    break;
                case SortByOption.Rating:
                    tabs = tabs.OrderByDescending(t => t.Rating);
                    break;
                case SortByOption.Newest:
                    tabs = tabs.OrderByDescending(t => t.DateOfCreation);
                    break;
                default:
                    break;
            }

            return tabs.AsNoTracking()
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
                    AuthorId = tab.AuthorId,
                    Views = tab.Views
                })
                .ToList();

        }
    }
}
