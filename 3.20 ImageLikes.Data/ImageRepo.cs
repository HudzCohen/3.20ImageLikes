using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3._20_ImageLikes.Data
{
    public class ImageRepo
    {
        private readonly string _connectionString;

        public ImageRepo(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Image> GetImages()
        {
            using var context = new ImageDataContext(_connectionString);
            return context.Images.ToList();
        }

        public Image GetImageById(int id)
        {
            using var context = new ImageDataContext(_connectionString);
            return context.Images.FirstOrDefault(i => i.Id == id);
        }

        public void AddImage(Image image)
        {
            using var context = new ImageDataContext(_connectionString);
            context.Images.Add(image);
            context.SaveChanges();
        }

        public void IncrementLikes(int id)
        {
            using var context = new ImageDataContext(_connectionString);
            context.Database.ExecuteSqlInterpolated($"UPDATE Images SET Likes = Likes + 1 WHERE Id = {id}");
        }

        public int GetLikesByImageId(int id)
        {
            using var context = new ImageDataContext(_connectionString);
            var image = context.Images.FirstOrDefault(i => i.Id == id);
            return image.Likes;
        }

    }
}
