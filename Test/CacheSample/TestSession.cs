using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.CacheSample
{
    //public class TestSession : SessionHandler<EntityOWorker, string>
    //{
    //    /// <summary>
    //    /// 加载数据
    //    /// </summary>
    //    public override List<EntityOWorker> Database
    //    {
    //        get
    //        {
    //            return FacadeProxy.GetProxyPub().GetWorder();
    //        }
    //    }

    //    public override void ReLoadSession()
    //    {
    //        LoadListSession();
    //    }

    //    protected override void LoadDictionarySession()
    //    {

    //    }

    //    protected override void LoadListSession()
    //    {
    //        try
    //        {
    //            var list = FacadeProxy.GetProxyPub().GetWorder();
    //            lock (Lock)
    //            {
    //                if (_sessionList != null)
    //                {
    //                    _sessionList.Clear();
    //                }
    //                _sessionList = list;
    //                LastModified = DateTime.Now;
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            LastModified = DateTime.MaxValue;
    //            LogHelpers.WriteLog("加载工作人员信息缓存失败:" + ex.ToString());
    //        }
    //    }
    //}
}
