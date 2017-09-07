using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using System.ComponentModel;

namespace ProjectHA
{
    [Table("ALLINFO")]
    public class AllInfo
    {
        public string NAME { get; set; }
        public string KEY { get; set; }

    }
}
