using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace OpenTelemetryCSharp.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class ContactsController : ControllerBase
    {
        private readonly ContactsAPIDbContext _contactsAPIDbContext;
        public ContactsController(ContactsAPIDbContext contactsAPIDbContext)
        {
            _contactsAPIDbContext = contactsAPIDbContext;
        }


        [HttpGet]
        public string Get()
        {
            var d = _contactsAPIDbContext.Contacts.ToList();
            return JsonConvert.SerializeObject(d);  
        }

        [HttpPost]
        public string Post(Details details)
        {
            var contact = new Contact()
            {
                Id = Guid.NewGuid(),
                Name = details.Name,  
                Description = details.Description

            };
            _contactsAPIDbContext.Contacts.Add(contact);
            _contactsAPIDbContext.SaveChanges();
            return "done";

        }

    }
}
