using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading;
using System.Web.Mvc;
using Com.Apphb.Cammarata.EF.Persistence;
using WebMatrix.WebData;

namespace Com.Apphb.Cammarata.Presentation.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class InitializeSimpleMembershipAttribute : ActionFilterAttribute
    {
        private static SimpleMembershipInitializer _initializer;
        private static object _initializerLock = new object();
        private static bool _isInitialized;

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // Ensure ASP.NET Simple Membership is initialized only once per app start
            LazyInitializer.EnsureInitialized(ref _initializer, ref _isInitialized, ref _initializerLock);
        }

        private class SimpleMembershipInitializer
        {
            public SimpleMembershipInitializer()
            {
                Database.SetInitializer<Context>(null);

                try
                {
                    using (var context = new Context())
                    {
                        if (!context.Database.Exists())
                        {
                            // Create the SimpleMembership database without Entity Framework migration schema
                            ((IObjectContextAdapter)context).ObjectContext.CreateDatabase();
                            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<Context, Configuration>());
                        }
                    }

                    WebSecurity.InitializeDatabaseConnection("context", "UserProfile", "UserId", "UserName", true);
                    CheckUserAdminExistence();
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException("The ASP.NET Simple Membership database could not be initialized. For more information, please see http://go.microsoft.com/fwlink/?LinkId=256588", ex);
                }
            }

            private void CheckUserAdminExistence()
            {
                using (var context = new Context())
                {
                    var res = from ctx in context.UserProfiles
                        where ctx.UserName.Equals("Admin") 
                        select ctx;
                    if (!res.Any())
                    {
                        WebSecurity.CreateUserAndAccount("Admin", "namibia");
                    }
                }
            }
        }
    }
}