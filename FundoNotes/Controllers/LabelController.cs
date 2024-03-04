using Common_Layer.RequestModel;
using Common_Layer.ResponseModel;
using Manager_Layer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository_Layer.Entity;
using Repository_Layer.Interfaces;
using Manager_Layer.Interfaces;
using System;
using System.Collections.Generic;

namespace FundoNotes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LabelController : ControllerBase
    {
        public readonly ILayerManager layer;
        public LabelController(ILayerManager _layer)
        {
            layer = _layer;
        }
        [HttpPost]
        [Authorize]
        [Route("createlabel")]
        public ActionResult Create_Label(CreateLabelOnlyModel model)
        {
            try
            {
                return Ok(new ResponseModel<LabelEntity>
                {
                    Success = true,
                    Message = "Only Label created Successful",
                    data = layer.CreateLabel(model, Convert.ToInt32(User.FindFirst("User Id").Value))
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseModel<LabelEntity>
                {
                    Success = false,
                    Message = ex.Message,
                    data = null
                });
            }
        }
        [HttpPost]
        [Authorize]
        [Route("Createlabelnote")]
        public ActionResult CreateLabel(CreateLabelModel model)
        {
            try
            {
                return Ok(new ResponseModel<LabelEntity>
                {
                    Success = true,
                    Message = "Label has been created through note successfully ...",
                    data = layer.create_label_by_note(model, Convert.ToInt32(User.FindFirst("User Id").Value))
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseModel<LabelEntity>
                {
                    Success = false,
                    Message = ex.Message,
                    data = null
                });
            }
        }
        [HttpGet]
        [Authorize]
        [Route("Fetchinglabel")]
        public ActionResult Fetchlabel()
        {
            try
            {
                return Ok(new ResponseModel<List<LabelEntity>>
                {
                    Success = true,
                    Message = "Label Fetch Successful",
                    data = layer.Fetching_Labels(Convert.ToInt32(User.FindFirst("User Id").Value))
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseModel<LabelEntity>
                {
                    Success = false,
                    Message = ex.Message,
                    data = null
                });
            }
        }
        [HttpPost]
        [Authorize]
        [Route("Addnoteinlabel")]
        public ActionResult Add_Note_label(CreateLabelModel model)
        {
            try
            {
                return Ok(new ResponseModel<LabelEntity>
                {
                    Success = true,
                    Message = "Note added to Label Successful",
                    data = layer.Add_note_in_label(model,Convert.ToInt32(User.FindFirst("User Id").Value))
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseModel<LabelEntity>
                {
                    Success = false,
                    Message = ex.Message,
                    data = null
                });
            }
        }
        [HttpPut]
        [Authorize]
        [Route("updatelabel")]
        public ActionResult Update_LabelName(UpdateLabelModel model)
        {
            try
            {
                return Ok(new ResponseModel<LabelEntity>
                {
                    Success = true,
                    Message = "Label name update Successful",
                    data = layer.update_label(model, Convert.ToInt32(User.FindFirst("User Id").Value))
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseModel<LabelEntity>
                {
                    Success = false,
                    Message = ex.Message,
                    data = null
                });
            }
        }
        [HttpDelete]
        [Authorize]
        [Route("deletelabel")]
        public ActionResult Delete_Label(int labelid)
        {
            try
            {
                return Ok(new ResponseModel<LabelEntity>
                {
                    Success = true,
                    Message = "Label Delete Successful",
                    data = layer.DeleteLabel(labelid)
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseModel<LabelEntity>
                {
                    Success = false,
                    Message = ex.Message,
                    data = null
                });
            }
        }
        [HttpPut]
        [Authorize]
        [Route("Removelabelfromnote")]
        public ActionResult Remove_label_from_note(RemoveNoteFromLabelModel model)
        {
            try
            {
                return Ok(new ResponseModel<LabelEntity>
                {
                    Success = true,
                    Message = "Label removed from note Successfully",
                    data = layer.RemoveNote_fromLabel(model)
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseModel<LabelEntity>
                {
                    Success = false,
                    Message = ex.Message,
                    data = null
                });
            }
        }
        [HttpGet]
        [Authorize]
        [Route("{labelid}",Name = "labellednotes")]
        public ActionResult Get_Notes_of_Label(int labelid)
        {
            try
            {
                return Ok(new ResponseModel<List<NoteEntity>>
                {
                    Success = true,
                    Message = "All the Notes of particular Label Fetched Successfully",
                    data = layer.get_notes_from_label(labelid)
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseModel<LabelEntity>
                {
                    Success = false,
                    Message = ex.Message,
                    data = null
                });
            }
        }
    }
}
