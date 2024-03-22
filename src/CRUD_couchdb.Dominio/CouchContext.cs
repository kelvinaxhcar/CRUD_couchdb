using CouchDB.Driver;
using CouchDB.Driver.Options;

namespace CRUD_couchdb.Dominio
{
    public class CouchContext : CouchDB.Driver.CouchContext
    {
        public CouchDatabase<Produto> Produtos { get; set; }

        protected override void OnConfiguring(CouchOptionsBuilder optionsBuilder)
        {
            optionsBuilder
              .UseEndpoint("http://localhost:5984/")
              .EnsureDatabaseExists()
              .UseBasicAuthentication(username: "admin", password: "123");
        }
    }
}
