using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GISApi.Models
{
    public class WordMatchedPhrase
    {
        public int charStartInFirstPar { get; set; }
        public int charEndInLastPar { get; set; }

        public int firstCharParOccurance { get; set; }
        public int lastCharParOccurance { get; set; }
    }
}
