using System;
using System.Linq;
using System.Reflection;
using Raven.Client;
using Raven.Client.Embedded;
using Raven.Client.Indexes;
using Riddley.VideoGame.Web.Models;

namespace Riddley.VideoGame.Web.Infrastructure.Raven
{
    public class DocumentStoreHolder
    {
        public static IDocumentStore Store { get; set; }

        static DocumentStoreHolder()
        {
            Store = new EmbeddableDocumentStore {DataDirectory = "App_Data"};
            Store.Initialize();
            IndexCreation.CreateIndexes(Assembly.GetExecutingAssembly(), Store);

            SeedDatabase();
        }

        private static void SeedDatabase()
        {
            // add our admin user if it doesn't exist
            using (var session = Store.OpenSession())
            {
                if (session.Query<User>().Count(u => u.Email == "admin@user.com") > 0) return;

                var admin = new User
                                {
                                    AspNetUserGuid = Guid.NewGuid().ToString(),
                                    Email = "admin@user.com",
                                    Name = "Admin"
                                };
                admin.SetPassword("admin");

                session.Store(admin);
                session.SaveChanges();
            }
        }
    }
}