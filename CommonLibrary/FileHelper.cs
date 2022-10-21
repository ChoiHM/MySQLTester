using System;
using System.IO;

namespace CommonLibrary
{
    public class FileHelper
    {
        /// <summary>
        /// 일정 날짜 이전의 파일을 모두 삭제합니다. (하위 폴더도 모두 탐색)
        /// </summary>
        /// <param name="DirectoryPath">최상위 디렉토리</param>
        /// <param name="OlderThan">지울 기준이 될 날짜를 입력 (예: DateTime.Now.AddMonths(-3) => 현재 날짜 기준 3개월 이전 파일 삭제)</param>
        public static void RemoveFiles(string DirectoryPath, DateTime OlderThan)
        {
            try
            {
                DirectoryInfo di = new DirectoryInfo(DirectoryPath);

                foreach (FileInfo file in di.GetFiles())
                {
                    try
                    {
                        FileInfo fi = new FileInfo(file.FullName);
                        if (fi.LastWriteTime < OlderThan)
                            fi.Delete();
                    }
                    catch
                    {
                        continue;
                    }
                }

                DirectoryInfo[] subFolders = di.GetDirectories();

                for (int i = 0; i < subFolders.Length; i++)
                {
                    RemoveFiles(subFolders[i].FullName, OlderThan);
                    if (Util.IsDirectoryEmpty(subFolders[i].FullName))
                    {
                        subFolders[i].Delete(true);
                    }
                }
            }
            catch
            {
                return;
            }
        }


    }
}
