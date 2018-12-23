using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using XOProject.Repository.Domain;
using XOProject.Repository.Exchange;
using XOProject.Repository.Tests.Helpers;

namespace XOProject.Repository.Tests
{
    public class TradeRepositoryTests
    {
        private TradeRepository _tradeRepository;
        private  ExchangeContext _context;

        [SetUp]
        public void Initialize()
        {
            _context = ContextFactory.CreateContext(true);
            _tradeRepository = new TradeRepository(_context);
        }

        [TearDown]
        public void Cleanup()
        {
            _context.Dispose();
            _context = null;
            _tradeRepository = null;
        }

        [Test]
        public async Task GetAsync_WhenDoesNotExist_ReturnsNull()
        {
            // Arrange

            // Act
            var result = await _tradeRepository.GetAsync(99);

            // Assert
            Assert.IsNull(result);
        }

    }
}
