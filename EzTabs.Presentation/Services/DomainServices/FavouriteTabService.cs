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
            var favouriteTab = await _repository.GetByCompositeId(userId, tabId);
            if (!favouriteTab.Success) throw new InvalidOperationException(favouriteTab.ErrorMessage);

            if (favouriteTab.Data is null && isFavourite)
            {
                var operation = await _repository.Add(new FavouriteTab() { UserId = userId, TabId = tabId });
                if (!operation.Success) throw new InvalidOperationException(operation.ErrorMessage);
            }
            else if (favouriteTab.Data != null && !isFavourite) 
            { 
                var operation = await _repository.Delete(favouriteTab.Data);
                if (!operation.Success) throw new InvalidOperationException(operation.ErrorMessage);
            }
        }
        
        public async Task<bool> IsTabFavouriteByIds(Guid userId, Guid tabId)
        {
            var favouriteTab = await _repository.GetByCompositeId(userId, tabId);
            if (!favouriteTab.Success) throw new InvalidOperationException(favouriteTab.ErrorMessage);

            if (favouriteTab.Data != null) return true;

            return false;
        }
    }
}
