using System.Text.Json.Serialization;
using MongoDB.Bson;

namespace SiteManager.Infrastructure
{
    public interface IBaseEntity
    {

    }

    public class BaseEntity : IBaseEntity
    {
        [JsonConverter(typeof(ObjectIdConverter))]
        public ObjectId Id { get; set; }
    }
}
