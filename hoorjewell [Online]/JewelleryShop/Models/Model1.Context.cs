﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class hoorjewellEntities : DbContext
    {
        public hoorjewellEntities()
            : base("name=hoorjewellEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<EmployeeTable> EmployeeTables { get; set; }
        public virtual DbSet<Ordered_Products> Ordered_Products { get; set; }
        public virtual DbSet<OrdersTable> OrdersTables { get; set; }
        public virtual DbSet<ProductCategoryTable> ProductCategoryTables { get; set; }
        public virtual DbSet<ProductsTable> ProductsTables { get; set; }
        public virtual DbSet<RolesTable> RolesTables { get; set; }
        public virtual DbSet<UsersTable> UsersTables { get; set; }
        public virtual DbSet<OrderView> OrderViews { get; set; }
    
        public virtual ObjectResult<GetOrders_Result> GetOrders(Nullable<int> orderId)
        {
            var orderIdParameter = orderId.HasValue ?
                new ObjectParameter("orderId", orderId) :
                new ObjectParameter("orderId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetOrders_Result>("GetOrders", orderIdParameter);
        }
    }
}
