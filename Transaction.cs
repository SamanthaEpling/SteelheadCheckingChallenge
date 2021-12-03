using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteelheadChecking
{
    public class Transaction
    {
        public double Amount { get; }
        public DateTime Date { get; }
        public string Notes { get; }

        public Transaction(double amount, DateTime date, string note)
        {
            this.Amount = amount;
            this.Date = date;
            this.Notes = note;
        }
    }
}
