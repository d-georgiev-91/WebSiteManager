using AutoMapper;
using WebSiteManager.DataModels;
using WebSiteManager.Services.Security;
using WebSiteManager.Web.Models;

namespace WebSiteManager.Web.AutoMapper.Resolvers
{
    public class PasswordConverter : IValueResolver<WebSite, WebSiteDetailed, string>
    {
        private readonly IPasswordEncryptor _passwordEncryptor;

        public PasswordConverter(IPasswordEncryptor passwordEncryptor) => _passwordEncryptor = passwordEncryptor;

        public string Resolve(WebSite source, WebSiteDetailed destination, string destMember, ResolutionContext context) => _passwordEncryptor.Decrypt(source.Login.Password);
    }
}
