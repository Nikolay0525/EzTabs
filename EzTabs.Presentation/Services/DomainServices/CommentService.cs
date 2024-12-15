using EzTabs.Data;
using EzTabs.Data.Domain;
using EzTabs.Presentation.Services.DomainServices.BaseServices;
using EzTabs.Presentation.Services.NavigationServices;
using EzTabs.Presentation.Views.MainControls.SimpleControls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
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
        
        public async Task<Comment?> FindCommentById(Guid commentId)
        {
            var comment = await _repository.GetById(commentId);
            return comment;
        }

        public async Task UpdateAmountOfLikesById(Guid commentId, int likes)
        {
            Comment? comment = await FindCommentById(commentId);
            if (comment is null) return;
            comment.Likes = likes;
            await _repository.Update(comment);
        }

        public async Task<List<Comment>> GetCommentReplyesById(Guid parentCommentId)
        {
            var replyes = await _repository.GetAll();
            replyes = replyes.Where(c => c.ParentCommentId == parentCommentId);
            return replyes.ToList();
        }

        public async Task<List<CommentControl>> IsThereAnyReplyes(Guid commentId, object dataContext)
        {
            var comments = await _repository.GetAll();
            List<CommentControl> dummyList = comments.Any(r => r.ParentCommentId == commentId) ? new() { new CommentControl() { DataContext = dataContext } } : new();
            return dummyList;
        }
    }
}
