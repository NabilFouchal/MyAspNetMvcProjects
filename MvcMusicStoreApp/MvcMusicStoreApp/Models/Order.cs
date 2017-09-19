using MvcMusicStoreApp.Controllers;
using MvcMusicStoreApp.Utils.ExtensionMethods;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcMusicStoreApp.Models
{
    public class Order
    {
        [HiddenInput]
        public int OrderId { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0: dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime OrderDate { get; set; }

        [Required]      
        [StringLength(150, MinimumLength = 2)]
        [Remote(nameof(OrderController.CheckUserName), "Order")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "First Name", Order = 15000)]
        [StringLength(100, MinimumLength = 2)]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name", Order = 15001)]
        [StringLength(100, MinimumLength = 2)]
        public string LastName { get; set; }

        [Range(typeof(int), "20", "50")]
        public int Age { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string PostalCode { get; set; }

        public string Country { get; set; }

        public string Phone { get; set; }

        [RegularExpression(@"[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}")]
        public string Email { get; set; }

        [System.ComponentModel.DataAnnotations.Compare(nameof(Order.Email))]
        public string EmailConfirm { get; set; }

        [ReadOnly(true)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:c}")]
        public decimal Total { get; set; }

        public List<OrderDetail> OrderDetails { get; set; }
    }
}