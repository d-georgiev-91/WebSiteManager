namespace WebSiteManager.Services.Security
{
    /// <summary>
    /// Provides encryption key
    /// </summary>
    public interface IEncryptionKeyProvider
    {
        /// <summary>
        /// Gets encryption key
        /// </summary>
        /// <returns>The encryption key</returns>
        string GetKey();
    }
}