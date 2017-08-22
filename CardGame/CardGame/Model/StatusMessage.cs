using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGame.Model
{
    public class StatusMessage
    {
        public int Code { get; set; }
        public string Description { get; set; }
        public object Result { get; set; }
        public int ResultCount { get; set; }

    }
}
