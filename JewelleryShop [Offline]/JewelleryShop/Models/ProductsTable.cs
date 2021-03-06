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
    
    public partial class ProductsTable
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ProductsTable()
        {
            this.Ordered_Products = new HashSet<Ordered_Products>();
        }
    
        public int productId { get; set; }
        public string productName { get; set; }
        public string productDescription { get; set; }
        public Nullable<int> productPrice { get; set; }
        public Nullable<int> categoryId { get; set; }
        public string productImage { get; set; }
    
        public virtual ProductCategoryTable ProductCategoryTable { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Ordered_Products> Ordered_Products { get; set; }
    }
}
