using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace 测试
{
    [XmlRoot("doc")]
    public class QueryCoreXml
    {
        [XmlElement("assembly")]
        public QueryAssembly Assembly { get; set; }

        [XmlElement("members")]
        public Members Members { get; set; }

    }

    public class Members
    {
        [XmlElement("member")]
        public List<Member> MemberList { get; set; }
    }

    public class Member
    {
        [XmlElement("summary")]
        public string summary { get; set; }
    }


    //[XmlRoot("member", IsNullable = true)]
    //public class Member
    //{
    //    [XmlAttribute("nameid")]
    //    public string PropertyName { get; set; }
    //    [XmlElement("summary")]
    //    public string Summary { get; set; }
    //    //[XmlElement("returns")]
    //    //public string Returns { get; set; }

    //    //[XmlArray("param")]
    //    //public List<QueryParmasXml> Params { get; set; }
    //}


    //public class QueryParmasXml
    //{
    //    [XmlAttribute("name")]
    //    public string Name { get; set; }
    //}

   // [XmlRoot("assembly")]
    public class QueryAssembly
    {
        [XmlElement("name")]
        public string Name { get; set; }
    }
}
