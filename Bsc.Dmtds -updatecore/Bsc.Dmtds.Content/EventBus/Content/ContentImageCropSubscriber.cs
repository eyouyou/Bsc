using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using Bsc.Dmtds.Common.Drawing;
using Bsc.Dmtds.Common.IO;
using Bsc.Dmtds.Common.Util;
using Bsc.Dmtds.Content.Models;
using Bsc.Dmtds.Content.Models.Paths;

namespace Bsc.Dmtds.Content.EventBus.Content
{
    public class ContentImageCropSubscriber : ISubscriber
    {
        public EventResult Receive(IEventContext context)
        {
            EventResult eventResult = new EventResult();
            if (context is ContentEventContext && HttpContext.Current != null)
            {
                try
                {
                    ContentEventContext contentEventContext = (ContentEventContext)context;
                    var content = contentEventContext.Content;

                    var result = (contentEventContext.ContentAction & ContentAction.PreAdd) == ContentAction.PreAdd || (contentEventContext.ContentAction & ContentAction.PreUpdate) == ContentAction.PreUpdate;

                    if (result)
                    {
                        var cropField = HttpContext.Current.Request.Form["BSC-Image-Crop-Field"];
                        var toRemove = new List<string>();
                        if (!string.IsNullOrEmpty(cropField))
                        {
                            var fields = cropField.Split(',');
                            foreach (var field in fields)
                            {

                                var imgParam = JsonHelper.Deserialize<ImageParam>(HttpContext.Current.Request.Form[field + "_param"].ToString());
                                if (imgParam != null)
                                {
                                    string sourceFilePath = HttpContext.Current.Server.MapPath(imgParam.Url);

                                    toRemove.Add(sourceFilePath);

                                    var contentPath = new TextContentPath(content);

                                    var vPath = UrlUtility.Combine(contentPath.VirtualPath, "BSC-crop-" + Path.GetFileName(imgParam.Url));

                                    IOUtility.EnsureDirectoryExists(contentPath.PhysicalPath);

                                    var phyPath = HttpContext.Current.Server.MapPath(vPath);

                                    ImageTools.CropImage(sourceFilePath, phyPath, imgParam.X, imgParam.Y, imgParam.Width, imgParam.Height);
                                    content[field] = vPath;
                                }
                            }

                        }
                        foreach (var r in toRemove)
                        {
                            if (File.Exists(r))
                            {
                                File.Delete(r);
                            }

                        }
                    }
                }
                catch (Exception e)
                {
                    eventResult.Exception = e;
                }
            }

            return eventResult;
        }

        class ImageParam
        {
            public string Url { get; set; }

            public int X { get; set; }
            public int Y { get; set; }

            public int Width { get; set; }
            public int Height { get; set; }
        }

    }
}