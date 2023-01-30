using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace GYA___Max
{
    internal class ListClass
    {
        public string Title { get; set; }
        public string Status { get; set; }
        public string Score { get; set; }

        public virtual string[] GetInfoInToArray()
        {
            string[] info = { Title, Status, Score.ToString() };
            return info;
        }
    }
}
