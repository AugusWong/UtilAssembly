using System;

namespace 想要解析与更新mysql迁移时的数据库备注
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }

        /// <summary>
        /// 更新表相关注释
        /// </summary>
        /// <param name="tableName"></param>
        public void ReparirDBComment(string tableName)
        {
            var sql_column = $@"SELECT
                            table_name,
	                        COLUMN_Name,
	                        COLUMN_COMMENT
                        FROM
                            information_schema.COLUMNS
                        WHERE
                            TABLE_SCHEMA = 'orderservicedb' ";

            var sql_table = $@"SELECT TABLE_NAME,
                                TABLE_COMMENT
                                FROM information_schema.TABLES
                                WHERE TABLE_SCHEMA='orderservicedb' AND TABLE_TYPE='base table' ";
            if (!tableName.IsNullOrWhiteSpace())
            {
                sql_column += $" and table_name='{tableName}' ";
                sql_table += $" and table_name='{tableName}' ";
            }
            var columnList = _dapperRepository.Query<Table_Column>(sql_column).ToList();
            var tableList = _dapperRepository.Query<Table>(sql_table).ToList();
            if (tableList.Any() && columnList.Any())
            {
                var dllName = "LinkedX.OrderService.Core";
                var path = new FileInfo(Assembly.GetEntryAssembly().Location).DirectoryName;

                var assembly = Assembly.Load(dllName);
                var types = assembly?.GetTypes();
                var entityBaseList = types?
                    .Where(t => t.IsClass
                    && !t.IsGenericType
                    && !t.IsAbstract
                    && (typeof(EntityBase<Guid>).IsAssignableFrom(t) || typeof(EntityBase<int>).IsAssignableFrom(t))
                    ).ToList();

                var entityList = types?
                    .Where(t => t.IsClass
                    && !t.IsGenericType
                    && !t.IsAbstract
                    && (typeof(Entity).IsAssignableFrom(t) || typeof(Entity<Guid>).IsAssignableFrom(t) || typeof(Entity<int>).IsAssignableFrom(t))
                    ).ToList();

                var xmlPath = Path.Combine(path, dllName + ".xml");
                FileStream fs = new FileStream(xmlPath, FileMode.Open, FileAccess.ReadWrite);
                var xmlObj = new QueryCoreXml().Deserialize(fs);

                // var member = xmlObj.Members.MemberList.Where(w => w.Name.ToLower().Contains("transactionbill"));

                var group = columnList.GroupBy(g => g.Table_Name);
                var descriptionList = new List<DbDescription>();
                foreach (var item in group)
                {
                    var add = new DbDescription
                    {
                        Name = item.Key,
                        Description = tableList.Where(w => w.Table_Name == item.Key).Select(s => s.Table_Comment).FirstOrDefault(),
                        Column = new List<DbDescription>()
                    };
                    foreach (var column in item)
                    {
                        add.Column.Add(new DbDescription
                        {
                            Name = column.Column_Name,
                            Description = column.Column_Comment
                        });
                    }
                    descriptionList.Add(add);
                }

                var xmlDescriptionList = new List<DbDescription>();
                var wantedClassList = new List<string>();
                foreach (var member in xmlObj.Members.MemberList)
                {
                    var className = "";
                    if (member.Name.StartsWith("T"))
                    {
                        var index = member.Name.LastIndexOf('.');
                        className = member.Name.Substring(index + 1).ToLower();
                        var dbtable = descriptionList.FirstOrDefault(f => f.Name.ToLower().Contains(className));
                        if (dbtable != null)
                        {
                            dbtable.Description = member.Summary;
                            var colonIndex = member.Name.IndexOf(":");
                        }

                    }
                }

                //entityList.ForEach
            }
        }
    }
}
