using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteelheadChecking
{
    public class CheckingAccount
    {
        public double balance;
        public string user;

       public CheckingAccount(string user, double balance)
        {
            this.user = user;
            this.balance = balance;
        }     

        private List<Transaction> allTransactions = new List<Transaction>();

        public void RecordDeposit(string filefolder, string username, double amount, DateTime date, string note, double currentbalance)
        {
            var deposit = new Transaction(amount, date, note);
            allTransactions.Add(deposit);

            string entrydate = DateTime.Now.ToString("ddMMyyyyHHmmss");
            string filename = filefolder + username + entrydate + ".txt";
            using (StreamWriter sw = new StreamWriter(File.Create(filename)))
            {
                sw.WriteLine(username);
                sw.WriteLine();
                sw.WriteLine(entrydate);
                sw.WriteLine("+" + amount);
                sw.WriteLine(currentbalance);
                sw.WriteLine(note);
                sw.Close();
            }
        }

        public void RecordWithdrawal(string filefolder, string username, double amount, DateTime date, string note, double currentbalance)
        {
            var withdraw = new Transaction(amount, date, note);
            allTransactions.Add(withdraw);

            string entrydate = DateTime.Now.ToString("ddMMyyyyHHmmss");
            string filename = filefolder + username + entrydate + ".txt";
            using (StreamWriter sw = new StreamWriter(File.Create(filename)))
            {
                sw.WriteLine(username);
                sw.WriteLine();
                sw.WriteLine(entrydate);
                sw.WriteLine("-" + amount);
                sw.WriteLine(currentbalance);
                sw.WriteLine(note);
                sw.Close();
            }
        }
    }
}
