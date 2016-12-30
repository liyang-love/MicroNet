using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace MicroNet.ConfigHelper
{
    /// <summary>
    /// Autho:liyang-live
    /// github:https://github.com/liyang-live/ConfigManage
    /// </summary>
    public class ConfigManageHelper : IDisposable
    {
        /// <summary>
        /// 全局锁对象
        /// </summary>
        private static object obj = new object();

        /// <summary>
        /// 配置字典
        /// </summary>
        private static ConcurrentDictionary<string, string> ConfigDictionary = new ConcurrentDictionary<string, string>();

        private static List<ConfigModel> _ConfigModelList = null;

        private static IServiceProvider _IServiceProvider;

        /// <summary>
        /// 当前实例对象
        /// </summary>
        private static ConfigManageHelper instance;

        /// <summary>
        /// 数据库链接字符串
        /// </summary>
        [Description("数据库链接字符串")]
        public static string DBConnectionStrings { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        private ConfigManageHelper()
        {

            if (_IServiceProvider == null) //注入默认数据库服务提供者
            {
                AddServiceProvider(new SqlServerServiceProvider());
            }

            _ConfigModelList = GetConfigList();
            foreach (var item in _ConfigModelList)
            {
                if (!ConfigDictionary.TryAdd(item.Name, item.Parameter))
                {
                    throw new ConfigException("初始化配置异常,已存在相同的配置名称!");
                }
            }
        }

        /// <summary>
        /// 单例
        /// </summary>
        public static ConfigManageHelper Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (obj)
                    {
                        if (instance == null)
                        {
                            instance = new ConfigManageHelper();
                        }
                    }
                }
                return instance;
            }
        }

        /// <summary>
        /// 获取配置
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        private string GetConfig(ConfigModel config)
        {
            string Value = string.Empty;

            if (ConfigDictionary.TryGetValue(config.Name, out Value))
            {
                return Value;
            }
            else
            {
                //添加至数据库
                if (CreateConfig(config))
                {
                    if (ConfigDictionary.TryAdd(config.Name, config.Parameter))
                    {
                        return config.Parameter;
                    }

                    throw new ConfigException("添加至字典集出错,请检查是否已存在相同名称的配置！");
                }

                throw new ConfigException("添加至数据库出错,请检查是否具有相同配置！");
            }


        }

        /// <summary>
        /// 获取String类型参数   如果为空则返回 string.Empty而不是null
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        [Description("获取String类型参数   如果为空则返回 string.Empty的值而不是null(空引用)")]
        public string ConfigString(ConfigString config)
        {
            if (config == null || string.IsNullOrWhiteSpace(config.Name))
            {
                throw new ConfigException("配置信息不能未null,请检查后重新传入!");
            }

            if (config.Name.Trim().Length > 50)
            {
                throw new ConfigException("配置名称的长度应小于50个字符!");
            }


            var result = GetConfig(new ConfigModel
            {
                Id = 0,
                Name = config.Name,
                Classify = config.Classify,
                Parameter = config.Parameter,
                ButtonType = config.ButtonType.GetHashCode(),
                Remark = config.Remark
            });

            if (result == null)
            {
                return string.Empty;
            }

            return result;
        }

        /// <summary>
        /// 获取Int32类型参数 默认值为0
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        [Description("获取Int32类型参数  默认值为-1")]
        public int ConfigInteger(ConfigInteger config)
        {
            if (config == null || string.IsNullOrWhiteSpace(config.Name))
            {
                throw new ConfigException("配置信息不能未null,请检查后重新传入!");
            }

            if (config.Name.Trim().Length > 50)
            {
                throw new ConfigException("配置名称的长度应小于50个字符!");
            }


            var result = GetConfig(new ConfigModel
            {
                Id = -1,
                Name = config.Name,
                Classify = config.Classify,
                Parameter = config.Parameter.ToString(),
                ButtonType = config.ButtonType.GetHashCode(),
                Remark = config.Remark
            });

            return Convert.ToInt32(result);
        }


        /// <summary>
        /// 获取List<string>类型参数 如果为空则返回空对象而不是null
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        [Description("获取List<string>类型参数  如果为空则返回空对象而不是null")]
        public List<string> ConfigList(ConfigList config)
        {
            if (config == null || string.IsNullOrWhiteSpace(config.Name))
            {
                throw new ConfigException("配置信息不能未null,请检查后重新传入!");
            }

            if (config.Name.Trim().Length > 50)
            {
                throw new ConfigException("配置名称的长度应小于50个字符!");
            }


            var result = GetConfig(new ConfigModel
            {
                Id = 0,
                Name = config.Name,
                Classify = config.Classify,
                Parameter = (config.Parameter == null ? null : string.Join(";", config.Parameter)),
                ButtonType = config.ButtonType.GetHashCode(),
                Remark = config.Remark
            });

            if (result == null)
            {
                return new List<string>();
            }

            return result.Split(';').ToList();
        }

        /// <summary>
        /// 获取List<int>类型参数 如果为空则返回空对象而不是null
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        [Description("获取List<string>类型参数  如果为空则返回空对象而不是null")]
        public List<int> ConfigListInt(ConfigListInt config)
        {
            if (config == null || string.IsNullOrWhiteSpace(config.Name))
            {
                throw new ConfigException("配置信息不能未null,请检查后重新传入!");
            }

            if (config.Name.Trim().Length > 50)
            {
                throw new ConfigException("配置名称的长度应小于50个字符!");
            }


            var result = GetConfig(new ConfigModel
            {
                Id = 0,
                Name = config.Name,
                Classify = config.Classify,
                Parameter = (config.Parameter == null ? null : string.Join(";", config.Parameter)),
                ButtonType = config.ButtonType.GetHashCode(),
                Remark = config.Remark
            });

            if (result == null)
            {
                return new List<int>();
            }

            return result.Split(';').Select(s => int.Parse(s)).ToList();
        }

        /// <summary>
        /// 获取bool类型参数
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        [Description("获取bool类型参数")]
        public bool ConfigBoolean(ConfigBoolean config)
        {
            if (config == null || string.IsNullOrWhiteSpace(config.Name))
            {
                throw new ConfigException("配置信息不能未null,请检查后重新传入!");
            }

            if (config.Name.Trim().Length > 50)
            {
                throw new ConfigException("配置名称的长度应小于50个字符!");
            }


            var result = GetConfig(new ConfigModel
            {
                Id = 0,
                Name = config.Name,
                Classify = config.Classify,
                Parameter = config.Parameter.ToString(),
                ButtonType = config.ButtonType.GetHashCode(),
                Remark = config.Remark
            });

            return Convert.ToBoolean(result);
        }

        [Description("获取全部配置")]
        public List<ConfigModel> GetConfigsList()
        {
            return _ConfigModelList;
        }

        [Description("更新单个配置")]
        public bool UpdateConfigrue(ConfigModel model)
        {
            return UpdateConfig(model);
        }

        #region 操作数据库

        [Description("注入数据库处理实例")]
        public static void AddServiceProvider(IServiceProvider serviceProvider)
        {
            _IServiceProvider = serviceProvider;
        }

        /// <summary>
        /// 读取数据库全部配置
        /// </summary>
        /// <returns></returns>
        private List<ConfigModel> GetConfigList()
        {
            return _IServiceProvider.GetConfigList();
        }

        /// <summary>
        /// 将配置写入数据库
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        private bool CreateConfig(ConfigModel config)
        {
            return _IServiceProvider.CreateConfig(config);
        }

        /// <summary>
        /// 更新单条配置信息
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        private bool UpdateConfig(ConfigModel config)
        {
            return _IServiceProvider.UpdateConfig(config);
        }
        #endregion

        /// <summary>
        /// 必须手动释放资源
        /// </summary>
        public void Dispose()
        {
            lock (obj)
            {
                ConfigDictionary.Clear();
                instance = null;
            }
        }
    }

    /// <summary>
    /// 与数据库交互接口
    /// </summary>
    [Description("与数据库交互接口")]
    public interface IServiceProvider
    {
        /// <summary>
        /// 获取全部配置
        /// </summary>
        /// <returns></returns>
        [Description("获取全部配置")]
        List<ConfigModel> GetConfigList();

        /// <summary>
        /// 新增配置
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        [Description("新增配置")]
        bool CreateConfig(ConfigModel config);

        bool UpdateConfig(ConfigModel config);
    }

    /// <summary>
    /// 自定义异常
    /// </summary>
    public class ConfigException : Exception
    {
        public ConfigException()
        {
        }

        public ConfigException(string message) : base(message)
        {
        }

        public ConfigException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ConfigException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }


    /// <summary>
    /// 配置实体
    /// </summary>
    [Description("配置实体")]
    [DataContract]
    public class ConfigModel
    {
        /// <summary>
        /// 主键编号
        /// </summary>
        [Description("主键编号")]
        [DataMember]
        public int Id { get; set; }

        /// <summary>
        /// 配置名称
        /// </summary>
        [Description("配置名称")]
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        /// 参数
        /// </summary>
        [Description("参数")]
        [DataMember]
        public string Parameter { get; set; }

        /// <summary>
        /// 配置分类
        /// </summary>
        [Description("配置分类")]
        [DataMember]
        public string Classify { get; set; }

        /// <summary>
        /// 按钮类型   注：该配置用以指定在可视化时显示的按钮类型 如：文本框  单选  下拉等
        /// 1、文本框  2、单选按钮  3、多选框  4、下拉框  5、多行文本框
        /// </summary>
        [Description("按钮类型（1、文本框  2、单选按钮  3、多选框  4、下拉框  5、多行文本框）  注：该配置用以指定在可视化时显示的按钮类型 如：文本框  单选  下拉等")]
        [DataMember]
        public int ButtonType { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [Description("备注")]
        [DataMember]
        public string Remark { get; set; }
    }

    /// <summary>
    /// 整形配置实体
    /// </summary>
    [Description("配置实体")]
    public class ConfigInteger
    {
        /// <summary>
        /// 配置名称
        /// </summary>
        [Description("配置名称")]
        public string Name { get; set; }

        /// <summary>
        /// 参数  默认返回0
        /// </summary>
        [Description("参数  默认返回0")]
        public int Parameter { get; set; }

        /// <summary>
        /// 配置分类
        /// </summary>
        [Description("配置分类")]
        public string Classify { get; set; }

        /// <summary>
        /// 按钮类型   注：该配置用以指定在可视化时显示的按钮类型 如：文本框  单选  下拉等
        /// 1、文本框  2、单选按钮  3、多选框  4、下拉框  5、多行文本框
        /// </summary>
        [Description("按钮类型（1、文本框  2、单选按钮  3、多选框  4、下拉框  5、多行文本框）  注：该配置用以指定在可视化时显示的按钮类型 如：文本框  单选  下拉等")]
        public EnumButtonType ButtonType { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [Description("备注")]
        public string Remark { get; set; }
    }

    /// <summary>
    /// 字符串类型配置实体
    /// </summary>
    [Description("配置实体")]
    public class ConfigString
    {
        /// <summary>
        /// 配置名称
        /// </summary>
        [Description("配置名称")]
        public string Name { get; set; }

        /// <summary>
        /// 参数  默认返回空
        /// </summary>
        [Description("参数  默认返回空")]
        public string Parameter { get; set; }

        /// <summary>
        /// 配置分类
        /// </summary>
        [Description("配置分类")]
        public string Classify { get; set; }

        /// <summary>
        /// 按钮类型   注：该配置用以指定在可视化时显示的按钮类型 如：文本框  单选  下拉等
        /// 1、文本框  2、单选按钮  3、多选框  4、下拉框  5、多行文本框
        /// </summary>
        [Description("按钮类型（1、文本框  2、单选按钮  3、多选框  4、下拉框  5、多行文本框）  注：该配置用以指定在可视化时显示的按钮类型 如：文本框  单选  下拉等")]
        public EnumButtonType ButtonType { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [Description("备注")]
        public string Remark { get; set; }
    }


    /// <summary>
    /// List类型配置实体
    /// </summary>
    [Description("配置实体")]
    public class ConfigList
    {
        /// <summary>
        /// 配置名称
        /// </summary>
        [Description("配置名称")]
        public string Name { get; set; }

        /// <summary>
        /// 参数   默认返回null 多个之间使用;隔开
        /// </summary>
        [Description("参数  默认返回null 多个之间使用;隔开")]
        public List<string> Parameter { get; set; }

        /// <summary>
        /// 配置分类
        /// </summary>
        [Description("配置分类")]
        public string Classify { get; set; }

        /// <summary>
        /// 按钮类型   注：该配置用以指定在可视化时显示的按钮类型 如：文本框  单选  下拉等
        /// 1、文本框  2、单选按钮  3、多选框  4、下拉框  5、多行文本框
        /// </summary>
        [Description("按钮类型（1、文本框  2、单选按钮  3、多选框  4、下拉框  5、多行文本框）  注：该配置用以指定在可视化时显示的按钮类型 如：文本框  单选  下拉等")]
        public EnumButtonType ButtonType { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [Description("备注")]
        public string Remark { get; set; }
    }

    /// <summary>
    /// List<Int>类型配置实体
    /// </summary>
    [Description("配置实体")]
    public class ConfigListInt
    {
        /// <summary>
        /// 配置名称
        /// </summary>
        [Description("配置名称")]
        public string Name { get; set; }

        /// <summary>
        /// 参数   默认返回null 多个之间使用;隔开
        /// </summary>
        [Description("参数  默认返回null 多个之间使用;隔开")]
        public List<int> Parameter { get; set; }

        /// <summary>
        /// 配置分类
        /// </summary>
        [Description("配置分类")]
        public string Classify { get; set; }

        /// <summary>
        /// 按钮类型   注：该配置用以指定在可视化时显示的按钮类型 如：文本框  单选  下拉等
        /// 1、文本框  2、单选按钮  3、多选框  4、下拉框  5、多行文本框
        /// </summary>
        [Description("按钮类型（1、文本框  2、单选按钮  3、多选框  4、下拉框  5、多行文本框）  注：该配置用以指定在可视化时显示的按钮类型 如：文本框  单选  下拉等")]
        public EnumButtonType ButtonType { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [Description("备注")]
        public string Remark { get; set; }
    }

    /// <summary>
    /// Boolean类型配置实体
    /// </summary>
    [Description("配置实体")]
    public class ConfigBoolean
    {

        /// <summary>
        /// 配置名称
        /// </summary>
        [Description("配置名称")]
        public string Name { get; set; }

        /// <summary>
        /// 参数   默认返回null 多个之间使用;隔开
        /// </summary>
        [Description("参数  默认返回null 多个之间使用;隔开")]
        public bool Parameter { get; set; }

        /// <summary>
        /// 配置分类
        /// </summary>
        [Description("配置分类")]
        public string Classify { get; set; }

        /// <summary>
        /// 按钮类型   注：该配置用以指定在可视化时显示的按钮类型 如：文本框  单选  下拉等
        /// 1、文本框  2、单选按钮  3、多选框  4、下拉框  5、多行文本框
        /// </summary>
        [Description("按钮类型（1、文本框  2、单选按钮  3、多选框  4、下拉框  5、多行文本框）  注：该配置用以指定在可视化时显示的按钮类型 如：文本框  单选  下拉等")]
        public EnumButtonType ButtonType { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [Description("备注")]
        public string Remark { get; set; }
    }

    /// <summary>
    /// 按钮类型 1、文本框  2、单选按钮  3、多选框  4、下拉框  5、多行文本框
    /// </summary>
    [Description("按钮类型（1、文本框  2、单选按钮  3、多选框  4、下拉框  5、多行文本框）")]
    public enum EnumButtonType
    {
        /// <summary>
        /// 文本框
        /// </summary>
        [Description("文本框")]
        TextBox = 1,

        /// <summary>
        /// 单选按钮
        /// </summary>
        [Description("单选按钮")]
        RadioButton = 2,

        /// <summary>
        /// 多选框
        /// </summary>
        [Description("多选框")]
        CheckBox = 3,

        /// <summary>
        /// 下拉框
        /// </summary>
        [Description("下拉框")]
        DropDownList = 4,

        /// <summary>
        /// 多行文本框
        /// </summary>
        [Description("多行文本框")]
        TextAre = 5
    }


    /// <summary>
    /// 默认数据库服务
    /// </summary>
    [Description("默认数据库服务")]
    public class SqlServerServiceProvider : IServiceProvider
    {

        //public string ConnectStr = System.Configuration.ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;

        private string CreateTable = @"Create Table 配置信息表(
                                      Id int identity(1,1) primary key not null,
                                      Name nvarchar(50) not null UNIQUE,
                                      Parameter   nvarchar(200),
                                      Classify     nvarchar(50) not null, 
                                      ButtonType    int,
                                      Remark        nvarchar(500)     
                                                              )";

        private string InsertData = @"Insert into 配置信息表(Name,Parameter,Classify,ButtonType,Remark) 
                                                     Values(@Name,@Parameter,@Classify,@ButtonType,@Remark)";

        private string UpdateData = @"UPDATE 配置信息表 SET Parameter=@Parameter where Id=@Id";

        private string ReadData = @"select Id,Name,Parameter,Classify,ButtonType,Remark from 配置信息表";

        private string IsExits = @"select * from sys.sysobjects where name='配置信息表'";

        private static bool Exist = false;

        public bool CreateConfig(ConfigModel config)
        {
            if (Exists())
            {
                //string sql = string.Format(InsertData, config.Name, config.Parameter, config.Classify, config.ButtonType, config.Remark);
                System.Data.SqlClient.SqlParameter[] para ={
                    new System.Data.SqlClient.SqlParameter ("@Name", config.Name),
                    new System.Data.SqlClient.SqlParameter ("@Parameter", config.Parameter),
                    new System.Data.SqlClient.SqlParameter ("@Classify", config.Classify),
                    new System.Data.SqlClient.SqlParameter ("@ButtonType", config.ButtonType),
                    new System.Data.SqlClient.SqlParameter ("@Remark", config.Remark),
                };

                var result = Create(InsertData, para);
                if (result > 0)
                    return true;
                return false;
            }

            throw new Exception("找不到数据信息！");

        }

        public bool UpdateConfig(ConfigModel config)
        {
            if (Exists())
            {
                //string sql = string.Format(InsertData, config.Name, config.Parameter, config.Classify, config.ButtonType, config.Remark);
                System.Data.SqlClient.SqlParameter[] para ={
                    new System.Data.SqlClient.SqlParameter ("@Id", config.Id),
                    new System.Data.SqlClient.SqlParameter ("@Parameter", config.Parameter)
                };

                var result = Create(UpdateData, para);
                if (result > 0)
                    return true;
                return false;
            }

            throw new Exception("找不到数据信息！");
        }

        public List<ConfigModel> GetConfigList()
        {
            if (Exists())
            {

                var dt = GetData(ReadData);
                var list = TableToEntity<ConfigModel>(dt);
                return list;
            }

            throw new Exception("找不到数据信息！");
        }

        /// <summary>
        /// 检测数据库表是否存在
        /// </summary>
        /// <returns></returns>
        public bool Exists()
        {
            if (Exist) return Exist;

            var dt = GetData(IsExits);
            if (dt.Rows.Count > 0)
            {
                Exist = true;
                return true;
            }
            else
            {
                if (Create(CreateTable) > 0)
                {
                    Exist = true;
                    return true;
                }
            }
            return false;
        }

        private System.Data.DataTable GetData(string sql)
        {
            using (System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(ConfigManageHelper.DBConnectionStrings))
            {
                System.Data.SqlClient.SqlDataAdapter sda = new System.Data.SqlClient.SqlDataAdapter(sql, conn);
                conn.Open();
                System.Data.DataSet ds = new System.Data.DataSet();
                sda.Fill(ds);
                conn.Close();
                return ds.Tables[0];
            }
        }

        private int Create(string sql)
        {
            using (System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(ConfigManageHelper.DBConnectionStrings))
            {
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(sql, conn);
                conn.Open();
                int result = cmd.ExecuteNonQuery();
                conn.Close();
                return result;
            }
        }

        private int Create(string sql, System.Data.SqlClient.SqlParameter[] para)
        {
            using (System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(ConfigManageHelper.DBConnectionStrings))
            {
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(sql, conn);
                cmd.Parameters.AddRange(para);
                conn.Open();
                int result = cmd.ExecuteNonQuery();
                conn.Close();
                return result;
            }
        }


        private static List<T> TableToEntity<T>(DataTable dt) where T : class, new()
        {
            Type type = typeof(T);
            List<T> list = new List<T>();

            foreach (DataRow row in dt.Rows)
            {
                PropertyInfo[] pArray = type.GetProperties();
                T entity = new T();
                foreach (PropertyInfo p in pArray)
                {
                    if (row[p.Name] is Int64)
                    {
                        p.SetValue(entity, Convert.ToInt32(row[p.Name]), null);
                        continue;
                    }
                    p.SetValue(entity, row[p.Name], null);
                }
                list.Add(entity);
            }
            return list;
        }
    }
}
