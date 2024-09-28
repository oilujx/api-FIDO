using Microsoft.EntityFrameworkCore;

namespace bibliotecaAPI.Entities
{
    public class DBContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //if (!optionsBuilder.IsConfigured)
            //{
            //    optionsBuilder.UseMySQL("server=localhost;port=3306;user=root;password=;database=database");
            //}
        }

    }
}
