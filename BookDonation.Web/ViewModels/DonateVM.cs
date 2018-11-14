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
        public virtual int Id { get; set; }
        //[Required(ErrorMessage = "Username is required")]
        //[StringLength(50)]
        //public virtual int UserId { get; set; }
        [Required]
        public virtual int GenreId { get; set; }
        [Required(ErrorMessage = "Author name is required")]
        public virtual int AuthorId{ get; set; }
        [Required]
        public virtual string Title { get; set; }

        public virtual string ISBN { get; set; }
        public virtual byte[] Image { get; set; }
        [Required]
        public virtual int NumBookDonated { get; set; }

    }
}