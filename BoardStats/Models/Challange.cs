using System.ComponentModel.DataAnnotations;

namespace BoardStats.Models
{
    public class Challange
    {
        [Key]
        public int ChallangeId { get; set; }

        public string ChallangeName { get; set; }
        public string UserName { get; set; }

        public string UserId { get; set; }
    }
}
