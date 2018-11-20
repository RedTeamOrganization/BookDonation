using BookDonation.DB.Models;
using BookDonation.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
//using System.Web.ModelBinding;

namespace BookDonation.Web.Repository
{
    public class HomeRepository
    {
        private readonly BookDonationDB db = new BookDonationDB();

        public int UploadImageInDataBase(HttpPostedFileBase file, DonateVM donateModel)
        {
            int i;
            Books existingBook = null;
            existingBook = db.Book.Where(b => b.Title == donateModel.Title).FirstOrDefault();

            if (existingBook != null)
            {
                existingBook.QuantityAvailable += donateModel.NumBookDonated;
                db.Entry(existingBook).State = EntityState.Modified;
                i = db.SaveChanges();
            }
            else
            {
                donateModel.Image = ConvertToBytes(file);

                var Content = new Books
                {
                    Id = donateModel.Id,
                    //UserId =donateModel.UserId,
                    Genres = db.Genre.Find(donateModel.GenreId),
                    Authors = db.Author.Find(donateModel.AuthorId),

                    Title = donateModel.Title,
                    ISBN = donateModel.ISBN,
                    QuantityAvailable = donateModel.NumBookDonated,
                    QuantityReserved = "No",
                    Image = donateModel.Image
                };

                db.Book.Add(Content);
                i = db.SaveChanges();
            }

            return i;
        }


        public byte[] ConvertToBytes(HttpPostedFileBase image)
        {
            byte[] imageBytes = null;
            BinaryReader reader = new BinaryReader(image.InputStream);
            imageBytes = reader.ReadBytes((int)image.ContentLength);
            return imageBytes;
        }

    }
}