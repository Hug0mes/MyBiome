using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBiome.Helpers
{
    public class ImageHelper 
    {
        
        static public string GetImageLink(string ImageName, string FolderName = "")
        {
            // if there are no image, our don't exists... (not done)
            if (ImageName == null)
            {
                ImageName = "no-image.png";
                FolderName = "";
            }
            else
            {
                FolderName += FolderName != "" ? "/" : "";
            }
            return "~/images/" + FolderName +ImageName;
        }

    }
}
