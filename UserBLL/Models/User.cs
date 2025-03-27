using FunWithReflextion.Attributes;
using System.Collections.Generic;

namespace FunWithReflextion
{
    [TableName("Users")]
    public class User : BaseModel<User>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
   
    }

}
