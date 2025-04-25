namespace HackathonWebsite.DTO.Hackaton
{
    public class HackatonDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int ParticipantsCount { get; set; } = 0;
    }
}
