using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace GYA___Max
{
    internal class MoviesClass : ListClass
    {
        private int hours;
        private int minutes;
        public int Hours { get; set; }
        public int Minutes { get; set; }

        public MoviesClass(string title, string status, string score, int hours, int minutes)
        {
            Title = title;
            Status = status;
            Score = score;
            Hours = hours;
            Minutes = minutes;
        }
        public override string[] GetInfoInToArray()
        {
            string[] info = { Title, Status, Score.ToString(), Hours.ToString(), Minutes.ToString() };
            return info;
        }
    }
}
