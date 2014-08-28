using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Indigo.Organization
{
    public class IdentityCard
    {
        private string number;

        public string Number { get { return number; } protected set { number = value; } }

        public IdentityCard() { }

        public IdentityCard(string number)
        {
            this.number = number;
        }
    }
}
