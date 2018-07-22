using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace IncendiaryAuth.Domain.Abstract.Models
{
    public class UserModel
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public HttpStatusCode StatusHint { get; set; }
    }
}
