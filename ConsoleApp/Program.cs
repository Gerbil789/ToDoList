using DataLayer;
using System;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using LogicLayer;

//Add-Migration Fix -project DataLayer
//Update-Database -project DataLayer

namespace ConsoleApp
{
    class Program
    {
        static void Main()
        {


            
            TaskManager taskManager = new TaskManager();
            UserManager userManager = new UserManager();

            using var db = new ApplicationDbContext();
            Console.WriteLine(db.getDBLocation());
            //db.DeleteAllData();

            /*
            userManager.Login("vojta", "123456");
            Console.WriteLine(UserManager.LoggedUser.Username + " " + UserManager.LoggedUser.UserId);

            using var db = new ApplicationDbContext();
            DataLayer.Task newTask = new DataLayer.Task { Title = "Bruh", Description = "asdasda", Deadline = null };
            //taskManager.AddNewTask(newTask);

            //if (db.AddNewTask(newTask, UserManager.LoggedUser.UserId))
            //{
            //    Console.WriteLine("New task added.");
            //}
            //else{
            //    Console.WriteLine("Adding task failed");
            //}


            

            List<TaskEntity> tasks = db.Tasks.ToList();

            foreach (TaskEntity task in tasks)
            {
                Console.WriteLine(task.Title + ": " + task.Description + " (" + task.TaskId + ")");
            }



            //using var db = new ApplicationDbContext();

            //DROP DATABASE !!!
            //db.Database.EnsureDeleted();









            //

            
            // Create
            Console.WriteLine("Inserting a new User");
            db.Add(new DataLayer.User { Username = "Vojta", Password = "123456" });
            db.SaveChanges();

            Console.WriteLine("Querying for a User");
            var user = db.Users
                .OrderBy(b => b.UserId)
                .First();

            Console.WriteLine(user.Username + user.Password);

            Console.WriteLine("Inserting a new Task");
            db.Add(new DataLayer.Task { Title = "Finish this!" , User = user});
            db.SaveChanges();

            // Read
            Console.WriteLine("Querying for a Task");
            var task = db.Tasks
                .OrderBy(b => b.TaskId)
                .First();

            Console.WriteLine(task.Title);


            // Delete
            Console.WriteLine("Delete the task");
            db.Remove(task);
            db.SaveChanges();
            */
        }
    }
}
