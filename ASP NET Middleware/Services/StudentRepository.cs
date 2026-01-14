namespace ASP_NET_Middleware.Services
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text.Json;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Hosting;
    using ASP_NET_Middleware.Models;

    public class StudentRepository : IStudentRepository
    {
        private readonly string _filePath;
        private readonly object _lock = new();

        public StudentRepository(IWebHostEnvironment env)
        {
            var dataDir = Path.Combine(env.ContentRootPath, "Data");
            if (!Directory.Exists(dataDir)) Directory.CreateDirectory(dataDir);
            _filePath = Path.Combine(dataDir, "students.json");
            if (!File.Exists(_filePath)) File.WriteAllText(_filePath, "[]");
        }

        public Task AddAsync(Student student)
        {
            lock (_lock)
            {
                var json = File.ReadAllText(_filePath);
                var list = JsonSerializer.Deserialize<List<Student>>(json) ?? new List<Student>();
                list.Add(student);
                File.WriteAllText(_filePath, JsonSerializer.Serialize(list, new JsonSerializerOptions { WriteIndented = true }));
            }

            return Task.CompletedTask;
        }

        public Task<IEnumerable<Student>> GetAllAsync()
        {
            lock (_lock)
            {
                var json = File.ReadAllText(_filePath);
                var list = JsonSerializer.Deserialize<List<Student>>(json) ?? new List<Student>();
                return Task.FromResult<IEnumerable<Student>>(list);
            }
        }
    }
}