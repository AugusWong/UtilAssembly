using Abp.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace LinkedX.OrderService.Repairs.Dto
{
    class ReparirDBCommentDto
    {
    }

    public class Table_Column
    {
        /// <summary>
        /// 表名
        /// </summary>
        public string Table_Name { get; set; }
        /// <summary>
        /// 列名
        /// </summary>
        public string Column_Name { get; set; }
        /// <summary>
        /// 列注释
        /// </summary>
        public string Column_Comment { get; set; }
    }

    public class Table
    {
        /// <summary>
        /// 表名
        /// </summary>
        public string Table_Name { get; set; }
        /// <summary>
        /// 表注释
        /// </summary>
        public string Table_Comment { get; set; }
    }
    [XmlRoot("doc")]
    public class QueryCoreXml
    {
        [XmlElement("assembly")]
        public QueryAssemblyXml Assembly { get; set; }
        [XmlElement("members")]
        public QueryMembersXml Members { get; set; }
    }
    public class QueryAssemblyXml
    {
        [XmlElement("name")]
        public string Name { get; set; }
    }
    public class QueryMembersXml
    {
        [XmlElement("member")]
        public List<MemberXml> MemberList { get; set; }
    }
    public class MemberXml
    {
        [XmlAttribute("name")]
        public string Name { get; set; }
        [XmlElement("summary")]
        public string Summary { get; set; }
        [XmlElement("param")]
        public List<QueryParaXml> ParamList { get; set; }
        [XmlElement("returns")]
        public String Return { get; set; }
    }

    public class QueryParaXml
    {
        [XmlAttribute("name")]
        public string Name { get; set; }
    }

    public class DbDescription
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<DbDescription> Column { get; set; }
    }

}
