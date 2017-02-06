## MicroNet（常用方法集合）
##### 一、常用公共方法
##### 二、项目目录介绍
	1、 MicroNet：适用于.Net Framework 4.0+的版本
	2、 MicroNetCore:适用.Net netstandard1.6版本
	3、 Test:单元测试项目
##### 三、项目命名空间介绍:
	1、 MicroNet.Logging
			该名称空间下为日志帮助类,使用时以Logger点扩展方法使用,当Net Framework版本为4.5及以上时将会自动记录代码行数及源代码文件
	2、 MicroNet.SQLHelpers
			该名称下为常用的数据库执行CRUD帮助类	

使用方法：
 连接字符串： 
 ```
 <add name="Constr" connectionString="Data Source=*/orcl;Persist Security Info=True;User ID=*;Password=*;"
      providerName="Oracle.ManagedDataAccess.Client"/>
 ```   
 > 该方法会自动根据转入的连接字符串的名称获取providerName 进行判断数据库类型，所有执行方法均使用ADO.Net标准方法
 
 ``` 
 SQLHelper db = new SQLHelper("Constr");
/*一般语句执行*/
var data = db.Instance.GetData("select * from tablename");
/*存储过程执行*/
OracleParameter[] OracleParameters = {
            new OracleParameter("p_BQID", OracleDbType.Varchar2,200,"8001",ParameterDirection.Input),
            new OracleParameter("P_UID", OracleDbType.Varchar2,200,"6666",ParameterDirection.Input),
            new OracleParameter("P_KIND", OracleDbType.Char,200,"0",ParameterDirection.Input),
            new OracleParameter("P_CUR", OracleDbType.RefCursor, ParameterDirection.Output)
        };

var drug = db.Instance.ExecuteStor("proc_test", OracleParameters);

/*返回实体*/
var model = db.Instance.GetData<Advice>("select * from tablename");
            
```

3、MicroNet.Common   
	&nbsp; &nbsp;&nbsp;&nbsp;该名称空间下为常用的基本函数类库   
	&nbsp; &nbsp;&nbsp;&nbsp;3.1、 DataTableToModelHelper:DataTable转成List,使用更加高效的EMIT方法生成.数据库字段的名称必须和实体的名称对应,并且区分英文大小写   
	&nbsp; &nbsp;&nbsp;&nbsp;3.2、 GuidHelper：GUID常用的帮助类   
	&nbsp; &nbsp;&nbsp;&nbsp;3.3、 StringHelper：字符串或对象帮助类   
