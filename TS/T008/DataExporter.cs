using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XuXiang.ClassLibrary;

namespace T008
{
    /// <summary>
    /// 用于对数据进行导出。
    /// </summary>
    public abstract class DataExporter
    {
        /// <summary>
        /// 获取数据导出对象。
        /// </summary>
        /// <param name="type">数据类型。</param>
        /// <returns></returns>
        public static DataExporter GetDataExporter(string type)
        {
            string tl = type.ToLower();
            if (tl.CompareTo("bool") == 0)
            {
                return _cacheDataExporterBool;
            }
            else if (tl.CompareTo("int") == 0)
            {
                return _cacheDataExporterInt;
            }
            else if (tl.CompareTo("float") == 0)
            {
                return _cacheDataExporterFloat;
            }
            else if (tl.CompareTo("string") == 0)
            {
                return _cacheDataExporterString;
            }

            EnumInfo einfo = ConfigArchive.Instance.GetEnumInfo(type);
            if (einfo != null)
            {
                return new DataExporterEnum(einfo);
            }

            ClassInfo cinfo = ConfigArchive.Instance.GetClassInfo(type);
            if (cinfo != null)
            {
                return new DataExporterClass(cinfo);
            }

            MainForm.CurForm.Log("未知数据类型:{0}", type);
            return null;
        }

        /// <summary>
        /// 布尔值导出者。
        /// </summary>
        private static DataExporter _cacheDataExporterBool = new DataExporterBool();

        /// <summary>
        /// 整数导出者。
        /// </summary>
        private static DataExporter _cacheDataExporterInt = new DataExporterInt();

        /// <summary>
        /// 浮点数导出者。
        /// </summary>
        private static DataExporter _cacheDataExporterFloat = new DataExporterFloat();

        /// <summary>
        /// 字符串导出者。
        /// </summary>
        private static DataExporter _cacheDataExporterString = new DataExporterString();

        /// <summary>
        /// 导出数据。
        /// </summary>
        /// <param name="data">数据字符串。</param>
        /// <param name="stream">要写入到的数据流。</param>
        public abstract void Exprot(string data, Stream stream);
    }

    /// <summary>
    /// 布尔值导出。
    /// </summary>
    public class DataExporterBool : DataExporter
    {
        public override void Exprot(string data, Stream stream)
        {
            int b = (string.IsNullOrEmpty(data) || data.ToLower().CompareTo("0") == 0 || data.ToLower().CompareTo("false") == 0) ? 0 : 1;
            stream.WriteByte((byte)b);
        }
    }

    /// <summary>
    /// 整数导出。
    /// </summary>
    public class DataExporterInt : DataExporter
    {
        public override void Exprot(string data, Stream stream)
        {
            int i;
            if (!int.TryParse(data, out i))
            {
                i = 0;
            }
            DataUtil.WriteInt32(stream, i);
        }
    }

    /// <summary>
    /// 浮点数导出。
    /// </summary>
    public class DataExporterFloat : DataExporter
    {
        public override void Exprot(string data, Stream stream)
        {
            float f;
            if (!float.TryParse(data, out f))
            {
                f = 0;
            }
            DataUtil.WriteSingle(stream, f);
        }
    }

    /// <summary>
    /// 字符串导出。
    /// </summary>
    public class DataExporterString : DataExporter
    {
        public override void Exprot(string data, Stream stream)
        {
            DataUtil.WriteString(stream, data);
        }
    }

    /// <summary>
    /// 枚举导出，按枚举对应的Int值导出，不存在的枚举按0导出。
    /// </summary>
    public class DataExporterEnum : DataExporter
    {
        /// <summary>
        /// 导出对应的枚举配置.。
        /// </summary>
        private EnumInfo m_eiInfo = null;

        /// <summary>
        /// 构造函数。
        /// </summary>
        /// <param name="info">导出对应的枚举配置。</param>
        public DataExporterEnum(EnumInfo info)
        {
            m_eiInfo = info;
        }

        public override void Exprot(string data, Stream stream)
        {
            int v;
            if (!int.TryParse(data, out v))     //数字直接导出
            {
                v = 0;
                if (m_eiInfo != null)
                {
                    EnumItemInfo iteminfo = m_eiInfo.GetEnumItemInfo(data);
                    if (iteminfo != null)
                    {
                        v = iteminfo.Value;
                    }
                    else if (!string.IsNullOrEmpty(data))
                    {
                        MainForm.CurForm.Log("没有从枚举({0})中找到枚举项({1})。", m_eiInfo.Name, data);
                    }
                }
            }
            DataUtil.WriteInt32(stream, v);
        }
    }

    /// <summary>
    /// 类导出。
    /// </summary>
    public class DataExporterClass : DataExporter
    {
        /// <summary>
        /// 导出对应的枚举配置.。
        /// </summary>
        private ClassInfo m_ciInfo = null;

        /// <summary>
        /// 构造函数。
        /// </summary>
        /// <param name="info">导出对应的类配置。</param>
        public DataExporterClass(ClassInfo info)
        {
            m_ciInfo = info;
        }

        public override void Exprot(string data, Stream stream)
        {
            List<string> mdata = DataUtil.Split(data, "*", true);     //成员数量
            if (m_ciInfo.FieldInfos.Count != mdata.Count && mdata.Count > 0)
            {
                MainForm.CurForm.Log("成员数量与要导出的数据不一致 Class:{0} Data:{1}", m_ciInfo.Name, data);
            }

            for (int i = 0; i < m_ciInfo.FieldInfos.Count; ++i)
            {
                m_ciInfo.FieldInfos[i].Export(i<mdata.Count ? mdata[i] : string.Empty, stream);
            }
        }
    }
}
