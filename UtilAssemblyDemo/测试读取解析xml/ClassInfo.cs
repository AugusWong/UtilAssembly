using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace 测试
{
    /// <summary>
    /// 班级信息类
    /// </summary>
    [XmlRootAttribute("MyClassInfo")]
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
        [XmlArrayAttribute("members")]
        public List<Student> MemberList { get; set; }
    }

    /// <summary>
    /// 学生信息类
    /// </summary>
    [XmlRootAttribute("ttt", IsNullable = false)]
    public class Student
    {
        /// <summary>
        /// 学生ID
        /// </summary>
        [XmlAttribute("name")]
        public string PropertyName { get; set; }

        //[XmlElement("summary")]
        //public string Summary { get; set; }

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
