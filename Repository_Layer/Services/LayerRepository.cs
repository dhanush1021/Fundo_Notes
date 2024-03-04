using Common_Layer.RequestModel;
using Microsoft.EntityFrameworkCore.Internal;
using Repository_Layer.Context;
using Repository_Layer.Entity;
using Repository_Layer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository_Layer.Services
{
    public class LayerRepository : ILayerInterface
    {
        public readonly EntityContext context;
        public LayerRepository(EntityContext _context)
        {
            context = _context;
        }
        public LabelEntity CreateLabel (CreateLabelOnlyModel model, int UserId)
        {
            LabelEntity label = new LabelEntity();
            if (context.LabelsTable.SingleOrDefault(label => label.labelname == model.labelname && label.UserId == UserId) == null)
            {
                label.labelname = model.labelname;
                label.UserId = UserId;
                context.LabelsTable.Add(label);
                context.SaveChanges();
                return label;
            }
            throw new Exception();
        }
        public bool Check_duplicate(CreateLabelModel model, int userid)
        {
            return context.LabelsTable.SingleOrDefault( label => label.labelname == model.labelname && label.UserId == userid ) == null;
        }
        public LabelEntity create_label_by_note(CreateLabelModel model,int userid)
        {
            LabelEntity label = new LabelEntity();
            if(Check_duplicate(model,userid))
            {
                label.labelname= model.labelname;
                label.UserId = userid;
                label.NoteId = Convert.ToString(model.NoteId);
                context.LabelsTable.Add(label);
                context.SaveChanges ();
                return label;
            }
            throw new Exception();
        }
        public LabelEntity get_label(CreateLabelModel model, int userid)
        {
            return context.LabelsTable.SingleOrDefault(label => label.labelname == model.labelname && label.UserId == userid);
        }
        public LabelEntity Add_note_in_label(CreateLabelModel model, int userid)
        {
            if (!Check_duplicate(model,userid))
            {
                LabelEntity label = get_label(model,userid);
                NoteEntity note = context.NotesTable.SingleOrDefault(note => note.NotesId == model.NoteId && note.Id == userid);   
                if(note != null)
                {
                    if(label.NoteId != null)
                    {
                        List<string> strings = label.NoteId.Split(',').ToList();
                        if (!strings.Contains(Convert.ToString(model.NoteId)))
                            label.NoteId = $"{label.NoteId}," + Convert.ToString(model.NoteId);
                        else
                            throw new Exception("Already the particular Note Exists in the Label ...");
                    }
                    else
                    {
                        label.NoteId = $"{model.NoteId}";
                    }
                    context.SaveChanges();
                    return label;
                }
                throw new Exception("Note does not exist ...");
            }
            throw new Exception();
        }
        public List<LabelEntity> Fetching_Labels(int userid)
        {
            return context.LabelsTable.Where<LabelEntity>(label => label.UserId == userid).ToList();
        }
        public LabelEntity update_label(UpdateLabelModel model, int userid)
        {
            LabelEntity label = context.LabelsTable.SingleOrDefault(label => label.UserId == userid && label.labelname == model.OldLabelName);
            if (label != null)
            {
                if(model.OldLabelName != model.NewLabelName)
                {
                    label.labelname = model.NewLabelName;
                    context.SaveChanges();
                    return label;
                }
                throw new Exception("New label name should be different than the old label name ...");
            }

            throw new Exception("Label does not exist ...");
        }
        public LabelEntity DeleteLabel (int labelid)
        {
            LabelEntity label = context.LabelsTable.SingleOrDefault(label => label.labelid == labelid);
            if(label != null)
            {
                context.LabelsTable.Remove(label);
                context.SaveChanges();
                return label;
            }
            throw new Exception();
        }
        public LabelEntity RemoveNote_fromLabel(RemoveNoteFromLabelModel model)
        {
            LabelEntity label = context.LabelsTable.SingleOrDefault(label => label.labelid == model.labelid);
            if(label != null)
            {
                if(label.NoteId.Length > 0)
                {
                    label.NoteId = string.Join(",", label.NoteId.Split(',').Where(id => id != Convert.ToString(model.NoteId)));
                    context.SaveChanges();
                    return label;
                }
                throw new Exception("Label does not contain Notes ...");
           }
            throw new Exception();
        }
        public List<NoteEntity> get_notes_from_label(int labelid)
        {
            LabelEntity label = context.LabelsTable.SingleOrDefault(label => label.labelid==labelid);
            List<NoteEntity> Notes = new List<NoteEntity>();
            if(label != null)
            {
                List<string> notesid = label.NoteId.Split(',').ToList();
                foreach(string i in notesid)
                {
                    NoteEntity Note = context.NotesTable.SingleOrDefault(note => note.NotesId == Convert.ToInt32(i));
                    Notes.Add(Note);
                }
                return Notes;
            }
            throw new Exception();
        }
    }
}
