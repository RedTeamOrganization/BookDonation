using BookDonation.DB.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookDonation.Web.ViewModels
{
    public class DonateVM
    {
        [Required]
        public int Id { get; set; }
        //[Required(ErrorMessage = "Username is required")]
        //[StringLength(50)]
        //public virtual int UserId { get; set; }
        [Required]
        [Display(Name ="Genre")]
        public int GenreId { get; set; }

        [Required(ErrorMessage = "Author name is required")]
        public int AuthorId{ get; set; }

        [Display(Name = "Author")]
        public string AuthorName { get; set; }
        public virtual Authors Authors { get; set; }

        public string GenreName { get; set; }
        public virtual Genres genres { get; set; }
        [Required]
        public string Title { get; set; }

        public string ISBN { get; set; }

        public byte[] Image { get; set; }
        

        public virtual int QuantityAvailable { get; set; }
        public virtual string QuantityReserved { get; set; }
        public virtual DateTime PickUpDate { get; set; }
        [Display(Name = "Qty Donated")]
        public int NumBookDonated { get; set; }


       
    }
}