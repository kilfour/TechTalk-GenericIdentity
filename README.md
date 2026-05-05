# The Generic Identity
> Gegeven het volgende (speelgoed) domein:  
**Entities:**  
```csharp
public class Course
{
    public Guid Id { get; }
    public string Title { get; }
    public Course(string title) => (Id, Title) = (Guid.NewGuid(), title);
    public override bool Equals(object? obj)
    {
        if (obj is null || obj.GetType() != GetType()) return false;
        var other = (Course)obj;
        return Id == other.Id;
    }
    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}
```
```csharp
public class Student
{
    public Guid Id { get; }
    public string Name { get; }
    public Student(string name) => (Id, Name) = (Guid.NewGuid(), name);
    public override bool Equals(object? obj)
    {
        if (obj is null || obj.GetType() != GetType()) return false;
        var other = (Student)obj;
        return Id == other.Id;
    }
    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}
```
**Service:**  
```csharp
public class CoursesRegistry
{
    // --------------------------------------------------------------------------------------
    // COURSES
    // --
    private readonly List<Course> courses = [new Course("Web Dev"), new Course("Pottery")];
    public IReadOnlyList<Course> Courses => courses;
    public Course GetCourseByTitle(string title) => courses.Single(a => a.Title == title);
    // --------------------------------------------------------------------------------------
    // STUDENTS
    // --
    private readonly List<Student> students = [new Student("Ayende Rahien"), new Student("Uncle Bob")];
    public IReadOnlyList<Student> Students => students;
    public Student GetStudentByName(string name) => students.Single(a => a.Name == name);
    // --------------------------------------------------------------------------------------
    // ENROLLMENTS
    // --
    public void Enroll(Guid courseId, Guid studentId)
    {
        var course = courses.SingleOrDefault(a => a.Id == courseId);
        if (course == null)
            throw new ArgumentException("Course not found", nameof(courseId));
        var student = students.SingleOrDefault(a => a.Id == studentId);
        if (course == null)
            throw new ArgumentException("Student not found", nameof(studentId));
        // Do stuff, a.k.a. enroll the student.
    }
}
```
## The Problem
```csharp
var coursesRegistry = new CoursesRegistry();
var course = coursesRegistry.GetCourseByTitle("Web Dev");
var student = coursesRegistry.GetStudentByName("Ayende Rahien");
coursesRegistry.Enroll(course.Id, student.Id);
```
**Primitive obsession**  
We gebruiken een primitief type voor iets dat eigenlijk domeinbetekenis heeft.  
Guid is technisch correct, maar domeinmatig te vaag.  

> `Course.Id` en `Student.Id` zijn niet hetzelfde concept.  
## The Solution
### Step One Typed Ids
```csharp
public record CourseId(Guid Value);
```
```csharp
public record StudentId(Guid Value);
```
En nu kunnen onze entities deze gebruiken.  
```csharp
public class Course
{
    public CourseId Id { get; }
    public string Title { get; }
    public Course(string title) => (Id, Title) = (new CourseId(Guid.NewGuid()), title);
    public override bool Equals(object? obj)
    {
        if (obj is null || obj.GetType() != GetType()) return false;
        var other = (Course)obj;
        return Id == other.Id;
    }
    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}
```
```csharp
public class Student
{
    public StudentId Id { get; }
    public string Name { get; }
    public Student(string name) => (Id, Name) = (new StudentId(Guid.NewGuid()), name);
    public override bool Equals(object? obj)
    {
        if (obj is null || obj.GetType() != GetType()) return false;
        var other = (Student)obj;
        return Id == other.Id;
    }
    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}
```
En de service wordt duidelijker.  
```csharp
public void Enroll(CourseId courseId, StudentId studentId)
{
    var course = courses.SingleOrDefault(a => a.Id == courseId);
    if (course == null)
        throw new ArgumentException("Course not found", nameof(courseId));
    var student = students.SingleOrDefault(a => a.Id == studentId);
    if (student == null)
        throw new ArgumentException("Student not found", nameof(studentId));
    // Do stuff, a.k.a. enroll the student.
}
```
**Gebruik:**  
```csharp
var coursesRegistry = new CoursesRegistry();
var course = coursesRegistry.GetCourseByTitle("Web Dev");
var student = coursesRegistry.GetStudentByName("Ayende Rahien");
coursesRegistry.Enroll(course.Id, student.Id);
```
Verwisselen van `Student.Id`en `Course.Id` is nu onmogelijk *at compile-time*.
Een ID is niet zomaar data. Een ID verwijst naar iets.

`Id<Course>` zegt:

> Dit is een verwijzing naar een Course

`Id<Student>` zegt:

> Dit is een verwijzing naar een Student

Dat is *domein informatie*.

We hebben ook de *run-time bug* omgezet naar een *compile-error*.
> Compile errors zijn goedkoper dan runtime bugs.  
### Step Two Removing Duplication
Cool, maar nu moeten we een extra klasse maken voor elke entiteit, ... less cool.  

Deze types vertegenwoordigen allen hetzelfde idee.  
> Een `Id` voor een specifiek domeintype.

Alleen het domeintype verandert.

*Generics* to the rescue !  
#### Generic Identity
```csharp
public record Id<T>(Guid Value)
{
    public override string ToString()
    {
        return $"{typeof(T).Name}: {Value}";
    }
}
```
**Gebruik:**  
```csharp
Id<Course> courseId = new(Guid.NewGuid());
Id<Student> studentId = new(Guid.NewGuid());
```
**Belangrijk:** `courseId` en `studentId` zijn ook hier van een verschillent type.  
#### Refactoring the Entities
```csharp
public class Course
{
    public Id<Course> Id { get; }
    public string Title { get; }
    public Course(string title) => (Id, Title) = (new Id<Course>(Guid.NewGuid()), title);
    public override bool Equals(object? obj)
    {
        if (obj is null || obj.GetType() != GetType()) return false;
        var other = (Course)obj;
        return Id == other.Id;
    }
    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}
```
```csharp
public class Student
{
    public Id<Student> Id { get; }
    public string Name { get; }
    public Student(string name) => (Id, Name) = (new Id<Student>(Guid.NewGuid()), name);
    public override bool Equals(object? obj)
    {
        if (obj is null || obj.GetType() != GetType()) return false;
        var other = (Student)obj;
        return Id == other.Id;
    }
    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}
```
#### And the Service
> Hier passen we enkel de *function signature* aan.  
```csharp
public void Enroll(Id<Course> courseId, Id<Student> studentId)
{
    var course = courses.SingleOrDefault(a => a.Id == courseId);
    if (course == null)
        throw new ArgumentException("Course not found", nameof(courseId));
    var student = students.SingleOrDefault(a => a.Id == studentId);
    if (student == null)
        throw new ArgumentException("Student not found", nameof(studentId));
    // Do stuff, a.k.a. enroll the student.
}
```
### Step Three Centralising Identity Behaviour
Nu kunnen we ook een eenvoudige *base class* maken die deze functionaliteit verenigt, want:
- Een `Student`  **is** een entiteit.
- Een `Course`  **is** een entiteit.   
#### Domain Entity
```csharp
public abstract class DomainEntity<T>
{
    // Simplifying Id assignment for demo purposes.
    // In the wild, this is likely handled by a data store.
    public Id<T> Id { get; } = new Id<T>(Guid.NewGuid());
    public override bool Equals(object? obj)
    {
        if (obj is null)
            return false;
        if (obj.GetType() != GetType())
            return false;
        var other = (DomainEntity<T>)obj;
        return Id == other.Id;
    }
    public override int GetHashCode()
        => Id.GetHashCode();
}
```
Dan kunnen we het gedrag van *Id equality* centraliseren en zowel `Student` als `Course` laten overerven van `Domain Entity`:  
```csharp
public class Course(string title) : DomainEntity<Course>
{
    public string Title { get; } = title;
}
```
```csharp
public class Student(string name) : DomainEntity<Student>
{
    public string Name { get; } = name;
}
```
Geen wijzigingen nodig in de service.  
## The Conclusion
*Geen nieuwe features toegevoegd.*
> **Vele potentiële bugs vermeden.**  
