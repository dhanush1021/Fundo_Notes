using System;
using System.Collections.Generic;
using System.Text;

namespace Common_Layer.RequestModel
{
    public class CreateNoteModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Reminder { get; set; }
        public string Color { get; set; }
        public string Image { get; set; }         
    }
}
