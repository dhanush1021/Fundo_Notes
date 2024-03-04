using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace Repository_Layer.Entity
{
    public class LabelEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int labelid { get; set; }
        public string labelname { get; set; }
        public string NoteId { get; set; }
        [ForeignKey("Notes")]
        public int UserId { get; set; }
        [JsonIgnore]
        public virtual UserEntity Notes { get; set; }
    }
}
