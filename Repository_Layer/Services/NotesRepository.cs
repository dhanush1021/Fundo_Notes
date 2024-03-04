using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Common_Layer.RequestModel;
using Repository_Layer.Context;
using Repository_Layer.Entity;
using Repository_Layer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository_Layer.Services
{
    public class NotesRepository : INoteRepository
    {
        private readonly EntityContext context;
        public NotesRepository(EntityContext _context)
        {
            context = _context;
        }
        public NoteEntity AddNote(CreateNoteModel model, int UserID)
        {
            NoteEntity note = new NoteEntity();
            note.Id = UserID;
            note.Title = model.Title;
            note.Description = model.Description;
            note.Reminder = model.Reminder;
            note.Color = model.Color;
            note.Image = image(model.Image, UserID, note.NotesId);
            note.IsArchive = false;
            note.IsPin = false;
            note.IsTrash = false;
            note.CreatedAt = DateTime.Now.ToString("dd-MM-yyyy HH:mm");
            note.LastUpdatedAt = DateTime.Now.ToString("dd-MM-yyyy HH:mm");
            context.NotesTable.Add(note);
            context.SaveChanges();
            return note;
        }
        public List<NoteEntity> GetAll(int id)
        {
            return context.NotesTable.Where<NoteEntity>(user => user.Id == id).ToList();
        }
        public NoteEntity note(int id, int userid)
        {
            return context.NotesTable.SingleOrDefault(notes => notes.Id == userid && notes.NotesId == id);
        }
        public NoteEntity update(UpdateNoteModel model,int id)
        {
            NoteEntity note = context.NotesTable.SingleOrDefault(notes => notes.Id == id && notes.NotesId == model.NotesId);
            if (note != null)
            {
                if(model.Title != "")
                note.Title = model.Title;
                if(model.Description != "")
                note.Description= model.Description;
                if(model.Reminder != null)
                note.Reminder = model.Reminder;
                note.LastUpdatedAt = DateTime.Now.ToString("dd-MM-yyyy HH:mm");
                context.SaveChanges();
                return note;
            }
            else
            {
                throw new Exception();
            }
        }
        public NoteEntity is_pin (int id, int userid)
        {
            NoteEntity notes = note(id, userid);
            if (notes != null)
            {
                if(notes.IsPin)
                    notes.IsPin = false;
                else
                    notes.IsPin = true;
                notes.LastUpdatedAt = DateTime.Now.ToString("dd-MM-yyyy HH:mm");
                context.SaveChanges();
                return notes;
            }
            else
            {
                throw new Exception();
            }
        }
        public NoteEntity is_archive(int id, int userid)
        {
            NoteEntity notes = note(id, userid);
            if (notes != null)
            {
                if (notes.IsArchive)
                    notes.IsArchive = false;
                else
                    notes.IsArchive = true;
                notes.LastUpdatedAt = DateTime.Now.ToString("dd-MM-yyyy HH:mm");
                context.SaveChanges();
                return notes;
            }
            else
            {
                throw new Exception();
            }
        }
        public NoteEntity is_trash(int id, int userid)
        {
            NoteEntity notes = note(id, userid);
            if (notes != null)
            {
                if (notes.IsTrash)
                    notes.IsTrash = false;
                else
                    notes.IsTrash = true;
                notes.LastUpdatedAt = DateTime.Now.ToString("dd-MM-yyyy HH:mm");
                context.SaveChanges();
                return notes;
            }
            else
            {
                throw new Exception();
            }
        }
        public NoteEntity Color(string  color,int userid, int id)
        {
            NoteEntity notes = note(id, userid);
            if(notes != null)
            {
                notes.Color = color;
                notes.LastUpdatedAt = DateTime.Now.ToString("dd-MM-yyyy HH:mm");
                context.SaveChanges();
                return notes;
            }
            else
            {
                throw new Exception();
            }
        }
        public NoteEntity AddRemeinder(DateTime date, int userid, int id)
        {
            NoteEntity notes = note(id, userid);
            if (notes != null)
            {
                notes.Reminder = date;
                notes.LastUpdatedAt = DateTime.Now.ToString("dd-MM-yyyy HH:mm");
                context.SaveChanges();
                return notes;
            }
            else
            {
                throw new Exception();
            }
        }
        public string image(string image_url,int userid,int noteid)
        {
            Account acc = new Account("demirsba2", "772131477512463", "_qOl76kXHBiE2uVkm3DQtOZa2cQ");
            Cloudinary cloud = new Cloudinary(acc);
            ImageUploadParams image = new ImageUploadParams()
            {
                File = new FileDescription(image_url),
                PublicId = "Notes" + $"/{userid}{noteid}"
            };
            ImageUploadResult result = cloud.Upload(image);
            return result.Url.ToString();
        }
        public NoteEntity upload_image(string image_url, int userid, int id)
        {
            NoteEntity notes = note(id, userid);
            if (notes != null)
            {
                notes.LastUpdatedAt = DateTime.Now.ToString("dd-MM-yyyy HH:mm");
                notes.Image = image(image_url,userid,id);
                context.SaveChanges();
                return notes;
            }
            else
            {
                throw new Exception();
            }
        }
        public NoteEntity delete_note(int userid,int noteid)
        {
            NoteEntity notes = note(noteid, userid);
            context.NotesTable.Remove(notes);
            context.SaveChanges();
            return notes;
        }
    }
}
