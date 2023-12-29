using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Media.Imaging;


namespace BankManage.utils {
    /// <summary>
    /// 文件是否占用
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns></returns>


    internal class FileUtils {
        //从本地上传图片
        public static BitmapImage UploadPicture() {
            OpenFileDialog openFileDialog = new OpenFileDialog {
                Filter = "Image files (*.jpg, *.jpeg, *.png)|*.jpg;*.jpeg;*.png|All files (*.*)|*.*"
            };

            if (openFileDialog.ShowDialog() == true) {
                return new BitmapImage(new Uri(openFileDialog.FileName));
            }
            return null;
        }

        //图片转Byte
        public static byte[] ImageToByte(BitmapImage image) {
            byte[] photoData;
            using (MemoryStream stream = new MemoryStream()) {
                BitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(image));
                encoder.Save(stream);
                photoData = stream.ToArray();
            }
            return photoData;
        }

        //Byte转图片
        public static BitmapImage ByteToImage(byte[] imageData) {
            BitmapImage biImg = new BitmapImage();
            try {
                using (MemoryStream ms = new MemoryStream(imageData)) {
                    biImg.BeginInit();
                    biImg.StreamSource = ms;
                    biImg.CacheOption = BitmapCacheOption.OnLoad;
                    biImg.EndInit();
                    biImg.Freeze();
                }
            } catch (Exception ex) {
                Debug.Print($"转换图片失败:: {ex.Message}");
            }
            return biImg;
        }




        //判断文件是否在被使用
        public static bool IsFileInUse(string fileName) {
            bool inUse = true;

            FileStream fs = null;
            try {

                fs = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.None);

                inUse = false;
            } catch {
            } finally {
                if (fs != null)

                    fs.Close();
            }
            return inUse;//true表示正在使用,false没有使用
        }
    }
}
