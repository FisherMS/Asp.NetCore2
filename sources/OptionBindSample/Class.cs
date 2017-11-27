using System.Collections.Generic;

namespace OptionBindSample
{
    public class Class
    {
        public int ClassNo { get; set; }

        public string ClassDesc { get; set; }


        public List<Student> Students { get; set; }

    }


    public class Student
    {
        public string Name { get; set; }

        public int Age { get; set; }

    }
}