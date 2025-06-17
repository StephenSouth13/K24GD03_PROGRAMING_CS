using System;
using System.Collections;
using System.Collections.Generic;

namespace Lab4
{
    internal class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Student(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }
    }



}
