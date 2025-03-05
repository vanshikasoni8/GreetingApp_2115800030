using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Interface
{
    public interface IGreetingBL
    {
        string GetGreetingMessage();

        public string NameGreeting(string firstName, string lastName);

    }
}
