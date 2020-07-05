using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace 读取xml
{
    /// <summary>
    /// 用户信息类
    /// </summary>
    public class UserInfo
    {
        /// <summary>
        /// 编号
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateTime { get; set; }
    }


    /// <summary>
    /// 班级信息类
    /// </summary>
    [XmlRootAttribute("MyClassInfo", Namespace = "ABC_123", IsNullable = false)]
    public class ClassInfo
    {
        /// <summary>
        /// 班级ID
        /// </summary>
        [XmlAttribute("MyClassID")]
        public int ClassID { get; set; }

        /// <summary>
        /// 班级名称
        /// </summary>
        [XmlElementAttribute("MyClassName", IsNullable = false)]
        public string ClassName { get; set; }

        /// <summary>
        /// 班长人
        /// </summary>
        [XmlElementAttribute("MyTeacher", IsNullable = false)]
        public string Teacher { get; set; }

        /// <summary>
        /// 学生列表
        /// </summary>
        [XmlArrayAttribute("MyStudents")]
        public List<Student> StudentList { get; set; }
    }

    /// <summary>
    /// 学生信息类
    /// </summary>
    [XmlRootAttribute("MyStudent", IsNullable = false)]
    public class Student
    {
        /// <summary>
        /// 学生ID
        /// </summary>
        [XmlAttribute("MyStuID")]
        public int StuID { get; set; }

        /// <summary>
        /// 学生名称
        /// </summary>
        [XmlElementAttribute("MyStuName", IsNullable = false)]
        public string StuName { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        [XmlElementAttribute("MySex", IsNullable = false)]
        public string Sex { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        [XmlIgnoreAttribute]
        public string Email { get; set; }
    }

}
