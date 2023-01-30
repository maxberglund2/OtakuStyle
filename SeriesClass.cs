using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace GYA___Max
{
    internal class SeriesClass : ListClass
    {
        private int lenghtEpisodic;
        public int LenghtEpisodic { get; set; }

        public SeriesClass(string title, string status, string score, int lenghtEpisodic)
        {
            Title = title;
            Status = status;
            Score = score;
            LenghtEpisodic = lenghtEpisodic;
        }
        public override string[] GetInfoInToArray()
        {
            string[] info = { Title, Status, Score.ToString(), LenghtEpisodic.ToString() };
            return info;
        }
    }
}
