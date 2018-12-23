using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using XOProject.Helper.ExtensionMethods;
using XOProject.Repository.Domain;
using XOProject.Repository.Exchange;
using XOProject.Services.Domain;

namespace XOProject.Services.Exchange
{
    public class AnalyticsService : GenericService<HourlyShareRate>, IAnalyticsService
    {

        public AnalyticsService(IShareRepository shareRepository):base(shareRepository)
        {
            
        }

        public async Task<AnalyticsPrice> GetDailyAsync(string symbol, DateTime day)
        {
            var TodayShares = await EntityRepository.Query()
                                    .Where(x => x.Symbol.Equals(symbol, StringComparison.OrdinalIgnoreCase) && x.TimeStamp.Date.CompareTo(day.Date) == 0)
                                    .OrderBy(x => x.TimeStamp)
                                    .ToArrayAsync();

            return SetAnalyticsPrice(TodayShares);
      
        }

        public async Task<AnalyticsPrice> GetWeeklyAsync(string symbol, int year, int week)
        {

            var weekShares = await EntityRepository.Query()
                                    .Where(x => x.Symbol.Equals(symbol, StringComparison.OrdinalIgnoreCase) && x.TimeStamp.Year.Equals(year) && x.TimeStamp.GetWeekNumber().Equals(week))
                                    .OrderBy(x => x.TimeStamp)
                                    .ToArrayAsync();

            return SetAnalyticsPrice(weekShares);
        }

        public async Task<AnalyticsPrice> GetMonthlyAsync(string symbol, int year, int month)
        {
            var monthlyShares = await EntityRepository.Query()
                                      .Where(x => x.Symbol.Equals(symbol, StringComparison.OrdinalIgnoreCase) && x.TimeStamp.Date.Year.Equals(year) && x.TimeStamp.Month.Equals(month))
                                      .OrderBy(x => x.TimeStamp)
                                      .ToArrayAsync();

            return SetAnalyticsPrice(monthlyShares);
        }

        private AnalyticsPrice SetAnalyticsPrice(HourlyShareRate[] hourlyShareRates)
        {
            var analyticsPrice = new AnalyticsPrice
            {
                Close = hourlyShareRates.LastOrDefault().Rate,
                Open = hourlyShareRates.FirstOrDefault().Rate,
                High = hourlyShareRates.Max(x => x.Rate),
                Low = hourlyShareRates.Min(x => x.Rate)
            };

            return analyticsPrice;
        }
    }
}