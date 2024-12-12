using EzTabs.Data;
using EzTabs.Data.Domain;
using EzTabs.Presentation.Services.DomainServices.BaseServices;
using EzTabs.Presentation.Services.NavigationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace EzTabs.Presentation.Services.DomainServices
{
    public class CommentService : BaseService<Comment>
    {
        public CommentService(EzTabsContext context, INavigationService navigationService) : base(context, navigationService)
        {

        }

        public async Task CreateComment(Guid authorId, Guid tabId,string text,Guid parentCommentId)
        {
            Comment comment = new() { UserId = authorId, TabId = tabId ,Text = text };

            if (parentCommentId != Guid.Empty) comment.ParentCommentId = parentCommentId;

            await _repository.Add(comment);
        }
    }
}
