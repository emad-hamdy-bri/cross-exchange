using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using XOProject.Api.Model.Analytics;
using XOProject.Services.Domain;
using XOProject.Services.Exchange;

using Microsoft.AspNetCore.Mvc;

namespace XOProject.Api.Controller
{
    [Route("api")]
    public class AnalyticsController : ControllerBase
    {
        private readonly IAnalyticsService _analyticsService;

        public AnalyticsController(IAnalyticsService analyticsService)
        {
            _analyticsService = analyticsService;
        }

        /// <summary>Get daily analytics price</summary>
        /// <param name="symbol">symbol of the share</param>
        /// <param name="year">numerical year example (2018)</param>
        /// <param name="month">numerical month example March is 3</param>
        /// <param name="day">numerical day example 3</param>
        [HttpGet("daily/{symbol}/{year}/{month}/{day}")]
        [Produces(typeof(DailyModel))]
        public async Task<IActionResult> Daily([FromRoute] string symbol, [FromRoute] int year, [FromRoute] int month, [FromRoute] int day)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var response = await _analyticsService.GetDailyAsync(symbol, new DateTime(year, month, day));
                    if (response != null)
                    {
                        var result = new DailyModel()
                        {
                            Symbol = symbol,
                            Day = new DateTime(year, month, day).Date,
                            Price = Map(response)
                        };

                        return Ok(result);
                    }
                    return NotFound();
                }
                catch(Exception)
                {
                    
                } 
            }
            return BadRequest("error occured");
        }

        /// <summary>Get analytics shares weekly</summary>
        /// <param name="symbol">symbol of the share</param>
        /// <param name="year">numeric year example 2018</param>
        /// <param name="week">numeric week number of the year example 12</param>
        [HttpGet("weekly/{symbol}/{year}/{week}")]
        [Produces(typeof(DailyModel))]
        public async Task<IActionResult> Weekly([FromRoute] string symbol, [FromRoute] int year, [FromRoute] int week)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var response = await _analyticsService.GetWeeklyAsync(symbol, year, week);
                    if (response != null)
                    {
                        var result = new DailyModel()
                        {
                            Symbol = symbol,
                            Day = DateTime.Now.Date,
                            Price = Map(response)
                        };

                        return Ok(result);
                    }
                    return NotFound();
                }
                catch (Exception)
                {
                    
                }
            }
            return BadRequest("error occured");
        }

        /// <summary>Get monthly analytics for share prices</summary>
        /// <param name="symbol">Symbol of the share</param>
        /// <param name="year">numerical year example 2018</param>
        /// <param name="month">numerical month example 3</param>
        [HttpGet("monthly/{symbol}/{year}/{month}")]
        [Produces(typeof(DailyModel))]
        public async Task<IActionResult> Monthly([FromRoute] string symbol, [FromRoute] int year, [FromRoute] int month)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var response = await _analyticsService.GetMonthlyAsync(symbol, year, month);
                    if (response != null)
                    {
                        var result = new DailyModel()
                        {
                            Symbol = symbol,
                            Day = DateTime.Now.Date,
                            Price = Map(response)
                        };

                        return Ok(result);
                    }
                    return NotFound();
                }
                catch (Exception)
                {
                
                }
            }
            return BadRequest("error occured");
        }

        private PriceModel Map(AnalyticsPrice price)
        {
            return new PriceModel()
            {
                Open = price.Open,
                Close = price.Close,
                High = price.High,
                Low = price.Low
            };
        }
    }
}