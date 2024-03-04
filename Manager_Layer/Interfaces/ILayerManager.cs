using Common_Layer.RequestModel;
using Repository_Layer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Manager_Layer.Interfaces
{
    public interface ILayerManager
    {
        public LabelEntity CreateLabel(CreateLabelOnlyModel model, int UserId);
        public LabelEntity create_label_by_note(CreateLabelModel model, int userid);
        public List<LabelEntity> Fetching_Labels(int userid);
        public LabelEntity Add_note_in_label(CreateLabelModel model, int userid);
        public LabelEntity update_label(UpdateLabelModel model, int userid);
        public LabelEntity DeleteLabel(int labelid);
        public LabelEntity RemoveNote_fromLabel(RemoveNoteFromLabelModel model);
        public List<NoteEntity> get_notes_from_label(int labelid);
    }
}
