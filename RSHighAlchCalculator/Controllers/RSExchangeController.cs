using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RSHighAlchCalculator.Models;
using RSHighAlchCalculator.Names;
using RSHighAlchCalculator.Price;

namespace RSHighAlchCalculator.Controllers
{
    public class RSExchangeController : Controller
    {
        private HttpClient _client = new HttpClient();
        public async Task<IActionResult> Index()
        {
            var url = "https://rsbuddy.com/exchange/summary.json";

            var responds = await _client.GetStringAsync(url);

            var result = RsExchangeData.FromJson(responds);

            var exchangeList = result.Select(x => x.Value).ToList();

            var natPriceData = await GetPriceDataById(561);
            var natPrice = natPriceData.Overall;

            var filteredList = FilterExchangeDataList(exchangeList, natPrice);

            var alchList = await GetHighAlchViewModelList(filteredList, natPrice);

            var displayViewModel = SetDisplayViewModel(alchList, natPrice);

            return View(displayViewModel);
        }

        private List<RsExchangeData> FilterExchangeDataList(List<RsExchangeData> exchangeList, long natPrice)
        {
            return exchangeList.Where(x => x.Members == false)
                            .Where(x => x.Sp * 0.6 > natPrice)
                            .Where(x => x.Sp * 0.6 > x.BuyAverage + natPrice).ToList();
        }

        private async Task<List<HighAlchViewModel>> GetHighAlchViewModelList(List<RsExchangeData> sortedList, long natPrice)
        {
            var alchList = new List<HighAlchViewModel>();

            foreach (var item in sortedList)
            {
                var currentPriceData = await GetPriceDataById(item.Id);

                var overallPrice = currentPriceData.Overall;
                var buyPrice = currentPriceData.Buying;
                var sellPrice = currentPriceData.Selling;
                var buyQuant = currentPriceData.BuyingQuantity;
                var sellQuant = currentPriceData.SellingQuantity;
                var highAlch = Convert.ToInt64(item.Sp * 0.6);
                long profit;

                if (buyPrice == 0)
                {
                    profit = highAlch - (overallPrice + natPrice);
                }
                else
                {
                    profit = highAlch - (buyPrice + natPrice);
                }

                var alchModel = SetHighAlchViewModel(item, buyPrice, sellPrice, buyQuant, sellQuant, highAlch, profit);
                alchList.Add(alchModel);
            }

            return alchList;
        }

        private HighAlchViewModel SetHighAlchViewModel(RsExchangeData item, long buyPrice, long sellPrice, long buyQuant, long sellQuant, long highAlch, long profit)
        {
            return new HighAlchViewModel
            {
                Id = item.Id,
                Name = item.Name,
                BuyPrice = buyPrice,
                SellPrice = sellPrice,
                HighAlch = highAlch,
                Profit = profit,
                BuyQuantity = buyQuant,
                SellQuantity = sellQuant,
            };
        }

        private DisplayViewModel SetDisplayViewModel(List<HighAlchViewModel> alchList, long natPrice)
        {
            return new DisplayViewModel
            {
                NaturePrice = natPrice,
                ItemCount = alchList.Count(),
                ProfitItemCount = alchList.Where(x => x.Profit > 0).Count(),
                HighAlchList = alchList.Where(x => x.BuyQuantity > 10 || x.SellQuantity > 10)
                                .OrderByDescending(x => x.Profit).ToList(),
            };
        }

        public async Task<IActionResult> PriceDetails(long id)
        {
            RsPriceData result = await GetPriceDataById(id);

            ViewBag.ItemName = await GetItemNameByIdAsync(id);

            return View(result);
        }

        private async Task<RsPriceData> GetPriceDataById(long id)
        {
            string url = "https://api.rsbuddy.com/grandExchange?a=guidePrice&i=" + id;

            var responds = await _client.GetStringAsync(url);

            var result = RsPriceData.FromJson(responds);
            return result;
        }

        private async Task<string> GetItemNameByIdAsync(long id)
        {
            //var client = new HttpClient();
            string url = "https://rsbuddy.com/exchange/summary.json";

            var responds = await _client.GetStringAsync(url);

            var result = RsExchangeData.FromJson(responds);

            var name = result[id.ToString()].Name;
            return name;
        }

        private async Task<RsExchangeData> GetItemByIdAsync(long id)
        {
            //var client = new HttpClient();
            string url = "https://rsbuddy.com/exchange/summary.json";

            var responds = await _client.GetStringAsync(url);

            var result = RsExchangeData.FromJson(responds);

            var data = result[id.ToString()];
            return data;
        }

    }
}