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
    public class TabRateService : BaseService<TabRate>
    {
        public TabRateService(EzTabsContext context, INavigationService navigationService) : base(context, navigationService) { }

        public async Task<(List<TabRate>, bool)> ApplyTabRate(Guid tabId, Guid userId, int rate)
        {
            bool removeAllStars = false;
            var tabRate = await _repository.GetByCompositeId(userId,tabId);
            if (!tabRate.Success) throw new InvalidOperationException(tabRate.ErrorMessage);

            if (tabRate != null && tabRate.Data!.Rate != rate)
            {
                tabRate.Data.Rate = rate;
                var operation = await _repository.Update(tabRate.Data);
                if (!operation.Success) throw new InvalidOperationException(operation.ErrorMessage);
            }
            else if (tabRate != null && tabRate.Data!.Rate == rate)
            { 
                var operation = await _repository.Delete(tabRate.Data); removeAllStars = true;
                if (!operation.Success) throw new InvalidOperationException(operation.ErrorMessage);
            }

            if (tabRate == null)
            {
                var operation = await _repository.Add(new TabRate() { TabId = tabId, UserId = userId, Rate = rate });
                if (!operation.Success) throw new InvalidOperationException(operation.ErrorMessage);
            }
            var allTabs = await GetAllTabRatesById(tabId);
            return (allTabs, removeAllStars);
        }

        public async Task<List<TabRate>> GetAllTabRatesById(Guid tabId)
        {
            var allRates = await _repository.GetAll();
            if (!allRates.Success) throw new InvalidOperationException(allRates.ErrorMessage);
            return allRates.Data!.Where(tr => tr.TabId == tabId).ToList();
        }
    }
}
