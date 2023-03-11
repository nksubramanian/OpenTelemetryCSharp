using Microsoft.EntityFrameworkCore;

namespace OpenTelemetryCSharp
{
    public class ContactsAPIDbContext: DbContext
    {

        public ContactsAPIDbContext(DbContextOptions options):base(options)
        {

        }

        public DbSet<Contact> Contacts { get; set; }
    }
}
