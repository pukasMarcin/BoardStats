using System.ComponentModel.DataAnnotations;

namespace BoardStats.Models
{
    public class Challange
    {
        [Key]
        public int ChallangeId { get; set; }

        public string ChallangeName { get; set; }
    }
}
