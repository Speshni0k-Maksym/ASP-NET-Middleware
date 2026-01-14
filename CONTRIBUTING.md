# CONTRIBUTING.md

## Guidelines

This project follows a simple, beginner-friendly MVC organization suitable for a small Razor/ASP.NET Core web app.

### Project layout
- `Controllers/` - MVC controllers.
- `Models/` - Data models (e.g., `Student`).
- `Views/` - Razor views organized by controller (`Views/Home`, `Views/Students`, `Views/Shared`).
- `Services/` - Simple services and repositories (e.g., `IStudentRepository`, `StudentRepository`).
- `Data/` - Runtime data files. The repository creates `Data/students.json` automatically if it does not exist.

### Data storage
- Use `Data/students.json` as the single source of stored student records in JSON format.
- The repository is responsible for creating the `Data` directory and the `students.json` file at application startup when first used.

### Coding style
- Target framework: .NET 8, C# 12.
- Naming: PascalCase for classes, methods and properties. camelCase for private fields where necessary (prefix with `_`).
- Keep code simple and easy to read. Avoid complex patterns; prefer clear, imperative code for beginners.
- Do not include comments in source files except to mark file boundaries or where necessary for very short explanations.

### UI and styling
- Use Bootstrap classes from the project layout (`Views/Shared/_Layout.cshtml`) for minimal, clean UI.
- Keep custom styling minimal; rely on Bootstrap.

### Services and DI
- Register services in `Program.cs`.
- Use `IStudentRepository` abstraction for data access. Keep implementation simple and file-based.

### Validation
- Use data annotations for model validation and enable client-side validation via unobtrusive scripts.

### Tests and extensions
- This project is intentionally simple. Add tests and advanced features incrementally.

## How to add code
- Create files in the directories above to match names and structure.
- Keep classes small and focused.