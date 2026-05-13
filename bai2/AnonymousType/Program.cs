var students = new List<Student>
{
new Student { Id = 1, Name = "An", Class = "CTK42" },
new Student { Id = 2, Name = "Bình", Class = "CTK42" },
new Student { Id = 3, Name = "Chi", Class = "CTK43" }
};
var courses = new List<Course>
{
new Course { Id = 1, Name = "Lập trình C#", Credits = 3 },
new Course { Id = 2, Name = "Cơ sở dữ liệu", Credits = 4 }
};
var scores = new List<Score>
{
new Score { StudentId = 1, CourseId = 1, Value = 8.5 },
new Score { StudentId = 2, CourseId = 1, Value = 9.0 },
new Score { StudentId = 3, CourseId = 2, Value = 6.5 }
};

//1
var result = scores
.GroupBy(s => s.StudentId)
.Select(g => new
{
StudentId = g.Key,
GPA = g.Average(x => x.Value)
});
Console.WriteLine("===/ DANH SÁCH SINH VIÊN & GPA /===");
foreach (var item in result)
{
Console.WriteLine($"StudentId: {item.StudentId}, GPA: {item.GPA}");
}

//2
var excellentStudents = result
.Where(x => x.GPA >= 8.0);
Console.WriteLine("===/ DANH SÁCH SINH VIÊN GPA >= 8.0 /===");
foreach (var item in excellentStudents)
{
Console.WriteLine($"StudentId: {item.StudentId}, GPA: {item.GPA}");
}

//3
var query =
from s in students
join sc in scores on s.Id equals sc.StudentId
join c in courses on sc.CourseId equals c.Id
select new
{
StudentName = s.Name,
CourseName = c.Name,
Score = sc.Value
};
Console.WriteLine("===/ DANH SÁCH SINH VIÊN & MÔN HỌC & ĐIỂM /===");
foreach (var item in query)
{
Console.WriteLine($"StudentName: {item.StudentName}, CourseName: {item.CourseName}, Score: {item.Score}");
}

//4
var classReport =
from st in students
join sc in scores on st.Id equals sc.StudentId
group sc by st.Class into g
select new
{
ClassName = g.Key,
AvgGPA = g.Average(x => x.Value),
StudentCount = g.Select(x => x.StudentId).Distinct().Count()
};
Console.WriteLine("===/ THONG KE THEO LOP /===");
foreach (var item in classReport)
{
Console.WriteLine($"ClassName: {item.ClassName}, AvgGPA: {item.AvgGPA}, StudentCount: {item.StudentCount}");
}

//5
var topStudents = result
.OrderByDescending(x => x.GPA)
.Take(3);
Console.WriteLine("===/ TOP 3 SINH VIÊN CÓ GPA CAO NHẤT /===");
foreach (var item in topStudents)
{
Console.WriteLine($"StudentId: {item.StudentId}, GPA: {item.GPA}");
}


