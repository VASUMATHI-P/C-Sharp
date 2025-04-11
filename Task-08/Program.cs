using System;
using System.Collections.Generic;
using System.Linq;

namespace GenericRepositoryPattern
{
    public interface IEntity {
        int Id { get; set; }
    }

    public interface IRepository<T> where T: IEntity {
        void Add(T entity);
        T GetById(int id);
        List<T> GetAll();
        void Update(int id, T updatedEntity);
        void Delete(int id);
    }

    public class InMemoryRepository<T>: IRepository<T> where T: IEntity {
        private List<T> _entities = new List<T>();
        private int _idCounter = 0;
        public void Add(T entity) {
            entity.Id = ++_idCounter;
            _entities.Add(entity);
        }

        public T GetById(int id) {
           return _entities.FirstOrDefault(e => e.Id == id);
        }

        public List<T> GetAll() {
            return _entities;
        }

        public void Update(int id, T updatedEntity) {
            updatedEntity.Id = id;
            _entities.RemoveAll(e => e.Id == id);
            _entities.Add(updatedEntity);
        }

        public void Delete(int id) {
            _entities.RemoveAll(e => e.Id == id);
        }
    }
    public class Student:IEntity {
        public int Id { get; set; }
        public string name { get; set; }
        public int age { get; set; }
    }

    class Program {
        static void Main(string[] args) {
            IRepository<Student> repo = new InMemoryRepository<Student>();
            bool flag = true;
            while(flag) {
                Console.WriteLine("-------------------------------------------------");
                Console.WriteLine("1. Add a Student");
                Console.WriteLine("2. Get a Student by Id");
                Console.WriteLine("3. Get All students");
                Console.WriteLine("4. Update a Student");
                Console.WriteLine("5. Delete a Student");
                Console.WriteLine("6. Exit");
                Console.WriteLine("-------------------------------------------------");
                Console.Write("Enter your choice: ");
                int choice = Convert.ToInt32(Console.ReadLine());
            
                switch (choice) {
                    case 1:
                        Student student = new Student();
                        Console.Write("Enter Student name: ");
                        student.name = Console.ReadLine();
                        Console.Write("Enter Student age: ");
                        student.age = Convert.ToInt32(Console.ReadLine());
                        repo.Add(student);
                        System.Console.WriteLine("Student Added successfully");
                        break;

                    case 2:
                        Console.Write("Enter Student Id: ");
                        int id = Convert.ToInt32(Console.ReadLine());
                        student = repo.GetById(id);
                        if (student != null) {
                            Console.WriteLine($"Id: {student.Id}  Name: {student.name}  Age: {student.age}");
                        } else {
                            Console.WriteLine("Student not found");
                        }
                        break;

                    case 3: 
                        List<Student> students = repo.GetAll();
                        foreach (var item in students) {
                            Console.WriteLine($"Id: {item.Id}  Name: {item.name}  Age: {item.age}");
                        }
                        break;

                    case 4:
                        Console.Write("Enter the Student Id to update:");
                        id = Convert.ToInt32(Console.ReadLine());
                        student = repo.GetById(id);
                        if (student != null) {
                            Console.Write("Enter Student name: ");
                            student.name = Console.ReadLine();
                            Console.Write("Enter Student age: ");
                            student.age = Convert.ToInt32(Console.ReadLine());
                            repo.Update(id, student);
                            Console.WriteLine("Student updated successfully");
                        } else {
                            Console.WriteLine("Student not found");
                        }
                        break;

                    case 5:
                        Console.Write("Enter the Student Id to delete:");
                        id = Convert.ToInt32(Console.ReadLine());
                        student = repo.GetById(id);
                        if (student != null) {
                            repo.Delete(id);
                            Console.WriteLine("Student Deleted Successfully");
                        } else {
                            Console.WriteLine("Student not found");
                        }
                        break;

                    case 6:
                        Console.Write("Exiting :)");
                        flag = false;
                        break;

                    default:
                        Console.WriteLine("Invalid choice");
                        break;
                }
            }
            
        }
    }
}
