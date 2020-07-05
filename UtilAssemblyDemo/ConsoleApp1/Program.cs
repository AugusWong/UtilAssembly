using System;

namespace ConsoleApp1
{
    public class Ddog : Dog
    {
        public int Age;
    }
    public class Dog : Animal
    {
        public string name { get; set; }
    }

    public class Animal
    {
        public string id { get; set; }
    }

    public class Program
    {
        public static void Main()
        {
            Dog aa = new Dog { name = "狗", id = "动物" };
            CheckClass(aa);
        }
        public static void CheckClass<T>(T entity)
        {
            bool re1 = typeof(Animal).IsAssignableFrom(typeof(T));
            //返回true的条件是Dog类直接或间接的实现了Animal类;
            bool re2 = typeof(T).IsSubclassOf(typeof(Animal));
            //返回true的条件是Dog类是Animal的子类

            bool re3 = typeof(Ddog).IsSubclassOf(typeof(Animal));


            //var re4 = typeof(Ddog).IsDefined(typeof(Dog), false);
            // typeof(Ddog).is
            var re5 = typeof(Ddog).IsDefined(typeof(Animal), false);







            var id = (entity as Animal).id;
        }
    }
}
