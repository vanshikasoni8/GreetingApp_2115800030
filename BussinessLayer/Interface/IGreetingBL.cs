using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Interface
{
    public interface IGreetingBL
    {
        string GetGreeting(string FirstName,  string LastName);
    }
}
