using System;
using System.Windows.Forms;
using System.IO;

namespace DVLD_Presentation
{
    public static class clsUtility
    {
        private static string _GenerateGUID()
        {
            Guid guid = Guid.NewGuid();

            return guid.ToString();
        }

        private static string _ReplaceFileNameToGuid(string FilePath)
        {
            FileInfo fileInfo = new FileInfo(FilePath);

            return _GenerateGUID() + fileInfo.Extension;
        }

        private static bool _CreatFolderIfDoesNotExist(string FolderPath)
        {
            if (!Directory.Exists(FolderPath))
            {
                try
                {
                    Directory.CreateDirectory(FolderPath);
                    return true;
                }
                catch (Exception e)
                {
                    MessageBox.Show("Failed to create folder: " + e.Message);
                    return false;
                }
            }

            return true;
        }

        public static bool CopyImageToFolder(ref string ImagePath)
        {
            string DestinationFolderPath = @"F:\Visual Studio\DVLD_PeopleImages\";

            if (!_CreatFolderIfDoesNotExist(DestinationFolderPath))
                return false;

            string DestinationFilePath = DestinationFolderPath + _ReplaceFileNameToGuid(ImagePath);

            try
            {
                File.Copy(ImagePath, DestinationFilePath, true);
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK);
                return false;
            }

            ImagePath = DestinationFilePath;
            return true;
        }
    }
}