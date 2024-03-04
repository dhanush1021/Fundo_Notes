using Common_Layer.RequestModel;
using Repository_Layer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository_Layer.Interfaces
{
    public interface INoteRepository
    {
        public NoteEntity AddNote(CreateNoteModel model, int UserID);
        public List<NoteEntity> GetAll(int id);
        public NoteEntity update(UpdateNoteModel model, int id);
        public NoteEntity is_pin(int id, int userid);
        public NoteEntity is_archive(int id, int userid);
        public NoteEntity is_trash(int id, int userid);
        public NoteEntity AddRemeinder(DateTime date, int userid, int id);
        public NoteEntity Color(string color, int userid, int id);
        public NoteEntity upload_image(string image_url, int userid, int id);
        public NoteEntity delete_note(int noteid, int userid);
    }
}
