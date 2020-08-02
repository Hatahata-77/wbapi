using System;
using System.Collections.Generic;

namespace wbapi.Models
{
    public partial class Message
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public string UpdateDay { get; set; }
        public string UpdateTime { get; set; }
    }
}
