using System.ComponentModel;

namespace HackathonWebsite.DTO.Case
{
    public class CaseDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = "Default Title";
        public string Description { get; set; } = "Default Description";
        public string Author { get; set; } = "Default Author";
        public int? HackathonId { get; set; }
    }
}
