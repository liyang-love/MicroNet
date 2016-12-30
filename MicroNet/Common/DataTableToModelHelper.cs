using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;

namespace MicroNet.Common
{
    /// <summary>
    /// 数据实体转换
    /// </summary>
    public sealed class DataTableToModelHelper
    {
        //把DataRow转换为对象的委托声明
        private delegate T Load<T>(DataRow dataRecord);

        //用于构造Emit的DataRow中获取字段的方法信息
        private static readonly MethodInfo getValueMethod = typeof(DataRow).GetMethod("get_Item", new Type[] { typeof(int) });

        //用于构造Emit的DataRow中判断是否为空行的方法信息
        private static readonly MethodInfo isDBNullMethod = typeof(DataRow).GetMethod("IsNull", new Type[] { typeof(int) });

        //使用字典存储实体的类型以及与之对应的Emit生成的转换方法
        private static Dictionary<Type, Delegate> rowMapMethods = new Dictionary<Type, Delegate>();

        /// <summary>
        /// 数据实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static List<T> DataTableToList<T>(DataTable dt)
        {

            List<T> list = new List<T>();
            if (dt == null)
                return list;

            //声明 委托Load<T>的一个实例rowMap
            Load<T> rowMap = null;

            Delegate key = null;
            //从rowMapMethods查找当前T类对应的转换方法，没有则使用Emit构造一个。
            //if (!rowMapMethods.ContainsKey(typeof(T)))
            if (!rowMapMethods.TryGetValue(typeof(T), out key))
            {
                DynamicMethod method = new DynamicMethod("DynamicCreateEntity_" + typeof(T).Name, typeof(T), new Type[] { typeof(DataRow) }, typeof(T), true);
                ILGenerator generator = method.GetILGenerator();
                LocalBuilder result = generator.DeclareLocal(typeof(T));
                generator.Emit(OpCodes.Newobj, typeof(T).GetConstructor(Type.EmptyTypes));
                generator.Emit(OpCodes.Stloc, result);

                for (int index = 0; index < dt.Columns.Count; index++)
                {
                    //StringComparison.CurrentCultureIgnoreCase
                    PropertyInfo propertyInfo = typeof(T).GetProperty(dt.Columns[index].ColumnName);
                    Label endIfLabel = generator.DefineLabel();
                    if (propertyInfo != null && propertyInfo.GetSetMethod() != null)
                    {
                        generator.Emit(OpCodes.Ldarg_0);
                        generator.Emit(OpCodes.Ldc_I4, index);
                        generator.Emit(OpCodes.Callvirt, isDBNullMethod);
                        generator.Emit(OpCodes.Brtrue, endIfLabel);
                        generator.Emit(OpCodes.Ldloc, result);
                        generator.Emit(OpCodes.Ldarg_0);
                        generator.Emit(OpCodes.Ldc_I4, index);
                        generator.Emit(OpCodes.Callvirt, getValueMethod);
                        generator.Emit(OpCodes.Unbox_Any, propertyInfo.PropertyType);
                        generator.Emit(OpCodes.Callvirt, propertyInfo.GetSetMethod());
                        generator.MarkLabel(endIfLabel);
                    }
                }
                generator.Emit(OpCodes.Ldloc, result);
                generator.Emit(OpCodes.Ret);

                //构造完成以后传给rowMap
                rowMap = (Load<T>)method.CreateDelegate(typeof(Load<T>));
            }
            else
            {
                rowMap = (Load<T>)rowMapMethods[typeof(T)];
            }

            //遍历Datatable的rows集合，调用rowMap把DataRow转换为对象（T）
            foreach (DataRow info in dt.Rows)
                list.Add(rowMap(info));
            return list;

        }

        ///// <summary>
        ///// 快速转换类[数据量越大[估约500条起],性能越高]
        ///// </summary>
        //internal class FastToT<T>
        //{
        //    public delegate T EmitHandle(DataRow row);
        //    /// <summary>
        //    /// 构建一个ORM实体转换器
        //    /// </summary>
        //    /// <typeparam name="T">转换的目标类型</typeparam>
        //    /// <param name="schema">表数据架构</param>
        //    public static EmitHandle Create(DataTable schema)
        //    {
        //        Type tType = typeof(T);
        //        Type rowType = typeof(DataRow);
        //        DynamicMethod method = new DynamicMethod("RowToT", tType, new Type[] { rowType }, tType);

        //        MethodInfo getValue = rowType.GetMethod("GetItemValue", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, new Type[] { typeof(int) }, null);


        //        ILGenerator gen = method.GetILGenerator();

        //        gen.DeclareLocal(tType);
        //        gen.DeclareLocal(typeof(object));
        //        gen.DeclareLocal(typeof(bool));

        //        gen.Emit(OpCodes.Newobj, tType.GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, new Type[] { }, null));
        //        gen.Emit(OpCodes.Stloc_0);
        //        int ordinal = -1;

        //        foreach (FieldInfo field in tType.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public))
        //        {
        //            string fieldName = field.Name.TrimStart('_');
        //            ordinal = schema.GetOrdinal(fieldName);
        //            if (ordinal > -1)
        //            {
        //                Label retFalse = gen.DefineLabel();
        //                gen.Emit(OpCodes.Ldarg_0);
        //                gen.Emit(OpCodes.Ldc_I4, ordinal);
        //                gen.Emit(OpCodes.Call, getValue);
        //                gen.Emit(OpCodes.Stloc_1);

        //                gen.Emit(OpCodes.Ldloc_1);
        //                gen.Emit(OpCodes.Ldnull);
        //                gen.Emit(OpCodes.Ceq);
        //                gen.Emit(OpCodes.Stloc_2);
        //                gen.Emit(OpCodes.Ldloc_2);

        //                gen.Emit(OpCodes.Brtrue_S, retFalse);//为null值，跳过

        //                gen.Emit(OpCodes.Ldloc_0);
        //                gen.Emit(OpCodes.Ldloc_1);
        //                EmitCastObj(gen, field.FieldType);
        //                gen.Emit(OpCodes.Stfld, field);

        //                gen.MarkLabel(retFalse);//继续下一个循环
        //            }
        //        }

        //        gen.Emit(OpCodes.Ldloc_0);
        //        gen.Emit(OpCodes.Ret);

        //        return method.CreateDelegate(typeof(EmitHandle)) as EmitHandle;
        //    }

        //    private static void EmitCastObj(ILGenerator il, Type targetType)
        //    {
        //        if (targetType.IsValueType)
        //        {
        //            il.Emit(OpCodes.Unbox_Any, targetType);
        //        }
        //        else
        //        {
        //            il.Emit(OpCodes.Castclass, targetType);
        //        }
        //    }
        //}
    }
}
