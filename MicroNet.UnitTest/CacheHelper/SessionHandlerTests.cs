using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.CacheSample
{
    //public class SessionFactory
    //{
    //    /// <summary>
    //    /// 单例模式
    //    /// </summary>
    //    public readonly static SessionFactory Singleton = new SessionFactory();

    //    /// <summary>
    //    /// 私有构造函数
    //    /// </summary>
    //    private SessionFactory()
    //    {
    //        _chargeItem = new ChargeItemSession();
    //        _chargeItem.Init();

    //        _workers = new Client.WorkerSession();
    //        _workers.Init();

    //    }

    //    #region 收费项目缓存
    //    /// <summary>
    //    /// 收费项目缓存
    //    /// </summary>
    //    ChargeItemSession _chargeItem;

    //    /// <summary>
    //    /// 收费项目缓存
    //    /// </summary>
    //    public List<EntityChargeItem> ChargeItemSession
    //    {
    //        get
    //        {
    //            if (_chargeItem.SessionList == null)
    //            {
    //                return _chargeItem.Database;
    //            }
    //            return _chargeItem.SessionList;
    //        }
    //    }

    //    /// <summary>
    //    /// 收费项目缓存
    //    /// </summary>
    //    public Dictionary<string, EntityChargeItem> ChargeItemSessionDic
    //    {
    //        get
    //        {
    //            return _chargeItem.SessionDictionary;
    //        }
    //    }

    //    #endregion

    //    #region 工作人员信息缓存

    //    /// <summary>
    //    /// 工作人员缓存信息
    //    /// </summary>
    //    WorkerSession _workers;

    //    /// <summary>
    //    /// 工作人员缓存信息
    //    /// </summary>
    //    public List<EntityOWorker> WorkerSession
    //    {
    //        get
    //        {
    //            if (_workers.SessionList == null) { return _workers.Database; }
    //            return _workers.SessionList;
    //        }
    //    }

    //    /// <summary>
    //    /// 工作人员缓存信息
    //    /// </summary>
    //    public Dictionary<string, EntityOWorker> WorkerSessionDic
    //    {
    //        get { return _workers.SessionDictionary; }
    //    }

    //    #endregion 


    //    /// <summary>
    //    /// 刷新缓存
    //    /// </summary>
    //    public void Reload()
    //    {
    //        _chargeItem.ReLoadSession();

    //        _workers.ReLoadSession();
    //    }
    //}
}





//-------------------------------------------使用--------------------------
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
