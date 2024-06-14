using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace FirstWeb.Models
{
    public class GuestDbContext : DbContext
    {
        public GuestDbContext() : base()
        {
        }

        public DbSet<Guest> Guests { get; set; }
    }

    [Table("Guest")]
    public class Guest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreateDate { get; set; }
        public string Message { get; set; }
    }
}