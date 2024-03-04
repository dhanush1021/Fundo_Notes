using Common_Layer.RequestModel;
using Manager_Layer.Interfaces;
using Repository_Layer.Entity;
using Repository_Layer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Manager_Layer.Services
{
    public class NoteManager : INoteManager
    {
        private readonly INoteRepository repository;
        public NoteManager(INoteRepository _repository)
        {
            repository = _repository;
        }
        public NoteEntity AddNote(CreateNoteModel model, int UserID)
        {
            return repository.AddNote(model, UserID);
        }
        public List<NoteEntity> GetAll(int id)
        {
            return repository.GetAll(id);
        }
        public NoteEntity update(UpdateNoteModel model, int id)
        {
            return repository.update(model, id);
        }
        public NoteEntity is_pin(int id, int userid)
        {
            return repository.is_pin(id, userid);
        }
        public NoteEntity is_archive(int id, int userid)
        {
            return repository.is_archive(id, userid);
        }
        public NoteEntity is_trash(int id, int userid)
        {
            return repository.is_trash(id, userid);
        }
        public NoteEntity AddRemeinder(DateTime date, int userid, int id)
        {
            return repository.AddRemeinder(date, userid, id);
        }
        public NoteEntity Color(string color, int userid, int id)
        {
            return repository.Color(color, userid, id);
        }
        public NoteEntity upload_image(string image_url, int userid, int id)
        {
            return repository.upload_image(image_url, userid, id);
        }
        public NoteEntity delete_note(int noteid, int userid)
        {
            return repository.delete_note(noteid, userid);
        }
    }
}
