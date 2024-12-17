using EzTabs.Data;
using EzTabs.Data.Domain;
using EzTabs.Data.Repository;
using EzTabs.Presentation.Services.DomainServices.BaseServices;
using EzTabs.Presentation.Services.NavigationServices;

namespace EzTabs.Presentation.Services.DomainServices
{
    public class CommentRateService : BaseService<CommentRate>
    {
        public CommentRateService(EzTabsContext context, INavigationService navigationService) : base(context, navigationService)
        {

        }

        public async Task<int> ApplyCommentRate(Guid authorId, Guid commentId, bool isLiked)
        {
            if (isLiked)
            {
                var operation = await _repository.Add(new CommentRate() { UserId = authorId, CommentId = commentId });
                if (!operation.Success) throw new InvalidOperationException(operation.ErrorMessage);
            }
            if (!isLiked)
            {
                var foundedRate = await _repository.GetByCompositeId(authorId, commentId);
                if (!foundedRate.Success) throw new InvalidOperationException(foundedRate.ErrorMessage);

                if (foundedRate != null) 
                { 
                    var operation = await _repository.Delete(foundedRate.Data!);
                    if (!operation.Success) throw new InvalidOperationException(operation.ErrorMessage);
                }
                
            }
            var allRates = await _repository.GetAll();
            if (!allRates.Success) throw new InvalidOperationException(allRates.ErrorMessage);
            return allRates.Data!.ToList().Count(r => r.CommentId == commentId);
        }

        public async Task<bool> IsCommentLiked(Guid userId, Guid commentId)
        {
            var commentRate = await _repository.GetByCompositeId(userId, commentId);
            if (!commentRate.Success) throw new InvalidOperationException(commentRate.ErrorMessage);
            return commentRate.Data is not null;
        }
    }
}
