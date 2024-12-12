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

        public async Task ApplyCommentRate(Guid authorId, Guid commentId, bool isLiked)
        {
            if (isLiked)
            {
                await _repository.Add(new CommentRate() { UserId = authorId, CommentId = commentId, Rate = true });
                return;
            }
            CommentRate? foundedCommentRate = await _repository.GetByCompositeId(authorId, commentId);
            if (foundedCommentRate is null) return;
            await _repository.Delete(foundedCommentRate);
        }
    }
}
