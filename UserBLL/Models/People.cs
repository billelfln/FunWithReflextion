﻿using FunWithReflextion.Attributes;

namespace FunWithReflextion.Models
{
    [TableName("People")]
    public class People : BaseModel<People>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public string Phone { get; set; }
    }
}