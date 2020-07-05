using System;
using Utils;

namespace 测试
{
    class Program
    {
        static void Main(string[] args)
        {
            // MyClassID=""1"" xmlns=""ABC_123"">
            var xml2 = @"<MyClassInfo>
            
  <MyClassName>高一（5）班</MyClassName>
  <MyTeacher>李老师</MyTeacher>
  <members>
    <Student name=""T:LinkedX.OrderService.AliRobot"">
      <MyStuName>张三</MyStuName>
      <MySex>男</MySex>
    </Student>
    <Student name=""2"">
      <MyStuName>李四</MyStuName>
      <MySex>女</MySex>
    </Student>   
  </members>
</MyClassInfo>";


            var test10 = XmlSerializeHelper.DESerializer<ClassInfo>(xml2);

            var xml = @"<doc>
                        <assembly>
                            <name>LinkedX.OrderService.Core</name>
                        </assembly>
                        <members>
                            <member name=""T:LinkedX.OrderService.AliRobot"">
                                <summary></summary>
                            </member>
                            <member name=""T:LinkedX.OrderService.AliRobot"">
                                <summary></summary>
                            </member>
                        </members>
                    </doc>";
            var test22 = XmlSerializeHelper.DESerializer<QueryCoreXml>(xml);
            Console.WriteLine("Hello World!");
        }
    }
}
