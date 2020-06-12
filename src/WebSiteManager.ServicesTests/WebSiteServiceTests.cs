using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using WebSiteManager.Data;
using WebSiteManager.Data.Repositories;
using WebSiteManager.DataModels;
using WebSiteManager.Services;

namespace WebSiteManager.ServicesTests
{
    [TestFixture]
    public class WebSiteServiceTests
    {
        private IWebSiteManagerData _webSiteManagerData;
        private WebSiteService _webSiteService;
        private IRepository<WebSite> _webSiteRepository;

        [SetUp]
        public void Setup()
        {
            _webSiteRepository = Substitute.For<IRepository<WebSite>>();
            _webSiteManagerData = Substitute.For<IWebSiteManagerData>();
            _webSiteService = new WebSiteService(_webSiteManagerData);
        }

        #region Delete Tests
        [Test]
        public async Task WhenExistingWebSiteIsDeletedThenThereShouldBeNoErrorsAndWebSiteShouldBeMarkedAsDeleted()
        {
            var webSiteToDelete = new WebSite
            {
                Id = 1,
                IsDeleted = false
            };
            _webSiteRepository.Get(Arg.Any<Expression<Func<WebSite, bool>>>()).Returns(new List<WebSite>
            {
                webSiteToDelete
            });

            _webSiteManagerData.WebSiteRepository.Returns(_webSiteRepository);
            var serviceResult = await _webSiteService.DeleteAsync(1);

            await _webSiteManagerData.Received(1).SaveChangesAsync();
            Assert.That(serviceResult.HasErrors, Is.False, "There should be no errors");
            Assert.That(webSiteToDelete.IsDeleted, Is.True, "WebSite should be marked as deleted");
        }

        [Test]
        public async Task WhenNonExistingWebSiteIsDeletedThenThereShouldBeNotFoundErrorAndSaveChangesShouldNotBeCalled()
        {
            _webSiteManagerData.WebSiteRepository.Returns(_webSiteRepository);
            var serviceResult = await _webSiteService.DeleteAsync(1);

            await _webSiteManagerData.DidNotReceive().SaveChangesAsync();
            Assert.That(serviceResult.HasErrors, Is.True, "There should be no errors");
            Assert.That(serviceResult.Errors, Does.ContainKey(ErrorType.NotFound), "There should be not found error");
        }
        #endregion
    }
}