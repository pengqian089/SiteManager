using MongoDB.Driver;
using SiteManager.Infrastructure;

namespace SiteManager.MongodbAccess
{
    public sealed class MongodbClient
    {
        internal static (MongoClient,DbOption) GetClient(DbOption option)
        {
            MongoIdentity identity = new MongoExternalIdentity(option.Db, option.ConnUser);
            MongoIdentityEvidence identityEvidence = new PasswordEvidence(option.Password);
            var setting = new MongoClientSettings
            {
                Server = new MongoServerAddress(option.Host, option.Port),
                Credential = new MongoCredential("SCRAM-SHA-1", identity, identityEvidence),
                RetryWrites = false,
            };
            var client = new MongoClient(setting);
            return (client, option);
        }
    }
}