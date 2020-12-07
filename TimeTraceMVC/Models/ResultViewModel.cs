using System;

namespace TimeTraceMVC.Models
{
    public class ResultViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public TimeSpan RaceTime { get; set; }
    }
}
