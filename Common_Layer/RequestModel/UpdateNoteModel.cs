using System;
using System.Collections.Generic;
using System.Text;

namespace Common_Layer.RequestModel
{
    public class UpdateNoteModel
    {
        public int NotesId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Reminder { get; set; }
    }
}
