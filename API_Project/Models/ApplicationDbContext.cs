
using Microsoft.EntityFrameworkCore;
using API_Ticketier.Models.Entities;
namespace API_Ticketier.Models
{
    public class ApplicationDbContext :DbContext
    {

        public DbSet<Ticket> Tickets {  get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {


        }
    }
}
