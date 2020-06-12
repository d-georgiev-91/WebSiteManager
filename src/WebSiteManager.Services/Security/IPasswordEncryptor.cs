namespace WebSiteManager.Services.Security
{
    /// <summary>
    /// Encrypts and decrypts password
    /// </summary>
    public interface IPasswordEncryptor
    {
        /// <summary>
        /// Encrypts password
        /// </summary>
        /// <param name="password">The password</param>
        /// <returns>The encrypted password</returns>
        string Encrypt(string password);

        /// <summary>
        /// Decrypts password
        /// </summary>
        /// <param name="encryptedPassword">The password</param>
        /// <returns>The decrypted password</returns>
        string Decrypt(string encryptedPassword);
    }
}
