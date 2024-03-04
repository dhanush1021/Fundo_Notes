using Common_Layer.RequestModel;
using Manager_Layer.Interfaces;
using Repository_Layer.Entity;
using Repository_Layer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Manager_Layer.Services
{
    public class LayerManager : ILayerManager
    {
        public readonly ILayerInterface layer;
        public LayerManager(ILayerInterface _layer)
        {
            layer = _layer;
        }
        public LabelEntity CreateLabel(CreateLabelOnlyModel model, int UserId)
        {
            return layer.CreateLabel(model, UserId);
        }
        public LabelEntity create_label_by_note(CreateLabelModel model, int userid)
        {
            return layer.create_label_by_note(model,userid);
        }
        public List<LabelEntity> Fetching_Labels(int userid)
        {
            return layer.Fetching_Labels(userid);
        }
        public LabelEntity Add_note_in_label(CreateLabelModel model, int userid)
        {
            return layer.Add_note_in_label(model,userid);
        }
        public LabelEntity update_label(UpdateLabelModel model, int userid)
        {
            return layer.update_label(model,userid);
        }
        public LabelEntity DeleteLabel(int labelid)
        {
            return layer.DeleteLabel(labelid);
        }
        public LabelEntity RemoveNote_fromLabel(RemoveNoteFromLabelModel model)
        {
            return layer.RemoveNote_fromLabel(model);
        }
        public List<NoteEntity> get_notes_from_label(int labelid)
        {
            return layer.get_notes_from_label(labelid);
        }
    }
}
