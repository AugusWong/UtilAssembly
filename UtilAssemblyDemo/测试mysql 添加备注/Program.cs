using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace 测试mysql_添加备注
{
    class Program
    {
        List<string> list;
        static List<FileInfo> fileInfos=new List<FileInfo>();
        static void Main(string[] args)
        {
            DbDescriptionHelper.GetDescription();
            GetFileNames(@"F:\DevSpace\LinkedX.OrderService\src\LinkedX.OrderService.Core");
            Console.WriteLine("Hello World!");
            Console.ReadKey();
        }

        public static  void GetFileNames(string path)
        {
            DirectoryInfo di = new DirectoryInfo(path);
            //遍历文件 当前path下的文件
            //foreach (FileInfo NextFile in di.GetFiles())
            //{
            //    fileInfos.Add(NextFile);
            //}

            //遍历文件夹
            foreach (DirectoryInfo NextFolder in di.GetDirectories())
            {
                foreach (FileInfo NextFile in NextFolder.GetFiles())
                {
                    fileInfos.Add(NextFile);
                }
                GetFileNames(NextFolder.FullName);


            }
        }
    }


    public class DbDescriptionHelper
    {
        public static string b { get; set; } = "LinkedX.OrderService.Core";
        public static string c { get; set; } = System.AppDomain.CurrentDomain.BaseDirectory;

        public static List<DbDescription> list { get; set; }


        public static string GetDescription(string table, string column = "")
        {
            if (list == null || list.Count() == 0)
            {
                list = GetDescription();
            }

            if (!string.IsNullOrWhiteSpace(table))
            {
                if (string.IsNullOrWhiteSpace(column))
                {
                    var x = list.FirstOrDefault(p => p.Name == table);
                    if (x != null)
                        return x.Description;
                    return string.Empty;
                }
                else
                {
                    var x = list.FirstOrDefault(p => p.Name == table);
                    if (x != null)
                    {
                        var y = x.Column;
                        if (y.IsNotNull())
                        {
                            var z = y.FirstOrDefault(p => p.Name == column);
                            if (z != null)
                                return z.Description;
                        }
                    }
                    return string.Empty;
                }
            }
            else
                return string.Empty;
        }

        public static List<DbDescription> GetDescription()
        {
            var d = new List<DbDescription>();

            var e = Assembly.Load(b);
            var f = e?.GetTypes();
            var g = f?
                .Where(t => t.IsClass
                && !t.IsGenericType
                && !t.IsAbstract
                && typeof(Entity).IsAssignableFrom(t)
                ).ToList();

            foreach (var h in g)
            {
                var i = new DbDescription();

                var j = c + "\\" + h.Name + ".cs";
                var k = File.ReadAllText(j);
                k = k.Substring(k.IndexOf("{") + 1, k.LastIndexOf("}") - k.IndexOf("{") - 1).Replace("\n", "");

                var l = k.Substring(k.IndexOf(" {") + 2, k.LastIndexOf(" }") - k.IndexOf(" {") - 1).Replace("\n", "");
                string[] slipt = { "}\r" };
                var m = l.Split(slipt, StringSplitOptions.None).ToList();

                var n = new List<DbDescription>();
                foreach (var o in m)
                {
                    var p = o.Replace("///", "");

                    var q = p.IndexOf("<summary>");
                    var r = p.LastIndexOf("</summary>");

                    var s = p.IndexOf("public");
                    var t = p.IndexOf("{");

                    var u = (q > 0 && r > 0) ? p.Substring(q + 9, r - q - 10).Replace("\r", "").Replace(" ", "") : "";
                    var v = (s > 0 && t > 0) ? p.Substring(s, t - s).Split(' ')[2] : "";

                    n.Add(new DbDescription()
                    {
                        Description = u,
                        Name = v
                    });
                }

                var w = k.Substring(0, k.IndexOf("{\r") - 1);
                w = w.Replace("///", "");

                var x = w.IndexOf("<summary>");
                var y = w.LastIndexOf("</summary>");
                var z = (x > 0 && y > 0) ? w.Substring(x + 9, y - x - 10).Replace("\r", "").Replace(" ", "") : "";

                d.Add(new DbDescription()
                {
                    Name = h.Name,
                    Description = z,
                    Column = n
                });
            }
            return d;
        }
    }


    public class DbDescription
    {
        /// <summary>
        /// 表名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 描述 （注释）
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        ///列
        /// </summary>
        public List<DbDescription> Column { get; set; }
    }


    public static class Extensions
    {

        public static bool IsNullOrWhiteSpace(this string str)
        {
            return string.IsNullOrWhiteSpace(str);

        }

        public static bool IsNotNull<T>(this T t)
        {
            return t != null;
        }
    }
}
