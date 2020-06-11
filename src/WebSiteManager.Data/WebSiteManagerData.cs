using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebSiteManager.Data.Repositories;
using WebSiteManager.DataModels;

namespace WebSiteManager.Data
{
    public class WebSiteManagerData : IWebSiteManagerData
    {
        private readonly WebSiteManagerContext _dbContext;
        private Dictionary<Type, object> _repositories;

        public WebSiteManagerData(WebSiteManagerContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IRepository<WebSite> WebSiteRepository => GetRepository<WebSite>();

        public IRepository<Login> LoginRepository => GetRepository<Login>();

        private IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            _repositories ??= new Dictionary<Type, object>();

            var type = typeof(TEntity);

            if (!_repositories.ContainsKey(type))
            {
                var repository = new Repository<TEntity>(_dbContext);
                _repositories.Add(type, repository);
            }

            return (IRepository<TEntity>)_repositories[type];
        }

        public async Task<int> SaveChangesAsync() => await _dbContext.SaveChangesAsync();
    }
}