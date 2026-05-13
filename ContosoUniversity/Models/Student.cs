using System;
using System.Collections.Generic;

namespace ContosoUniversity.Models;

public partial class Student
{
    public int Id { get; set; }

    public string LastName { get; set; } = null!;

    public string FirstMidName { get; set; } = null!;

    public DateTime EnrollmentDate { get; set; }

    public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
}
