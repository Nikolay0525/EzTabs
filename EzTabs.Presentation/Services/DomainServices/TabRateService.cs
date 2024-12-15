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
            TabRate? tabRate = await _repository.GetByCompositeId(userId,tabId);

            if (tabRate != null && tabRate.Rate != rate)
            {
                tabRate.Rate = rate;
                await _repository.Update(tabRate);
            }
            else if (tabRate != null && tabRate.Rate == rate) { await _repository.Delete(tabRate); removeAllStars = true; }

            if(tabRate == null) await _repository.Add(new TabRate() { TabId = tabId, UserId = userId, Rate = rate });

            var allTabs = await GetAllTabRatesById(tabId);
            return (allTabs, removeAllStars);
        }

        public async Task<List<TabRate>> GetAllTabRatesById(Guid tabId)
        {
            var allRates = await _repository.GetAll();
            return allRates.Where(tr => tr.TabId == tabId).ToList();
        }
    }
}
