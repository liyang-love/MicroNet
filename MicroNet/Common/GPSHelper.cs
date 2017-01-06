using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MicroNet.Common
{
    public sealed class GPSHelper
    {
        const double pi = 3.14159265358979324;
        const double ee = 0.00669342162296594323;
        const double a = 6378245.0;
        const double x_pi = 3.14159265358979324 * 3000.0 / 180.0;

        /// <summary>
        /// 火星坐标转百度坐标
        /// </summary>
        /// <param name="gg_lat">火星纬度</param>
        /// <param name="gg_lon">火星经度</param>
        /// <param name="bd_lat">百度纬度</param>
        /// <param name="bd_lon">百度经度</param>
        public static void GCJToBaidu(double gg_lat, double gg_lon, ref double bd_lat, ref double bd_lon)
        {
            double x = gg_lon, y = gg_lat;
            double z = Math.Sqrt(x * x + y * y) + 0.00002 * Math.Sin(y * x_pi);
            double theta = Math.Atan2(y, x) + 0.000003 * Math.Cos(x * x_pi);
            bd_lon = z * Math.Cos(theta) + 0.0065;
            bd_lat = z * Math.Sin(theta) + 0.006;
        }

        /// <summary>
        /// 百度坐标转火星坐标
        /// </summary>
        /// <param name="bd_lat">百度纬度</param>
        /// <param name="bd_lon">百度经度</param>
        /// <param name="gg_lat">火星纬度</param>
        /// <param name="gg_lon">火星经度</param>
        public static void BaiduToGCJ(double bd_lat, double bd_lon, ref double gg_lat, ref double gg_lon)
        {
            double x = bd_lon - 0.0065, y = bd_lat - 0.006;
            double z = Math.Sqrt(x * x + y * y) - 0.00002 * Math.Sin(y * x_pi);
            double theta = Math.Atan2(y, x) - 0.000003 * Math.Cos(x * x_pi);
            gg_lon = z * Math.Cos(theta);
            gg_lat = z * Math.Sin(theta);
        }

        /// <summary>
        /// 公式转换gps转baidu
        /// </summary>
        /// <param name="longitude">gps经度</param>
        /// <param name="latitude">gps纬度</param>
        /// <param name="longbaidu">百度经度</param>
        /// <param name="latbaidu">百度纬度</param>
        public static void GPSToBaiduByFunc(double longitude, double latitude,
                                                out double longbaidu, out double latbaidu)
        {
            double gg_lon;
            double gg_lat;
            TransformFromWGSToGCJ(longitude, latitude, out gg_lon, out gg_lat);
            TransformFromGCJToBD(gg_lon, gg_lat, out longbaidu, out latbaidu);
            return;
        }
        /// <summary>
        /// 公式转换baidu转gps
        /// </summary>
        /// <param name="longbaidu">百度经度</param>
        /// <param name="latbaidu">百度纬度</param>
        /// <param name="longitude">gps经度</param>
        /// <param name="latitude">gps纬度</param>
        public static void BaiduToGPSByFunc(double longbaidu, double latbaidu,
                                                out double longitude, out double latitude)
        {
            double gg_lon;
            double gg_lat;
            TransformFromBDToGCJ(longbaidu, latbaidu, out gg_lon, out gg_lat);
            TransformFromGCJToWGS(gg_lon, gg_lat, out longitude, out latitude);
            return;
        }
        /// <summary>
        /// 公式转换gps转谷歌
        /// </summary>
        /// <param name="longitude">gps经度</param>
        /// <param name="latitude">gps纬度</param>
        /// <param name="longgoogle">谷歌经度</param>
        /// <param name="latgoogle">谷歌纬度</param>
        public static void GPSToGoogleByFunc(double longitude, double latitude,
                                                out double longgoogle, out double latgoogle)
        {
            TransformFromWGSToGCJ(longitude, latitude, out longgoogle, out latgoogle);
            return;
        }

        /// <summary>
        /// 公式转换谷歌转gps
        /// </summary>
        /// <param name="longgoogle">谷歌经度</param>
        /// <param name="latgoogle">谷歌纬度</param>
        /// <param name="longitude">gps经度</param>
        /// <param name="latitude">gps纬度</param>
        public static void GoogleToGPSByFunc(double longgoogle, double latgoogle,
                                                out double longitude, out double latitude)
        {
            TransformFromGCJToWGS(longgoogle, latgoogle, out longitude, out latitude);
            return;
        }

        /// <summary>
        /// 公式转换百度转谷歌
        /// </summary>
        /// <param name="longbaidu">百度经度</param>
        /// <param name="latbaidu">百度纬度</param>
        /// <param name="longgoogle">谷歌经度</param>
        /// <param name="latgoogle">谷歌纬度</param>
        public static void BaiduToGoogleByFunc(double longbaidu, double latbaidu,
                                                out double longgoogle, out double latgoogle)
        {
            TransformFromBDToGCJ(longbaidu, latbaidu, out longgoogle, out latgoogle);
            return;
        }

        /// <summary>
        /// 公式转换谷歌转百度
        /// </summary>
        /// <param name="longgoogle">谷歌经度</param>
        /// <param name="latgoogle">谷歌纬度</param>
        /// <param name="longbaidu">百度经度</param>
        /// <param name="latbaidu">百度纬度</param>
        public static void GoogleToBaiduByFunc(double longgoogle, double latgoogle,
                                                out double longbaidu, out double latbaidu)
        {
            TransformFromGCJToBD(longgoogle, latgoogle, out longbaidu, out latbaidu);
            return;
        }

        /// <summary>
        /// 计算两点间距离
        /// </summary>
        /// <param name="longitude1">经度1</param>
        /// <param name="latitude1">纬度1</param>
        /// <param name="longitude2">经度2</param>
        /// <param name="latitude2">纬度2</param>
        /// <returns>距离</returns>
        public static double? GetDistance(double? longitude1,
            double? latitude1, double? longitude2, double? latitude2)
        {
            double s, radLat1, @radLat2, a, b;
            if (!longitude1.HasValue || !latitude1.HasValue
                || !longitude2.HasValue || !latitude2.HasValue)
            {
                return null;
            }
            radLat1 = latitude1.Value * Math.PI / 180.0;
            radLat2 = latitude2.Value * Math.PI / 180.0;
            a = radLat1 - radLat2;
            b = longitude1.Value * Math.PI / 180.0
                - longitude2.Value * Math.PI / 180.0;

            s = 2 * Math.Asin(Math.Sqrt(Math.Pow(Math.Sin(a / 2), 2) +
                    Math.Cos(@radLat1) * Math.Cos(@radLat2)
                    * Math.Pow(Math.Sin(@b / 2), 2)));
            s *= 6378.137;
            s = Math.Round(s * 10000, 3) / 10000;
            return @s * 1000;
        }


        #region====================================gps转google实现==================================
        private static void TransformFromWGSToGCJ(double lon, double lat, out double gg_lon, out double gg_lat)
        {
            if (lon < 72.004 || lon > 137.8347 || lat < 0.8293 || lat > 55.8271)
            {
                gg_lon = lon;
                gg_lat = lat;
                return;
            }
            double dLat = transformLat(lon - 105.0, lat - 35.0);
            double dLon = transformLon(lon - 105.0, lat - 35.0);
            double radLat = lat / 180.0 * pi;
            double magic = System.Math.Sin(radLat);
            magic = 1 - ee * magic * magic;
            double sqrtMagic = System.Math.Sqrt(magic);
            dLat = (dLat * 180.0) / ((a * (1 - ee)) / (magic * sqrtMagic) * pi);
            dLon = (dLon * 180.0) / (a / sqrtMagic * System.Math.Cos(radLat) * pi);
            gg_lat = lat + dLat;
            gg_lon = lon + dLon;
            return;
        }
        private static double transformLat(double x, double y)
        {
            double ret = -100.0 + 2.0 * x + 3.0 * y + 0.2 * y * y + 0.1 * x * y + 0.2 * System.Math.Sqrt(x > 0 ? x : -x);
            ret += (20.0 * System.Math.Sin(6.0 * x * pi) + 20.0 * System.Math.Sin(2.0 * x * pi)) * 2.0 / 3.0;
            ret += (20.0 * System.Math.Sin(y * pi) + 40.0 * System.Math.Sin(y / 3.0 * pi)) * 2.0 / 3.0;
            ret += (160.0 * System.Math.Sin(y / 12.0 * pi) + 320 * System.Math.Sin(y * pi / 30.0)) * 2.0 / 3.0;
            return ret;
        }
        private static double transformLon(double x, double y)
        {
            double ret = 300.0 + x + 2.0 * y + 0.1 * x * x + 0.1 * x * y + 0.1 * System.Math.Sqrt(x > 0 ? x : -x);
            ret += (20.0 * System.Math.Sin(6.0 * x * pi) + 20.0 * System.Math.Sin(2.0 * x * pi)) * 2.0 / 3.0;
            ret += (20.0 * System.Math.Sin(x * pi) + 40.0 * System.Math.Sin(x / 3.0 * pi)) * 2.0 / 3.0;
            ret += (150.0 * System.Math.Sin(x / 12.0 * pi) + 300.0 * System.Math.Sin(x / 30.0 * pi)) * 2.0 / 3.0;
            return ret;
        }
        #endregion

        #region====================================google转gps实现==================================
        private static void TransformFromGCJToWGS(double gg_lon, double gg_lat, out double lon, out double lat)
        {
            double wgLoclong = gg_lon;
            double wgLoclat = gg_lat;
            double currGcLoclon;
            double currGcLoclat;
            double dLoclon;
            double dLoclat;
            while (true)
            {
                TransformFromWGSToGCJ(wgLoclong, wgLoclat, out currGcLoclon, out currGcLoclat);
                dLoclat = gg_lat - currGcLoclat;
                dLoclon = gg_lon - currGcLoclon;
                if (System.Math.Abs(dLoclat) < 1e-8 && System.Math.Abs(dLoclon) < 1e-8)
                {
                    lon = wgLoclong;
                    lat = wgLoclat;
                    return;
                }
                wgLoclat += dLoclat;
                wgLoclong += dLoclon;
            }
        }
        #endregion

        #region====================================baidu转google实现================================
        private static void TransformFromBDToGCJ(double bd_lon, double bd_lat, out double gg_lon, out double gg_lat)
        {
            double x = bd_lon - 0.0065, y = bd_lat - 0.006;
            double z = System.Math.Sqrt(x * x + y * y) - 0.00002 * System.Math.Sin(y * x_pi);
            double theta = System.Math.Atan2(y, x) - 0.000003 * System.Math.Cos(x * x_pi);
            gg_lon = z * System.Math.Cos(theta);
            gg_lat = z * System.Math.Sin(theta);
            return;
        }
        #endregion

        #region====================================google转baidu实现================================
        private static void TransformFromGCJToBD(double gg_lon, double gg_lat, out double bd_lon, out double bd_lat)
        {
            double x = gg_lon, y = gg_lat;
            double z = System.Math.Sqrt(x * x + y * y) + 0.00002 * System.Math.Sin(y * x_pi);
            double theta = System.Math.Atan2(y, x) + 0.000003 * System.Math.Cos(x * x_pi);
            bd_lon = z * System.Math.Cos(theta) + 0.0065;
            bd_lat = z * System.Math.Sin(theta) + 0.006;
            return;
        }
        #endregion
    }
}
