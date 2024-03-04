using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Common_Layer.RequestModel
{
    public class RemoveNoteFromLabelModel
    {
        public int labelid { get; set; }
        public int NoteId { get; set; }
    }
}
