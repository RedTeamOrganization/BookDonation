using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookDonation.DB.Models
{
    public class Transactions
    {
        public virtual int Id { get; set; }
        public virtual int ActionByUserId{ get; set; }
        public virtual int BookId { get; set; }
        public virtual string ActionId { get; set; }
        public virtual Actions Actions { get; set; }
        public virtual DateTime Date { get; set; }
    }
}
