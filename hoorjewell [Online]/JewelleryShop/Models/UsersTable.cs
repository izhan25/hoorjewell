//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace JewelleryShop.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class UsersTable
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public UsersTable()
        {
            this.OrdersTables = new HashSet<OrdersTable>();
        }
    
        public int userId { get; set; }

        [Required(ErrorMessage ="required")]
        public string userName { get; set; }

        [Required(ErrorMessage = "required")]
        public string userEmail { get; set; }

        [Required(ErrorMessage = "required")]
        public string userContact { get; set; }


        [Required(ErrorMessage = "required")]
        public string userPassword { get; set; }

        [NotMapped]
        [Required(ErrorMessage = "required")]
        [Compare("userPassword", ErrorMessage = "password doesn't match")]
        public string ConfirmPassword { get; set; }


        [Required(ErrorMessage = "required")]
        public string userAddress { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrdersTable> OrdersTables { get; set; }
    }
}
