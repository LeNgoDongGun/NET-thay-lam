var students = new List<Student>
{
new Student
{
Name = "An",
Profile = new Profile
{
Email = "an@gmail.com",
Phone = "0901234567"
}
},
new Student
{
Name = "Bình",
Profile = new Profile
{
Email = null,
Phone = "0912345678"
}
},
new Student
{
Name = "Tâm",
Profile = null
}
};

foreach (var s in students)
{
Console.WriteLine($"Tên: {s.Name}");
Console.WriteLine(
$"Phone: {s.Profile?.Phone ?? "Chưa có Phone"}"
);
Console.WriteLine(
$"Email: {s.Profile?.Email ?? "Chưa có Email"}"
);
Console.WriteLine(
$"Profile: {(s.Profile == null ? "Chưa có Profile" : "Đã có Profile")}"
);
Console.WriteLine("---------------------");
}