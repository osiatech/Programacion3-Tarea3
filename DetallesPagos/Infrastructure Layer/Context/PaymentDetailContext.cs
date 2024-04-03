
using Microsoft.EntityFrameworkCore;
using PaymentDetails.Infrastructure.Models;

namespace PaymentDetails.Infrastructure.Context
{
    public class PaymentDetailContext : DbContext
    {
        public PaymentDetailContext(DbContextOptions dbContextOptions) : base(dbContextOptions) { }
        public DbSet<PaymentDetail> PaymentDetails { get; set; }
    }
}