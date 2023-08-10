using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DataLayer
{
    public class UserEntity
    {
        [Key]
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public List<TaskEntity> Tasks { get; set; } = new List<TaskEntity>();
    }

    public class TaskEntity
    {
        [Key]
        public int TaskId { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public DateTime? Deadline { get; set; }
        public int Priority { get; set; } = 3;
        public bool IsCompleted { get; set; } = false;

        public int UserId { get; set; }
    }

    public class ApplicationDbContext : DbContext
    {
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<TaskEntity> Tasks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=Test");
        }
        //dev only, this function should not be used in production
        public void DeleteAllData()
        {
            Users.RemoveRange(Users);
            Tasks.RemoveRange(Tasks);
            SaveChanges();
        }

        public string getDBLocation()
        {
            var connectionString = Database.GetDbConnection().ConnectionString;
            return $"Database location: {connectionString}";
        }

        public int AddNewTask(TaskEntity newTaskEntity)
        {
            Tasks.Add(newTaskEntity);
            SaveChanges();
            return newTaskEntity.TaskId;
            
        }

    }
}
