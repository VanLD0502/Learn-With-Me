

using System.Globalization;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;

namespace Learn
{
    class LinQ {

        public static void Run() {
            
            List<Subject> subjects = new List<Subject>(){
                new Subject(1, "OOP"),
                new Subject(2, "DSA"),
                new Subject(3, "CSLT")

            };

            List<Role> roles = new List<Role>()
            {
                new Role(1, "Student"),
                new Role(2, "Teacher"),
                new Role(3, "Principle")
            };

            List<Person> people = new List<Person>(){
                new Person("Van", 19, new int[] {1,2,3}, 1),
                new Person("Duyen", 19, new int[] {1,3}, 2),

                new Person("Minh", 11, new int[] {2}, 3),
                new Person("Vu", 21, new int[] {2}, 2),


            };

            //Select
            //Where
            //SelectMany

            // var query = from p in people where p.MarkId.Contains(1) select p;

            // foreach (var person in query)
            // {
            //     Console.WriteLine(person);
            // }

            // var kq = people.Select(
            //     (p) =>
            //     {
            //         // return p.Name;
            //         return new
            //         {
            //             Name = p.Name,
            //             Age = p.Age
            //         };
            //     }  
            // );

            // var kq = people.Where(
            //     (p) =>
            //     {
            //         return p.Name.Contains("an");
            //     }
            // );

            //Thanh nhung gia tri thay vi la mang
            // var kq = people.SelectMany(
            //     (p) =>
            //     {
            //         // return new {
            //         //     Name = p.Name,
            //         //     Mark = p.MarkId
            //         // } ;
            //         return p.MarkId;
            //     }
            // );

            //Min, Max, Sum, Avarage

            int[] numbers = {1,2,3,4,5,6};
            // Console.WriteLine(numbers.Where((p) => p % 2 == 0).Max());

            // var kq = people
            //     .SelectMany(p => p.MarkId, (p, markId) => new { p.Name, MarkId = markId })
            //     .Join(subjects,
            //         pm => pm.MarkId,
            //         s => s.Id,
            //         (pm, s) => new { Ten = pm.Name, MonHoc = s.Name });
            // var test = people
            //     .SelectMany(p => p.MarkId, (p, markId) => new { p.Name, MarkId = markId });

            // foreach (var k in test)
            // {
            //     Console.WriteLine(k);
            // }

            // var kq = people.Join(roles, p => p.RoleId, r => r.ID, (person, role) =>
            // {
            //    return new
            //    {
            //         Name = person.Name,
            //         R = role.Name     
            //    } ;
            // });

            // var kq = roles.Join(people, r =>r.ID, p => p.RoleId, (role, person) =>
            // {
            //    return new
            //    {
            //         R = role.Name,
            //         peo = person
            //    } ;
            // });

            // var kq = roles.GroupJoin(people, r => r.ID, p => p.RoleId, (role, props) =>
            // {
            //     return new
            //     {
            //         R = role.Name,
            //         peo = props
            //     };
            // });

            // people.Take(2).ToList().ForEach(p =>
            // {
            //     Console.WriteLine(p);
            // });
            // people.Skip(1).ToList().ForEach(p =>
            // {
            //     Console.WriteLine(p);
            // });

            //OrderByDescending
            //Reverse
            // people.OrderBy(p => p.Age).ToList().ForEach(p =>
            // {
            //     Console.WriteLine(p);
            // });
            // numbers.Reverse().ToList().ForEach(i => Console.WriteLine(i));

            // var kq = people.GroupBy(kq => kq.Age);

            // foreach (var k in kq)
            // {
            //     Console.WriteLine(k.Key);

            //     foreach (var item in k)
            //     {
            //         Console.WriteLine(item);
            //     }
            // }

            // //Distinct;
            // people.SelectMany(p => p.MarkId).Distinct().ToList().ForEach(
            //     id =>
            //     {
            //         Console.WriteLine(id);
            //     }
            // );

            // var kq = people.SingleOrDefault(p => p.Age == 21);
            // if (kq != null)
            //     Console.WriteLine(kq);

            // var check = people.Any(p => p.Age == 21);
            // var check = people.All(p => p.Age >= 10);
            var cnt = people.Count(p => p.Age >= 12);
            Console.WriteLine(cnt);

        }
    }

    class Role
    {
        public int ID {get; private set;}
        public string Name {get;private set;}

        public Role(int id, string name)
        {
            ID = id;
            Name = name;
        }
    }

    public class Subject
    {
        public int Id;
        public string Name {get; private set;}

        public Subject(int id, string name)
        {
            Name = name;
            Id = id;
        }
    }
    class Person {
        public string Name {get;private set;}
        public int Age {get; private set;}
        public int[] MarkId {get; private set;}

        public int RoleId;

        public Person(string name, int age, int[] markId, int roleId) {
            Name = name;
            Age = age;
            MarkId = markId;
            RoleId = roleId;
        }     
        override public string ToString() {
            return $"{Name} {Age, 5} [{string.Join(", ", MarkId)}]";
        }
    }  
}
//Language Integrated Query