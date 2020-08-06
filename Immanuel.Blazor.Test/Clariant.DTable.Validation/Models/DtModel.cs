using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clariant.DTable.Validation
{
    public class DtModel
    {
        public string src { get; set; }
        public string dest { get; set; }
        public Dictionary<string, List<string>> srctblclmns { get; set; } = new Dictionary<string, List<string>>();
        public Dictionary<string, List<string>> desttblclmns { get; set; } = new Dictionary<string, List<string>>();
        public DtblMdl comparetbl { get; set; } = new DtblMdl();
        public List<string> srctbls { get; set; } = new List<string>();
        public List<string> desttbls { get; set; } = new List<string>();
    }
    public class DtblMdl
    {
        public string srcname { get; set; }
        public string destname { get; set; }
    }
}