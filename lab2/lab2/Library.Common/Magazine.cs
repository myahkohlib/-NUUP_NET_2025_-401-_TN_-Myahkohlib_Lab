using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    internal class Magazine : Item
    {
         
        public string Publisher { get; set; }
        public int IssueNumber { get; set; }

       
        public Magazine(string title, int year, string publisher, int issueNumber)
            : base(title, year)
        {
            Publisher = publisher;
            IssueNumber = issueNumber;
        }

        
        public override void DisplayInfo()
        {
            base.DisplayInfo();
            Console.WriteLine($"Видавець: {Publisher}, Номер випуску: {IssueNumber}");
        }
    
    }
}
