﻿namespace LAB14.Models
{
    public class Enrollment
    {
        public int EnrollmentId { get; set; }
        public int StudentId { get; set; }    
        public int CourseId { get; set; }     
        public DateTime Date { get; set; }
        public bool IsActive { get; set; }


        public Student Student { get; set; }
        public Course Course { get; set; }
    }
}
