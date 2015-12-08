using WebReader.Samples.Models;
using System.Collections.Generic;
using System.Web.Http;

namespace WebReader.Samples.ApiControllers
{
    public class PeopleController : ApiController
    {
        public IEnumerable<Person> Get()
        {
            return Person.GetPersons();
        }
    }
}
