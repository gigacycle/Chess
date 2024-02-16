using System;
using System.Drawing;
using System.IO;
using System.Reflection;

namespace Chess
{
    public static class ImageTool
    {
        public static Image ReadResourceImage(string imageName)
        {
            Image image = null;
            try
            {
                Assembly assembly = Assembly.GetExecutingAssembly();
                string resourceName = $"{assembly.GetName().Name}.Resources.{imageName}";
                using (Stream stream = assembly.GetManifestResourceStream(resourceName))
                {
                    if (stream == null)
                    {
                        return image;
                    }
                    image = Image.FromStream(stream);
                }
            }
            catch { }
            return image;
        }
    }
}
