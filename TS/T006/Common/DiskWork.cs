using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace T006.Common
{
    /// <summary>
    /// 提供硬盘文件或文件夹处理。
    /// </summary>
    public static class DiskWork
    {
        /// <summary>
        /// 将文件剪切到指定文件夹中。
        /// </summary>
        /// <param name="strDestFolder">目标文件夹。</param>
        /// <param name="strCutFile">要剪切的文件。</param>
        /// <returns>0、成功剪切 1、目标文件夹不存在 2、要剪切的文件不存在 3、目标文件夹以存在同名文件 4、其它错误</returns>
        public static Int32 CutFileToFolder(String strDestFolder, String strCutFile)
        {
            //确保目标文件夹存在
            if (!Directory.Exists(strDestFolder))
            {
                return 1;
            }

            //确保要剪切的文件存在
            if (!File.Exists(strCutFile))
            {
                return 2;
            }

            //构造新文件名
            StringBuilder sbDestFileName = new StringBuilder(strDestFolder);
            sbDestFileName.Append("\\");
            sbDestFileName.Append(Path.GetFileName(strCutFile));
            String strDestFileName = sbDestFileName.ToString();

            //确保目标文件夹没有存在相同的文件
            if (File.Exists(strDestFileName))
            {
                return 3;
            }

            //可以剪切文件了
            try
            {
                File.Move(strCutFile, strDestFileName);
            }
            catch
            {
                return 4;
            }
            return 0;
        }

        /// <summary>
        /// 将文件夹剪切到指定文件夹中。
        /// </summary>
        /// <param name="strDestFolder">目标文件夹。</param>
        /// <param name="strCutFolder">要剪切的文件夹。</param>
        /// <returns>0、成功剪切 1、目标文件夹不存在 2、要剪切的文件夹不存在 3、目标文件夹中已存在同名文件夹</returns>
        public static Int32 CutFolderToFolder(String strDestFolder, String strCutFolder)
        {
            //确保目标文件夹存在
            if (!Directory.Exists(strDestFolder))
            {
                return 1;
            }

            //确保要剪切的文件夹存在
            if (!Directory.Exists(strCutFolder))
            {
                return 2;
            }

            //构造新的文件夹名称
            StringBuilder sbFolderName = new StringBuilder(strDestFolder);
            sbFolderName.Append(strCutFolder.Substring(strCutFolder.LastIndexOf("\\")));
            String strFolderName = sbFolderName.ToString();

            //确保目标文件夹中不存在同名文件夹
            if (Directory.Exists(strFolderName))
            {
                return 3;
            }

            //可以剪切文件夹了
            try
            {
                Directory.Move(strCutFolder, strFolderName);
            }
            catch
            {
                return 4;
            }
            return 0;
        }

        /// <summary>
        /// 复制文件。
        /// </summary>
        /// <param name="strDestFile">目标文件文件名。</param>
        /// <param name="strSrcFile">要复制的文件路径。</param>
        /// <param name="bOverWrite">标志是否覆盖。</param>
        /// <returns>0、复制成功 1、要复制的文件不存在 2、文件已存在 3、其它错误</returns>
        public static Int32 CopyFile(String strDestFile, String strSrcFile, Boolean bOverWrite)
        {
            if (!File.Exists(strSrcFile))
            {
                return 1;
            }

            if (!bOverWrite && File.Exists(strDestFile))
            {
                return 2;
            }

            try
            {
                File.Copy(strSrcFile, strDestFile, bOverWrite);
            }
            catch
            {
                return 3;
            }

            return 0;
        }

        /// <summary>
        /// 复制文件夹及其里面的所有文件和子文件夹。
        /// </summary>
        /// <param name="strDestFolder">目标文件夹名称。</param>
        /// <param name="strSrcFolder">要复制的文件夹名称。</param>
        /// <returns>0、成功复制 1、要复制的文件夹不存在 2、文件夹创建失败 3、文件拷贝失败</returns>
        public static Int32 CopyFolder(String strDestFolder, String strSrcFolder)
        {
            if (!Directory.Exists(strSrcFolder))
            {
                return 1;
            }

            //如果目标文件夹不存在则创建
            if (!Directory.Exists(strDestFolder))
            {
                try
                {
                    Directory.CreateDirectory(strDestFolder);
                }
                catch
                {
                    return 2;
                }
            }

            //拷贝里面的文件
            DirectoryInfo diSrc = new DirectoryInfo(strSrcFolder);
            FileInfo[] afiFiles = diSrc.GetFiles();
            foreach (FileInfo fiFile in afiFiles)
            {
                if (CopyFile(strDestFolder + "\\" + fiFile.Name, fiFile.FullName, true) != 0)
                {
                    return 3;
                }
            }

            //递归拷贝文件夹
            DirectoryInfo[] adiFolders = diSrc.GetDirectories();
            foreach (DirectoryInfo diFolder in adiFolders)
            {
                Int32 iRet = CopyFolder(strDestFolder + "\\" + diFolder.Name, diFolder.FullName);
                if (iRet != 0)
                {
                    return iRet;
                }
            }

            return 0;
        }

        /// <summary>
        /// 重命名文件。
        /// </summary>
        /// <param name="strFileName">要修改名称的文件，包含完整路径。</param>
        /// <param name="strNewName">要修改的名称（无路径，无后缀）。</param>
        /// <returns>0、成功 1、文件不存在 2、重命名后的文件已存在 3、其它错误</returns>
        public static Int32 ReNameFile(String strFileName, String strNewName)
        {
            //保证要改名的文件存在
            if (!File.Exists(strFileName))
            {
                return 1;
            }

            //获取改名后的新文件名
            String strExt = Path.GetExtension(strFileName);
            String strFolder = Path.GetDirectoryName(strFileName);
            StringBuilder sbNewFile = new StringBuilder(Path.GetDirectoryName(strFileName));
            sbNewFile.Append("\\");
            sbNewFile.Append(strNewName);
            sbNewFile.Append(strExt);
            String strNewFileName = sbNewFile.ToString();

            //保证改名后的文件不存在才修改
            if (File.Exists(strNewFileName))
            {
                return 2;
            }

            //给文件命名
            try
            {
                File.Move(strFileName, strNewFileName);
            }
            catch
            {
                return 3;
            }
            return 0;
        }

        /// <summary>
        /// 重命名文件夹。
        /// </summary>
        /// <param name="strFolderName">要修改名称的文件夹。</param>
        /// <param name="strNewName">要修改的名称。</param>
        /// <returns>0、文件夹重命名成功。 1、要重命名的文件夹不存在。 2、重命名后的文件夹已存在。 3、其它错误</returns>
        public static Int32 ReNameFolder(String strFolderName, String strNewName)
        {
            //保证文件夹存在
            if (!Directory.Exists(strFolderName))
            {
                return 1;
            }

            //获取改名后的文件夹名称
            StringBuilder sbNewFolder = new StringBuilder(strFolderName.Substring(0, strFolderName.LastIndexOf("\\") + 1));
            sbNewFolder.Append(strNewName);
            String strNewFolderName = sbNewFolder.ToString();

            //判断改名后的文件夹不存在才修改
            if (Directory.Exists(strNewFolderName))
            {
                return 2;
            }

            //开始给文件夹改名
            try
            {
                Directory.Move(strFolderName, strNewFolderName);
            }
            catch
            {
                return 3;
            }

            return 0;
        }

        /// <summary>
        /// 删除文件。
        /// </summary>
        /// <param name="strFileName">文件的决定路径，包含后缀名。</param>
        /// <returns>0、成功删除 1、文件不存在 2、其它错误</returns>
        public static Int32 DeleteFile(String strFileName)
        {
            //文件必须得存在
            if (!File.Exists(strFileName))
            {
                return 1;
            }

            try
            {
                File.Delete(strFileName);
            }
            catch
            {
                return 2;
            }

            return 0;
        }

        /// <summary>
        /// 删除文件夹。
        /// </summary>
        /// <param name="strFolderName">文件夹路径。</param>
        /// <returns>0、成功删除 1、文件夹不存在 2、其它错误</returns>
        public static Int32 DeleteFolder(String strFolderName)
        {
            //文件必须得存在
            DirectoryInfo diFolder = new DirectoryInfo(strFolderName);
            if (!diFolder.Exists)
            {
                return 1;
            }

            //删除文件夹极其子文件夹
            try
            {
                diFolder.Delete(true);
            }
            catch
            {
                return 2;
            }

            return 0;
        }

        /// <summary>
        /// 清空文件夹。
        /// </summary>
        /// <param name="strFolderName">文件夹名。</param>
        /// <returns>0、成功删除 1、文件夹不存在 2、其它错误</returns>
        public static Int32 ClearFolder(String strFolderName)
        {
            //文件必须得存在
            if (!Directory.Exists(strFolderName))
            {
                return 1;
            }

            //删除文件夹极其子文件夹
            try
            {
                String[] strFiles = Directory.GetFiles(strFolderName);
                String[] strFolders = Directory.GetDirectories(strFolderName);

                foreach (String strTemp in strFiles)
                {
                    File.Delete(strTemp);
                }
                foreach (String strTemp in strFolders)
                {
                    Directory.Delete(strTemp, true);
                }
            }
            catch
            {
                return 2;
            }

            return 0;
        }
    }
}
