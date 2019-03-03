using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Class_Homework_2_29_2019.Models
{
    public class ToDoItem
    {
        public string Title { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? CompletedDate { get; set; }
        public int CategoryId { get; set; }
        public int? Id { get; set; }
    }
}