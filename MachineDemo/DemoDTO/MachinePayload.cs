using System;
using System.Collections.Generic;
using System.Text;

namespace DemoDTO
{
     public class Payload
    {
        public Guid Machine_Id { get; set; }

        public Guid Id { get; set; }

        public DateTime Timestamp { get; set; }

        public string Status { get; set; }
    }

  
}
