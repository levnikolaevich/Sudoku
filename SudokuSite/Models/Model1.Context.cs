﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SudokuSite.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class db_sudokuEntities : DbContext
    {
        public db_sudokuEntities()
            : base("name=db_sudokuEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Field> Field { get; set; }
        public virtual DbSet<Game> Game { get; set; }
        public virtual DbSet<Point> Point { get; set; }
        public virtual DbSet<User> User { get; set; }
    }
}