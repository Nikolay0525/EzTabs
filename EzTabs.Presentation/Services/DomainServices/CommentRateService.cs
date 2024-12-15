using EzTabs.Data;
using EzTabs.Data.Domain;
using EzTabs.Presentation.Services.DomainServices.BaseServices;
using EzTabs.Presentation.Services.NavigationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                await _repository.Add(new CommentRate() { UserId = authorId, CommentId = commentId });
            }
            if (!isLiked)
            {
                var foundedRate = await _repository.GetByCompositeId(authorId, commentId);
                if (foundedRate != null) await _repository.Delete(foundedRate);
            }
            IEnumerable<CommentRate> allRates = await _repository.GetAll();
            return allRates.ToList().Count(r => r.CommentId == commentId);
        }

        public async Task<bool> IsCommentLiked(Guid userId, Guid commentId)
        {
            var commentRate = await _repository.GetByCompositeId(userId, commentId);
            return commentRate is not null;
        }
    }
}
