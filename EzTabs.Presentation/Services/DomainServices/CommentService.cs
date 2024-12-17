using EzTabs.Data;
using EzTabs.Data.Domain;
using EzTabs.Data.Repository;
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

            var operation = await _repository.Add(comment);
            if (!operation.Success) throw new InvalidOperationException(operation.ErrorMessage);
        }
        
        public async Task<Comment?> FindCommentById(Guid commentId)
        {
            var comment = await _repository.GetById(commentId);
            if (!comment.Success) throw new InvalidOperationException(comment.ErrorMessage);
            return comment.Data;
        }

        public async Task UpdateAmountOfLikesById(Guid commentId, int likes)
        {
            Comment? comment = await FindCommentById(commentId);
            if (comment is null) return;
            comment.Likes = likes;
            var operation = await _repository.Update(comment);
            if (!operation.Success) throw new InvalidOperationException(operation.ErrorMessage);
        }

        public async Task<List<Comment>> GetCommentReplyesById(Guid parentCommentId)
        {
            var replyes = await _repository.GetAll();
            if(!replyes.Success) throw new InvalidOperationException(replyes.ErrorMessage);

            replyes.Data = replyes.Data!.Where(c => c.ParentCommentId == parentCommentId);
            return replyes.Data.ToList();
        }

        public async Task<List<CommentControl>> IsThereAnyReplyes(Guid commentId, object dataContext)
        {
            var comments = await _repository.GetAll();
            if(!comments.Success) throw new InvalidOperationException(comments.ErrorMessage);

            List<CommentControl> dummyList = comments.Data!.Any(r => r.ParentCommentId == commentId) ? new() { new CommentControl() { DataContext = dataContext } } : new();
            return dummyList;
        }
    }
}
