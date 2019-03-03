using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Class_Homework_2_29_2019.Models
{
    public class Categories
    {
        public Categories()
        {
            ToDoItems = new List<ToDoItem>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ToDoItem> ToDoItems { get; set; }
    }
}