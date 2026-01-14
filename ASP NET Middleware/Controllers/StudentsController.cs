namespace ASP_NET_Middleware.Controllers
{
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using ASP_NET_Middleware.Models;
    using ASP_NET_Middleware.Services;

    public class StudentsController : Controller
    {
        private readonly IStudentRepository _repo;

        public StudentsController(IStudentRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Create() => View(new Student());

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(Student student)
        {
            if (!ModelState.IsValid) return View(student);
            await _repo.AddAsync(student);
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Export()
        {
            var students = await _repo.GetAllAsync();
            var sb = new StringBuilder();
            sb.AppendLine("Name,Age,Group,IsGrade");
            foreach (var s in students)
            {
                sb.AppendLine($"{Escape(s.Name)},{s.Age},{Escape(s.Group)},{s.IsGrade}");
            }
            var bytes = System.Text.Encoding.UTF8.GetBytes(sb.ToString());
            return File(bytes, "text/csv", "students.csv");
        }

        private static string Escape(string value)
        {
            if (string.IsNullOrEmpty(value)) return string.Empty;
            if (value.Contains(",") || value.Contains("\"") || value.Contains("\n"))
            {
                return $"\"{value.Replace("\"", "\"\"")}\"";
            }
            return value;
        }
    }
}