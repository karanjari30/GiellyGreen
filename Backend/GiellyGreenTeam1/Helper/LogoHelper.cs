using System;
using System.IO;
using System.Web;

namespace GiellyGreenTeam1.Helper
{
    public class LogoHelper
    {
        public static string StoreLogoInFileSystem(string logo, string supplierName)
        {
            if (!String.IsNullOrEmpty(logo))
            {
                String path = HttpContext.Current.Server.MapPath("~/ImageStorage"); //Path
                                                                                    //Check if directory exist
                if (!System.IO.Directory.Exists(path))
                    System.IO.Directory.CreateDirectory(path); //Create directory if it doesn't exist

                string imageName = supplierName + ".jpg";
                //set the image path
                string imgPath = Path.Combine(path, imageName);
                byte[] imageBytes = Convert.FromBase64String(logo);
                System.IO.File.WriteAllBytes(imgPath, imageBytes);
                logo = imageName;
            }
            return logo;
        }
    }
}