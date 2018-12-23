using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using XOProject.Repository.Domain;
using XOProject.Repository.Exchange;
using XOProject.Services.Exchange;
using XOProject.Services.Tests.Helpers;

namespace XOProject.Services.Tests
{
    public class TradeServiceTests
    {
        private readonly Mock<IShareService> _shareServiceMock = new Mock<IShareService>();
        private readonly Mock<ITradeRepository>  _tradeRepository = new Mock<ITradeRepository>();
        private readonly TradeService _tradeService;

        public TradeServiceTests()
        {
            _tradeService = new TradeService(_tradeRepository.Object, _shareServiceMock.Object);
        }

        [TearDown]
        public void Cleanup()
        {
            _shareServiceMock.Reset();
            _tradeRepository.Reset();
        }

        [Test]
        public async Task Should_GetByPortfolioId()
        {
            //Arrange 
            ArrangeRates();

            //Act
            var result = await _tradeService.GetByPortfolioId(1);

            //Assert
            Assert.NotNull(result);
            Assert.AreEqual(1, result[0].PortfolioId);
        }

        [Test]
        public async Task Should_BuyOrSell()
        {
            //Arrange 
            ArrangeRates();

            //Act
            var result = await _tradeService.BuyOrSell(1,"CBI",2,"BUY");

            //Assert
            Assert.IsNull(result);
        }

        


        private void ArrangeRates()
        {
            var trades = new[]
            {
                new Trade
                {
                    Action = "BUY",
                    ContractPrice = 21.0M,
                    NoOfShares = 2,
                    PortfolioId = 1
                },
                new Trade
                {
                    Action = "SELL",
                    ContractPrice = 20.0M,
                    NoOfShares = 2,
                    PortfolioId = 1
                },
                new Trade
                {
                    Action = "BUY",
                    ContractPrice = 20.0M,
                    NoOfShares = 2,
                    PortfolioId = 1
                },
               new Trade
                {
                    Action = "BUY",
                    ContractPrice = 20.0M,
                    NoOfShares = 2,
                    PortfolioId = 1
                }
            };
            _tradeRepository
                .Setup(mock => mock.Query())
                .Returns(new AsyncQueryResult<Trade>(trades));
        }
    }
}
