using System.Collections.Generic;

namespace LevelData
{
    public class Details
    {
        public string Question { get; set; }
        public string Answer { get; set; }
        public string TimeofDay { get; set; }
        public string TimeofYear { get; set; }
    }

    public class Data
    {
        public List<Details> Datas { get; set; }
    }
}
