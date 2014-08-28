using System;

namespace Indigo.Organization
{
    public class IdentityCard
    {
        private string _number;

        public string Number { get { return _number; } protected set { _number = value; } }

        protected IdentityCard() { }

        public IdentityCard(string number)
        {
            _number = number;
        }

        public DateTime GetBirthday()
        {
            int year = Convert.ToInt32(Number.Substring(6, 4));
            int month = Convert.ToInt32(Number.Substring(10, 2));
            int day = Convert.ToInt32(Number.Substring(12, 2));

            return new DateTime(year, month, day);
        }

        public int GetAge()
        {
            DateTime now = DateTime.Now;

            if (now.DayOfYear >= GetBirthday().DayOfYear)
                return now.Year - GetBirthday().Year;
            else
                return now.Year - GetBirthday().Year - 1;
        }

        public Gender GetGender()
        {
            int flag = Convert.ToInt32(Number.Substring(16, 1));
            return flag == 0 || flag % 2 == 0 ? Gender.Female : Gender.Male;
        }

        public override string ToString()
        {
            return Number;
        }
    }
}
