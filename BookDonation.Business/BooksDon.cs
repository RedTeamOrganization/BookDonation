using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookDonation.Business
{
    public class BooksDon
    {
        public int CalcNumDonate(int availBooks, int BooksDonate)
        {
            availBooks = availBooks + BooksDonate;
            return availBooks;

        }
    }
}
