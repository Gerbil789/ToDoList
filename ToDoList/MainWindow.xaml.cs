using DataLayer;
using LogicLayer;
using PresentationLayer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ToDoList
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public ObservableCollection<DataLayer.Task> Tasks { get; set; } = new ObservableCollection<DataLayer.Task>();
        public event PropertyChangedEventHandler PropertyChanged;
        private DataLayer.Task? currentTask = null;
        public DataLayer.Task? CurrentTask
        {
            get { return currentTask; }
            set
            {
                currentTask = value;
                OnPropertyChanged(nameof(CurrentTask));
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            Tasks = TaskManager.GetTasks();
        }

        private void Complete_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = (Button)sender;
            DataLayer.Task task = (DataLayer.Task)clickedButton.DataContext;

            TaskManager.CompleteTask(task);
            Tasks.Remove(task);
            ShowDefault();
        }

        private void ShowNewTaskForm_Click(object sender, RoutedEventArgs e)
        {
            ShowNewTaskForm();
        }

        private void AddTask_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentTask != null && CurrentTask.Title != null && CurrentTask.Title != string.Empty)
            {
                int newTaskId = TaskManager.AddNewTask(CurrentTask);
                if (newTaskId > 0)
                {
                    CurrentTask.TaskId = newTaskId;
                    Tasks.Add(CurrentTask);
                    ShowTaskDetails(CurrentTask);
                }
                else
                {
                    ShowDefault();
                }
            }
        }

        private void EditTask_Click(object sender, RoutedEventArgs e)
        {
            ShowEditTaskForm();
        }

        private void SaveChanges_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentTask != null)
            {
                TaskManager.UpdateTask(CurrentTask);
                ShowTaskDetails(CurrentTask);
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            ShowDefault();
        }

        private void taskListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (taskListBox.SelectedItem is DataLayer.Task selectedTask)
            {
                ShowTaskDetails(selectedTask);
            }
            
        }

        public void ShowTaskDetails(DataLayer.Task task)
        {
            if (CurrentTask != null)
            {
                CurrentTask.IsEditing = false;
                CurrentTask = null;
            }
            CurrentTask = task;
        }

        public void ShowNewTaskForm()
        {
            if (CurrentTask != null)
            {
                CurrentTask.IsEditing = false;
                CurrentTask = null;
            }
            DataLayer.Task newTask = new DataLayer.Task();
            CurrentTask = newTask;
        }

        public void ShowEditTaskForm()
        {
            if (CurrentTask != null)
            {
                CurrentTask.IsEditing = false;
            }

            DataLayer.Task newTask = CurrentTask;
            newTask.IsEditing = true;

            CurrentTask = null; //THIS MUST BE HERE!!!
            CurrentTask = newTask;

        }

        public void ShowDefault()
        {
            if (CurrentTask != null)
            {
                CurrentTask.IsEditing = false;
            }
            taskListBox.SelectedItem = null;
            CurrentTask = null;
        }

    }
}
