using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectWeb.Data
{
    public class UserModel : IdentityUser
    {

        public class ChatMemeber
        {

            [ForeignKey("User")]
            public string Id { get; set; }

            [ForeignKey("Chat")]
            public string ChatId { get; set; }

            public ApplicationUser User { get; set; }

            public Chat Chat { get; set; }
        }


        public class Request
        {     
            
            public string Id { get; set; }

            public string RequestpersonId { get; set; }

            public bool Accepted { get; set; } = false;
            
            
            
            
            
            
            
        }





        public class Chat
        {
            public string ChatId { get; set; }
            public string? Name { get; set; }

            // Navigation property
            public ICollection<Message> Messages { get; set; } = new List<Message>();
        }

        public class Message
        {
            public int? MessageId { get; set; }

            public string? Text { get; set; }
            public DateTime? DateTime { get; set; }

            // Foreign keys
            [ForeignKey("User")]
            public string Id { get; set; } // Change the type to string

            [ForeignKey("Chat")]
            public string ChatId { get; set; }

            // Navigation properties
            public ApplicationUser User { get; set; }
            public Chat Chat { get; set; }
        }
    }
}
