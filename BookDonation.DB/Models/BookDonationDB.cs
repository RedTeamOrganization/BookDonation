using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace BookDonation.DB.Models
{
    public class BookDonationDB : DbContext
    {


        public BookDonationDB() : base("name=BookDonationDB")
        {
        }

        public DbSet<Actions> Action { get; set; }

        public DbSet<Authors> Author { get; set; }

        public DbSet<Books> Book { get; set; }

        public DbSet<Genres> Genre { get; set; }

        public DbSet<Transactions> Transaction { get; set; }

    }
}


