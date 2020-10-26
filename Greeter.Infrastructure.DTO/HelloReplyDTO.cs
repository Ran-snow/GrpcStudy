using System;
using System.Collections.Generic;
using System.Text;

namespace Greeter.Infrastructure.DTO
{
    public class HelloReplyDTO
    {
        public string Message { get; set; }
        public DateTime Start { get; set; }
        public int? Age { get; set; }
        public byte[] SmallPicture { get; set; }
        public IList<string> Roles { get; set; }
        public IDictionary<string, string> Attributes { get; set; }
    }
}
