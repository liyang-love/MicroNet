using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace MicroNet.CacheHelper
{
    public abstract class SessionHandler : IDisposable
    {
        /// <summary>
        /// 缓存更新线程
        /// </summary>
        Thread SessionThread;

        /// <summary>
        /// 最后更新时间
        /// </summary>
        public DateTime LastModified { get; protected set; }

        /// <summary>
        /// 加载缓存
        /// </summary>
        protected abstract void LoadSession();

        /// <summary>
        /// 线程开始时间
        /// </summary>
        /// <param name="sleep">休眠时间</param>
        protected void StartSessionThread(int sleep)
        {
            SessionThread = new Thread(() =>
            {
                while (true)
                {
                    Thread.Sleep(sleep);
                    LoadSession();
                }
            });
            SessionThread.Start();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public virtual void Init()
        {
            LastModified = DateTime.MaxValue;
            LoadSession();
            StartSessionThread(1800000);
        }

        private bool Disposed;

        public void Dispose()
        {
            if (SessionThread != null)
            {
                SessionThread.Abort();
            }
            GC.SuppressFinalize(this);
        }
    }

    public abstract class ListSessionHandler<T> : SessionHandler, IDisposable
            where T : new()
    {

        /// <summary>
        /// 同步锁
        /// </summary>
        protected object Lock = new object();

        /// <summary>
        /// 缓存集合
        /// </summary>
        protected List<T> _session;

        /// <summary>
        /// 只读集合
        /// </summary>
        public IQueryable<T> Session
        {
            get
            {
                if (_session == null) return null;
                lock (Lock)
                {
                    return _session.AsQueryable<T>();
                }
            }
        }

        /// <summary>
        /// 数据库数据
        /// </summary>
        public abstract IQueryable<T> Database { get; }

        private bool Disposed;
    }

    public abstract class DictionarySessionHandler<T, V> : SessionHandler, IDisposable
    where V : new()
    {
        /// <summary>
        /// 同步锁
        /// </summary>
        protected object Lock = new object();

        /// <summary>
        /// 缓存集合
        /// </summary>
        protected Dictionary<T, V> _session;

        /// <summary>
        /// 只读集合
        /// </summary>
        public Dictionary<T, V> Session
        {
            get
            {
                if (_session == null) return null;
                lock (Lock)
                {
                    return _session;
                }
            }
        }

        /// <summary>
        /// 数据库数据
        /// </summary>
        public abstract Dictionary<T, V> Database { get; }

        private bool Disposed;
    }
}
