using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MicroNet.Common
{
    /// <summary>
    /// 负载均衡算法
    /// </summary>
    public sealed class LoadBalancingHelper
    {
        /// <summary>
        /// 轮询负载均衡算法
        /// </summary>
        public static class RoundRobin
        {
            private static object obj = new object();

            static Dictionary<string, int> dic = new Dictionary<string, int>
        {
            { "192.168.1.12", 1},
            {"192.168.1.13", 1 },
            { "192.168.1.14", 3},
            { "192.168.1.15", 1},
            {"192.168.1.16", 1},
            {"192.168.1.17", 1 },
            { "192.168.1.18", 1},
            { "192.168.1.19", 1}
        };

            static int pos = 0;
            public static string roundRobin()
            {
                var keyList = dic.Keys.ToList();


                string server = null;

                lock (obj)
                {
                    if (pos >= keyList.Count)
                    {
                        pos = 0;
                    }
                    server = keyList[pos];
                    pos++;
                }
                return server;
            }
        }

        /// <summary>
        /// 加权轮询算法
        /// </summary>
        public static class WeightRoundRobin
        {
            private static object obj = new object();
            private static int pos = 0;

            static Dictionary<string, int> dic = new Dictionary<string, int>
        {
            { "192.168.1.12", 1},
            {"192.168.1.13", 1 },
            { "192.168.1.14", 3},
            { "192.168.1.15", 1},
            {"192.168.1.16", 1},
            {"192.168.1.17", 1 },
            { "192.168.1.18", 1},
            { "192.168.1.19", 1}
        };

            public static string roundRobin()
            {
                //獲取ip列表list
                List<string> it = dic.Keys.ToList();

                List<String> serverList = new List<string>();

                foreach (var item in it)
                {
                    int weight = 0;
                    dic.TryGetValue(item, out weight);

                    for (int i = 0; i < weight; i++)
                    {
                        serverList.Add(item);
                    }
                }

                string server = null;

                lock (obj)
                {
                    if (pos >= serverList.Count)
                    {
                        pos = 0;
                    }
                    server = serverList[pos];
                    pos++;
                }
                return server;
            }

        }

        /// <summary>
        /// 加权随机负载均衡算法
        /// </summary>
        public static class WeightRandom
        {

            static Dictionary<string, int> dic = new Dictionary<string, int>
        {
            { "192.168.1.12", 1},
            {"192.168.1.13", 1 },
            { "192.168.1.14", 3},
            { "192.168.1.15", 1},
            {"192.168.1.16", 1},
            {"192.168.1.17", 1 },
            { "192.168.1.18", 1},
            { "192.168.1.19", 1}
        };

            public static string weightRandom()
            {
                //獲取ip列表list
                List<string> it = dic.Keys.ToList();

                List<String> serverList = new List<string>();

                foreach (var item in it)
                {
                    int weight = 0;
                    dic.TryGetValue(item, out weight);

                    for (int i = 0; i < weight; i++)
                    {
                        serverList.Add(item);
                    }
                }
                Random random = new Random();
                int randomPos = random.Next(serverList.Count);
                string server = serverList[randomPos];
                return server;
            }
        }

        /// <summary>
        /// IP Hash负载均衡算法
        /// </summary>
        public static class IpHash
        {
            static Dictionary<string, int> dic = new Dictionary<string, int>
        {
            { "192.168.1.12", 1},
            {"192.168.1.13", 1 },
            { "192.168.1.14", 3},
            { "192.168.1.15", 1},
            {"192.168.1.16", 1},
            {"192.168.1.17", 1 },
            { "192.168.1.18", 1},
            { "192.168.1.19", 1}
        };

            public static string ipHash(string remoteIp)
            {
                List<string> keys = dic.Keys.ToList();

                int hashCode = Math.Abs(remoteIp.GetHashCode());
                int serverListSize = keys.Count;
                int serverPos = hashCode % serverListSize;

                return keys[serverPos];
            }

        }
    }
}
