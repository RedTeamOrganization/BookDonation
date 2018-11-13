using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookDonation.DB.Models
{
    public class Books
    {
        public virtual int Id { get; set; }
        public virtual int UserId { get; set; }
        public virtual int GenreId { get; set; }
        public virtual Genres Genres { get; set; }
        public virtual int AuthorId { get; set; }
        public virtual Authors Authors { get; set; }
        public virtual string Title { get; set; }
        public virtual string ISBN { get; set; }
        public virtual byte[] Image { get; set; }
        public virtual int QuantityAvailable { get; set; }
        public virtual string QuantityReserved { get; set; }
    }
}
