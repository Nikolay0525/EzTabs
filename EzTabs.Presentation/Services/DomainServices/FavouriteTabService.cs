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
    public class FavouriteTabService : BaseService<FavouriteTab>
    {
        public FavouriteTabService(EzTabsContext context, INavigationService navigationService) : base(context, navigationService) { }

        public async Task UpdateStateOfTab(Guid userId, Guid tabId, bool isFavourite)
        {
            FavouriteTab? favouriteTab = await _repository.GetByCompositeId(userId, tabId);
            if (favouriteTab is null && isFavourite) await _repository.Add(new FavouriteTab() { UserId = userId, TabId = tabId });
            else if (favouriteTab != null && !isFavourite) { await _repository.Delete(favouriteTab); }
        }
        
        public async Task<bool> IsTabFavouriteByIds(Guid userId, Guid tabId)
        {
            FavouriteTab? favouriteTab = await _repository.GetByCompositeId(userId, tabId);

            if (favouriteTab != null) return true;

            return false;
        }
    }
}
