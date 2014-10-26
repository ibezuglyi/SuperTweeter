using System;

namespace Web.App.Controllers
{
    public class UserTweetDto
    {
        public string Text { get; set; }
        public string Timestamp { get; set; }
        public string From { get; set; }

        public UserTweetDto(string text, long ticks, string from)
        {
            Text = text;
            From = @from;
            Timestamp = new DateTime(ticks).ToString();
        }
    }
}