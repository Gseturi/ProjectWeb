using Microsoft.EntityFrameworkCore;
using static ProjectWeb.Data.UserModel;

namespace ProjectWeb.Data
{
    

        public class ChatAppDbContext: DbContext
        {

       
       

       public  ChatAppDbContext(DbContextOptions<ChatAppDbContext> options): base(options)
        {


        }
        
        public DbSet<Message> Messages { get; set; }
        public DbSet<Chat> Chats { get; set; }

        public DbSet<ChatMemeber> chatMemebers { get; set; }


    }
    
}
