using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace ToDoList
{
    public class MyDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate DefaultTemplate { get; set; }
        public DataTemplate TaskFormTemplate { get; set; }
        public DataTemplate TaskDetailsTemplate { get; set; }
        public DataTemplate EditTaskTemplate { get; set; }
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if(item == null)
            {
                return DefaultTemplate;
            }

            if (item is DataLayer.Task task)
            {
                if(task.TaskId != 0)
                {
                    if(task.IsEditing)
                    {
                        return EditTaskTemplate;
                    }
                    return TaskDetailsTemplate;
                }
                else{
                    return TaskFormTemplate;
                }
                
            }

            return DefaultTemplate;
        }
    }

}
