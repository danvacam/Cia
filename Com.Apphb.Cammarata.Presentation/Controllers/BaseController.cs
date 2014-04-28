using System.Configuration;
using System.Web.Mvc;
using Com.Apphb.Cammarata.Presentation.Filters;
using MongoDB.Driver;

namespace Com.Apphb.Cammarata.Presentation.Controllers
{
    [InitializeSimpleMembership]
    public abstract class BaseController : Controller
    {
        public MongoDatabase Database
        {
            get
            {
                return MongoDatabase.Create(GetMongoDbConnectionString());
            }
        }

        private string GetMongoDbConnectionString()
        {
            return ConfigurationManager.AppSettings.Get("MONGOHQ_URL") ??
                ConfigurationManager.AppSettings.Get("MONGOLAB_URI") ??
                "mongodb://localhost/Things";
        }
    }
}