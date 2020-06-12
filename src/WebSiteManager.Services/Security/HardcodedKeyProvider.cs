namespace WebSiteManager.Services.Security
{
    public class HardcodedKeyProvider : IEncryptionKeyProvider
    {
        public string GetKey() => "SkHmthR4sk4ENDqY";
    }
}