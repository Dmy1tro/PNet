using PNet_Pz_1.Models;
using System;

namespace PNet_Pz_1.Tasks
{
    public class Delegates
    {
        public delegate void ShowInfoAboutTeacher(Teacher teacher);

        public delegate void ShowInfoAboutStudent(Student student);

        public delegate Person CreatePerson(string name, int age);

        public void Example()
        {
            var createStudent = new CreatePerson(CreateStudent); // ковариантность

            var showStudentInfo = new ShowInfoAboutStudent(GetPersonInfo); // контравариантность

            var showTeacherInfo = new ShowInfoAboutTeacher(GetPersonInfo); // контравариантность

            var teacher = new Teacher
            {
                Name = "Ihor",
                Age = 30,
                Position = "Lector",
                Experience = 3
            };
            var student = new Student
            {
                Name = "Adam",
                Age = 20,
                Speciality = "PZPI",
                Course = 3
            };

            showTeacherInfo(teacher);
            showTeacherInfo.Invoke(teacher);

            showStudentInfo(student);
            showStudentInfo.Invoke(student);

            var createdStudent = createStudent("Irina", 19);

            showStudentInfo(createdStudent as Student);
        }

        public void GetPersonInfo(Person person)
        {
            var info = $"Name: {person.Name}\n" +
                       $"Age: {person.Age}";

            Console.WriteLine(info);
        }

        public static Student CreateStudent(string name, int age)
        {
            return new Student
            {
                Name = name,
                Age = age,
                Speciality = "KIU",
                Course = 1
            };
        }
    }
}
