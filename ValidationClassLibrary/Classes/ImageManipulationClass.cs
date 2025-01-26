#region Documentation Header
// File: ImageManipulationClass.cs
// Author: Mikayle Coetzee (ST10023767)
// Course: PROG6212 POE 2023
// Part: 1
// Description: This class is for image manipulation and processing.
#endregion

using System.IO;

namespace CustomClassLibrary
{
    /// <summary>
    /// This class is for image manipulation and processing.
    /// </summary>
    public class ImageManipulationClass
    {
        /// <summary>
        /// Default Constructor of the ImageManipulationClass
        /// </summary>
        public ImageManipulationClass()
        {
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// This method converts a System.Drawing.Bitmap image to a byte array.
        /// </summary>
        /// <param name="image">The input image to be processed.</param>
        /// <returns>A byte array representing the processed image.</returns>
        public byte[] PhotoProcess(System.Drawing.Bitmap image)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                return ms.ToArray();
            }
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// This method converts a byte array back to a System.Drawing.Bitmap image.
        /// </summary>
        /// <param name="imageData">The byte array representing the image data.</param>
        /// <returns>The reconstructed System.Drawing.Bitmap image.</returns>
        public System.Drawing.Bitmap PhotoBuilder(byte[] imageData)
        {
            using (MemoryStream ms = new MemoryStream(imageData))
            {
                return new System.Drawing.Bitmap(ms);
            }
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// This method opens a file dialog to select an image file and returns the loaded image and file name.
        /// </summary>
        /// <returns>A tuple containing the loaded image and the file name.</returns>
        public (System.Drawing.Bitmap, string) GetPhoto()
        {
            Microsoft.Win32.OpenFileDialog open = new Microsoft.Win32.OpenFileDialog()
            {
                Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp; *.png) | *.jpg; *.jpeg; *.gif; *.bmp; *.png; *.jfif"
            };

            if (open.ShowDialog() == true)
            {
                var image = new System.Drawing.Bitmap(open.FileName);
                return (image, open.FileName);
            }

            return (null, null);
        }
        //・♫-------------------------------------------------------------------------------------------------♫・//
    }
}//★---♫:;;;: ♫ ♬:;;;:♬ ♫:;;;: ♫ ♬:;;;:♬ ♫---★・。。END OF FILE 。。・★---♫ ♬:;;;:♬ ♫:;;;: ♫ ♬:;;;:♬ ♫:;;;: ♫---★//
