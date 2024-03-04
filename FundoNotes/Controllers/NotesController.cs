using Common_Layer.RequestModel;
using Common_Layer.ResponseModel;
using Manager_Layer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository_Layer.Entity;
using System;
using System.Collections.Generic;

namespace FundoNotes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly INoteManager noteManager;
        public NotesController(INoteManager _noteManager)
        {
            noteManager = _noteManager;
        }
        [Authorize]
        [HttpPost]
        [Route("Add")]
        public ActionResult Add(CreateNoteModel model)
        {
            int userid = Convert.ToInt32(User.FindFirst("User Id").Value);
            try
            {
                return Ok(new ResponseModel<NoteEntity>
                {
                    Success = true,
                    Message = "Data Added Successfully ...",
                    data = noteManager.AddNote(model,userid)
                }) ;
            }
            catch (Exception ex)
            {
                return Ok(new ResponseModel<NoteEntity>
                {
                    Success = false,
                    Message = ex.Message,
                    data = null
                });
            }
        }
        [Authorize]
        [HttpGet]
        [Route("Display")]
        public ActionResult Fetching_Data()
        {
            try
            {
                return Ok(new ResponseModel<List<NoteEntity>>
                {
                    Success = true,
                    Message = "Data Fetched Successfully ...",
                    data = noteManager.GetAll(Convert.ToInt32(User.FindFirst("User Id").Value))
                }) ;
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseModel<NoteEntity>
                {
                    Success = false,
                    Message = ex.Message,
                    data = null
                });
            }
        }
        [Authorize]
        [HttpPut]
        [Route("Update")]
        public ActionResult Update_Note(UpdateNoteModel model)
        {
            try
            {
                return Ok(new ResponseModel<NoteEntity>
                {
                    Success = true,
                    Message = "Data updated Successfully ...",
                    data = noteManager.update(model,Convert.ToInt32(User.FindFirst("User Id").Value))
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseModel<NoteEntity>
                {
                    Success = false,
                    Message = ex.Message,
                    data = null
                });
            }
        }
        [Authorize]
        [HttpPut]
        [Route("pin")]
        public ActionResult Update_IsPin(int id)
        {
            try
            {
                return Ok(new ResponseModel<NoteEntity>
                {
                    Success = true,
                    Message = "Pinned Successfully ...",
                    data = noteManager.is_pin(id, Convert.ToInt32(User.FindFirst("User Id").Value))
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseModel<NoteEntity>
                {
                    Success = false,
                    Message = ex.Message,
                    data = null
                });
            }
        }
        [Authorize]
        [HttpPut]
        [Route("archive")]
        public ActionResult Update_IsArchive(int id)
        {
            try
            {
                return Ok(new ResponseModel<NoteEntity>
                {
                    Success = true,
                    Message = "Archived Successfully ...",
                    data = noteManager.is_archive(id, Convert.ToInt32(User.FindFirst("User Id").Value))
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseModel<NoteEntity>
                {
                    Success = false,
                    Message = ex.Message,
                    data = null
                });
            }
        }
        [Authorize]
        [HttpPut]
        [Route("trash")]
        public ActionResult Update_IsTrash(int id)
        {
            try
            {
                return Ok(new ResponseModel<NoteEntity>
                {
                    Success = true,
                    Message = "Note has been Trashed ...",
                    data = noteManager.is_trash(id, Convert.ToInt32(User.FindFirst("User Id").Value))
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseModel<NoteEntity>
                {
                    Success = false,
                    Message = ex.Message,
                    data = null
                });
            }
        }
        [Authorize]
        [HttpPut]
        [Route("AddReminder")]
        public ActionResult Add_Reminder(int noteid,DateTime date)
        {
            try
            {
                return Ok(new ResponseModel<NoteEntity>
                {
                    Success = true,
                    Message = "reminder Added Successfully ...",
                    data = noteManager.AddRemeinder(date, Convert.ToInt32(User.FindFirst("User Id").Value),noteid)
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseModel<NoteEntity>
                {
                    Success = false,
                    Message = ex.Message,
                    data = null
                });
            }
        }
        [Authorize]
        [HttpPut]
        [Route("color")]
        public ActionResult Color(string color, int noteid)
        {
            try
            {
                return Ok(new ResponseModel<NoteEntity>
                {
                    Success = true,
                    Message = "colour updated Successfully ...",
                    data = noteManager.Color(color, Convert.ToInt32(User.FindFirst("User Id").Value), noteid)
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseModel<NoteEntity>
                {
                    Success = false,
                    Message = ex.Message,
                    data = null
                });
            }
        }
        [Authorize]
        [HttpPut]
        [Route("image")]
        public ActionResult ImageUpload(string image_url, int noteid)
        {
            try
            {
                return Ok(new ResponseModel<NoteEntity>
                {
                    Success = true,
                    Message = "Image updated Successfully ...",
                    data = noteManager.upload_image(image_url, Convert.ToInt32(User.FindFirst("User Id").Value), noteid)
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseModel<NoteEntity>
                {
                    Success = false,
                    Message = ex.Message,
                    data = null
                });
            }
        }
        [Authorize]
        [HttpDelete]
        [Route("delete")]
        public ActionResult Delete_Note(int noteid)
        {
            try
            {
                return Ok(new ResponseModel<NoteEntity>
                {
                    Success = true,
                    Message = "Note Deleted Successfully ...",
                    data = noteManager.delete_note(Convert.ToInt32(User.FindFirst("User Id").Value), noteid)
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseModel<NoteEntity>
                {
                    Success = false,
                    Message = ex.Message,
                    data = null
                });
            }
        }
    }
}
