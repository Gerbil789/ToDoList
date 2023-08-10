using DataLayer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public class TaskManager
    {
        public static ObservableCollection<DataLayer.Task> GetTasks()
        {
            return DBAccess.GetTasks(UserManager.LoggedUser.UserId);
        }

        public static int AddNewTask(DataLayer.Task task)
        {
            int newId = DBAccess.AddNewTask(task, UserManager.LoggedUser.UserId);

            return newId;
        }

        public static void CompleteTask(DataLayer.Task task)
        {
            task.IsCompleted = true;
            DBAccess.UpdateTask(task);
        }

        public static void UpdateTask(DataLayer.Task task)
        {
            DBAccess.UpdateTask(task);
        }

        public static void DeleteTask(DataLayer.Task task)
        {
            DBAccess.DeleteTask(task.TaskId);
        }
    }
}
