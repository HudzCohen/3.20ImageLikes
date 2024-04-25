using Microsoft.EntityFrameworkCore;

namespace _3._20_ImageLikes.Data
{
    public class Image
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ImagePath { get; set; }
        public int Likes { get; set; }
        public DateTime Date { get; set; }
    }

    public class ImageDataContext : DbContext
    {
        private readonly string _connectionString;

        public ImageDataContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
        public DbSet<Image> Images { get; set; }
    }
}
