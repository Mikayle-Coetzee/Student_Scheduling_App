#region Documentation Header
// File: SignUpViewModel.cs
// Author: Mikayle Coetzee (ST10023767)
// Course: PROG6212 POE 2023
// Part: 2
// Description: This file contains the SignUpViewModel class, which is a ViewModel for signing up users in a WPF application.
// It handles user registration, input validation, and image uploading.
#endregion

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace _10023767_P2.Model
{
    public class ImageModel
    {
        public byte[] ConvertBitmapToByteArray(Bitmap bitmap)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Png);
                return memory.ToArray();
            }
        }

        public  Bitmap ConvertByteArrayToBitmap(byte[] imageBytes)
        {
            using (MemoryStream memory = new MemoryStream(imageBytes))
            {
                return new Bitmap(memory);
            }
        }

        internal byte[] ConvertBitmapToByteArray(byte[] bytes)
        {
            throw new NotImplementedException();
        }
    }
}
