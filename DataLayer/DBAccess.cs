using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class User
    {
        public static User? FromUserEntity(UserEntity entity)
        {
            if (entity == null)
            {
                return null;
            }

            return new User
            {
                UserId = entity.UserId,
                Username = entity.Username,
                Password = entity.Password,
            };
        }
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
    public class Task : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public static Task? FromUserEntity(TaskEntity entity)
        {
            if (entity == null)
            {
                return null;
            }

            return new Task
            {
                TaskId = entity.TaskId,
                Title = entity.Title,
                Description = entity.Description,
                Deadline = entity.Deadline,
                Priority = entity.Priority,
                IsCompleted = entity.IsCompleted,
            };
        }
        private int taskId = 0;
        public int TaskId
        {
            get { return taskId; }
            set
            {
                if (taskId != value)
                {
                    taskId = value;
                    NotifyPropertyChanged(nameof(TaskId));
                }
            }
        }

        private string title = string.Empty;
        public string Title
        {
            get { return title; }
            set
            {
                if (title != value)
                {
                    title = value;
                    NotifyPropertyChanged(nameof(Title));
                }
            }
        }
        private string? description;
        public string? Description
        {
            get { return description; }
            set
            {
                if (description != value)
                {
                    description = value;
                    NotifyPropertyChanged(nameof(Description));
                }
            }
        }
        private DateTime? deadline;
        public DateTime? Deadline
        {
            get { return deadline; }
            set
            {
                if (deadline != value)
                {
                    deadline = value;
                    NotifyPropertyChanged(nameof(Deadline));
                }
            }
        }

        private int priority = 2;
        public int Priority
        {
            get { return priority; }
            set
            {
                if (priority != value)
                {
                    priority = value;
                    NotifyPropertyChanged(nameof(Priority));
                }
            }
        }

        private bool isCompleted = false;
        public bool IsCompleted
        {
            get { return isCompleted; }
            set
            {
                if (isCompleted != value)
                {
                    isCompleted = value;
                    NotifyPropertyChanged(nameof(IsCompleted));
                }
            }
        }

        private bool isEditing = false;
        public bool IsEditing
        {
            get { return isEditing; }
            set
            {
                if (isEditing != value)
                {
                    isEditing = value;
                    NotifyPropertyChanged(nameof(IsEditing));
                }
            }
        }


        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
    public class DBAccess
    {
        public static User? AddNewUser(string username, string password)
        {
            using(var db = new ApplicationDbContext())
            {
                try
                {
                    var newUserEntity = new UserEntity
                    {
                        Username = username,
                        Password = password
                    };

                    db.Users.Add(newUserEntity);
                    db.SaveChanges();

                    var newUser = User.FromUserEntity(newUserEntity);
                    return newUser;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
            
        }

        public static User? GetUser(int id)
        {
            using (var db = new ApplicationDbContext())
            {
                var userEntity = db.Users.Find(id);
                var user = User.FromUserEntity(userEntity);

                return user;
            }
                
        }

        public static User? GetUser(string username, string password)
        {
            using (var db = new ApplicationDbContext())
            {
                var userEntity = db.Users.SingleOrDefault(u => u.Username == username && u.Password == password);
                var user = User.FromUserEntity(userEntity);
                return user;
            }
            
        }

        public static ObservableCollection<Task> GetTasks(int userId)
        {
            using (var db = new ApplicationDbContext())
            {
                var taskEntities = db.Tasks.Where(t => t.UserId == userId && t.IsCompleted == false).ToList();
                var tasks = new ObservableCollection<Task>(taskEntities.Select(taskEntity => new Task
                {
                    TaskId = taskEntity.TaskId,
                    Title = taskEntity.Title,
                    Description = taskEntity.Description,
                    Deadline = taskEntity.Deadline,
                    Priority = taskEntity.Priority,
                    IsCompleted = taskEntity.IsCompleted
                }));

                return tasks;
            }
            
        }

        public static int AddNewTask(Task task, int userId)
        {
            using (var db = new ApplicationDbContext())
            {
                TaskEntity newTaskEntity = new TaskEntity
                {
                    Title = task.Title,
                    Description = task.Description,
                    Deadline = task.Deadline,
                    Priority = task.Priority,
                    IsCompleted = task.IsCompleted,
                    UserId = userId
                };
                
                int newTaskId = db.AddNewTask(newTaskEntity);
                return newTaskId;
            }
        }

        public static bool DeleteTask(int taskId)
        {
            using (var db = new ApplicationDbContext())
            {
                var taskToDelete = db.Tasks.SingleOrDefault(t => t.TaskId == taskId);

                if (taskToDelete == null)
                {
                    // Task with the given ID not found
                    return false;
                }

                db.Tasks.Remove(taskToDelete);
                db.SaveChanges();
                return true;
            } 
        }

        public static bool UpdateTask(Task task)
        {
            using (var db = new ApplicationDbContext())
            {
                var taskEntityToUpdate = db.Tasks.SingleOrDefault(t => t.TaskId == task.TaskId);

                if (taskEntityToUpdate == null)
                {
                    // Task with the given ID not found
                    return false;
                }

                taskEntityToUpdate.Title = task.Title;
                taskEntityToUpdate.Description = task.Description;
                taskEntityToUpdate.Deadline = task.Deadline;
                taskEntityToUpdate.Priority = task.Priority;
                taskEntityToUpdate.IsCompleted = task.IsCompleted;

                db.SaveChanges();
                return true;
            }
        }
    }
}
