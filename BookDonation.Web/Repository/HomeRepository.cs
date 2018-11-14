using BookDonation.DB.Models;
using BookDonation.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace BookDonation.Web.Repository
{
    public class HomeRepository
    {
        private readonly BookDonationDB db = new BookDonationDB();
        public int UploadImageInDataBase(HttpPostedFileBase file,  DonateVM donateModel)
        {
            donateModel.Image = ConvertToBytes(file);
            var Content = new Books
            {
                Id = donateModel.Id,
                //UserId =donateModel.UserId,
                GenreId =donateModel.GenreId,
                AuthorId =donateModel.AuthorId,
                Title = donateModel.Title,
                ISBN = donateModel.ISBN,
                QuantityAvailable =donateModel.NumBookDonated,
                
                Image = donateModel.Image
            };
            db.Book.Add(Content);
            int i = db.SaveChanges();
            if (i == 1)
            {
                return 1;
            }
            else
            {
                return 0;
            }

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