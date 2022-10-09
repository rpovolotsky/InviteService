using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace InviteService.Models
{
    [Table("invite")]
    public class Invite
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id"), Key]
        public long Id { get; set; }
        [Column("phone")]
        public string Phone { get; set; }
        [Column("message_text")]
        public string MessageText { get; set; }
        [Column("api_id")]
        public int ApiId { get; set; }
        [Column("event_date")]
        public DateTime EventDate { get; set; } 
    }
}
