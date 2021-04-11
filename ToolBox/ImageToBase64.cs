using System;
using System.Collections.Generic;
using System.Drawing;

namespace ToolBox
{

    /// <summary>
    /// Wandelt ein Bild in einen Base64-String und zurück
    /// </summary>
    public static class ImageToBase64
    {
        /// <summary>
        /// Konvertiert ein Bild in einen Base64-String
        /// </summary>
        /// <param name="image">
        /// Zu konvertierendes Bild
        /// </param>
        /// <returns>
        /// Base64 Repräsentation des Bildes
        /// </returns>
        public static string ToBase64(this Image image)
        {
            if (image != null)
            {
                ImageConverter ic = new ImageConverter();
                byte[] buffer = (byte[])ic.ConvertTo(image, typeof(byte[]));
                return Convert.ToBase64String(buffer, Base64FormattingOptions.InsertLineBreaks);
            }
            return null;
        }
        //---------------------------------------------------------------------
        /// <summary>
        /// Konvertiert einen Base64-String zu einem Bild
        /// </summary>
        /// <param name="base64String">
        /// Zu konvertierender String
        /// </param>
        /// <returns>
        /// Bild das aus dem String erzeugt wird
        /// </returns>
        public static Image FromBase64(this Image image, string base64String)
        {
            byte[] buffer = Convert.FromBase64String(base64String);

            if (buffer != null)
            {
                ImageConverter ic = new ImageConverter();
                return ic.ConvertFrom(buffer) as Image;
            }
            return null;
        }
    }
}
