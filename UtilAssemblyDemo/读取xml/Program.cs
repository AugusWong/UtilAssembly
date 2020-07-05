using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace 读取xml
{
    class Program
    {
        static void Main(string[] args)
        {
            var path = @"F:\Dev\LinkedX.OrderService\src\LinkedX.OrderService.Web\bin\Debug\netcoreapp2.1\LinkedX.OrderService.Core.xml";
            var x = new XmlDocument();
            x.Load(path);
            //var stream = new MemoryStream();
            //x.Save(stream);

            // 打开文件

            FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);

            // 读取文件的 byte[]

            byte[] bytes = new byte[fileStream.Length];

            fileStream.Read(bytes, 0, bytes.Length);

            fileStream.Close();

            // 把 byte[] 转换成 Stream

            Stream stream = new MemoryStream(bytes);
            //ListToXmlTest();
            //DataTableToXmlTest();
            ClassInfoToXml();

            var test=XmlUtil.Deserialize(typeof(ResQueryXml), stream);

            Console.WriteLine("Hello World!");
            Console.ReadKey();
        }
        

        /// <summary>
        /// 将班级信息转换成XML
        /// </summary>
        public static void ClassInfoToXml()
        {
            //获取班级信息
            ClassInfo classInfo = GetClassInfo();

            //将班级信息转换成XML
            string classXml = XmlSerializeHelper.XmlSerialize(classInfo);

            var ca = XmlSerializeHelper.DESerializer<ClassInfo>(classXml);
        }

        /// <summary>
        /// 获取班级信息
        /// </summary>
        public static ClassInfo GetClassInfo()
        {
            //创建班级信息
            ClassInfo classInfo = new ClassInfo();
            classInfo.ClassID = 1;
            classInfo.ClassName = "高一（5）班";
            classInfo.Teacher = "李老师";

            //创建学生列表
            List<Student> studentList = new List<Student>();
            studentList.Add(new Student() { StuID = 1, StuName = "张三", Sex = "男", Email = "zhangsan@mail.com" });
            studentList.Add(new Student() { StuID = 2, StuName = "李四", Sex = "女", Email = "lisi@mail.com" });
            studentList.Add(new Student() { StuID = 3, StuName = "王五", Sex = "男", Email = "wangwu@mail.com" });
            classInfo.StudentList = studentList;

            return classInfo;
        }



        /// <summary>
        /// 将DataTable与XML相互转换
        /// </summary>
        public static void DataTableToXmlTest()
        {
            //创建DataTable对象
            DataTable dt = CreateDataTable();

            //将DataTable转换成XML
            string xmlResult = XmlSerializeHelper.XmlSerialize(dt);

            //将XML转换成DataTable
            DataTable deResult = XmlSerializeHelper.DESerializer<DataTable>(xmlResult);
        }
        /// <summary>
        /// 创建DataTable对象
        /// </summary>
        public static DataTable CreateDataTable()
        {
            //创建DataTable
            DataTable dt = new DataTable("NewDt");

            //创建自增长的ID列
            DataColumn dc = dt.Columns.Add("ID", Type.GetType("System.Int32"));
            dt.Columns.Add(new DataColumn("Name", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("CreateTime", Type.GetType("System.DateTime")));

            //创建数据
            DataRow dr = dt.NewRow();
            dr["ID"] = 1;
            dr["Name"] = "张三";
            dr["CreateTime"] = DateTime.Now;
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["ID"] = 2;
            dr["Name"] = "李四";
            dr["CreateTime"] = DateTime.Now;
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["ID"] = 3;
            dr["Name"] = "王五";
            dr["CreateTime"] = DateTime.Now;
            dt.Rows.Add(dr);

            return dt;
        }

        /// <summary>
        /// 将List与XML相互转换
        /// </summary>
        public static void ListToXmlTest()
        {
            //获取用户列表
            List<UserInfo> userList = GetUserList();

            //将实体对象转换成XML
            string xmlResult = XmlSerializeHelper.XmlSerialize(userList);

            //将XML转换成实体对象
            List<UserInfo> deResult = XmlSerializeHelper.DESerializer<List<UserInfo>>(xmlResult);
        }

        /// <summary>
        /// 获取用户信息列表
        /// </summary>
        public static List<UserInfo> GetUserList()
        {
            List<UserInfo> userList = new List<UserInfo>();
            userList.Add(new UserInfo() { ID = 1, Name = "张三", CreateTime = DateTime.Now });
            userList.Add(new UserInfo() { ID = 2, Name = "李四", CreateTime = DateTime.Now });
            userList.Add(new UserInfo() { ID = 2, Name = "王五" });
            return userList;
        }

    }

    [Serializable]
    [XmlRoot(elementName: "doc")]
    public class ResQueryXml
    {
        [XmlElement(ElementName = "assembly")]
        public QueryAssembly assembly { get; set; }
        /// <summary>
        /// member 集合
        /// </summary>
        [XmlElement(ElementName = "members")]
        public List<QueryMember> member { get; set; }
    }
    [Serializable]
    public class QueryAssembly
    {
        public string name { get; set; }
    }
    [Serializable]
    public class QueryMember
    {
        //[XmlAttribute(AttributeName = "name")]

        //public string name { get; set; }

        /// <summary>
        /// 注释
        /// </summary>
        public string summary { get; set; }
    }

    /// <summary>
    /// Xml序列化与反序列化
    /// </summary>
    public class XmlUtil
    {
        #region 反序列化
        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="xml">XML字符串</param>
        /// <returns></returns>
        public static object Deserialize(Type type, string xml)
        {
            try
            {
                using (StringReader sr = new StringReader(xml))
                {
                    XmlSerializer xmldes = new XmlSerializer(type);
                    return xmldes.Deserialize(sr);
                }
            }
            catch (Exception e)
            {

                return null;
            }
        }
        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="type"></param>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static object Deserialize(Type type, Stream stream)
        {
            XmlSerializer xmldes = new XmlSerializer(type);
            return xmldes.Deserialize(stream);
        }
        #endregion

        #region 序列化
        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="obj">对象</param>
        /// <returns></returns>
        public static string Serializer(Type type, object obj)
        {
            MemoryStream Stream = new MemoryStream();
            XmlSerializer xml = new XmlSerializer(type);
            try
            {
                //序列化对象
                xml.Serialize(Stream, obj);
            }
            catch (InvalidOperationException)
            {
                throw;
            }
            Stream.Position = 0;
            StreamReader sr = new StreamReader(Stream);
            string str = sr.ReadToEnd();

            sr.Dispose();
            Stream.Dispose();

            return str;
        }

        #endregion
    }
}
