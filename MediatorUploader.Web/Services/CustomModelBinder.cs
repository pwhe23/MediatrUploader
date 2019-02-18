using System.Collections.Generic;
using System.Web.Mvc;
using MediatorUploader.Domain;

namespace MediatorUploader.Web
{
    public class CustomModelBinder : DefaultModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            if (bindingContext.ModelType == typeof(List<StreamInfo>))
            {
                return BindMultipleFile(controllerContext, bindingContext);
            }
            else if (bindingContext.ModelType == typeof(StreamInfo))
            {
                return BindSingleFile(controllerContext, bindingContext);
            }
            else
            {
                return base.BindModel(controllerContext, bindingContext);
            }
        }

        private static object BindMultipleFile(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var result = new List<StreamInfo>();
            var requestFiles = controllerContext.HttpContext.Request.Files;

            for (var i = 0; i < requestFiles.Count; i++)
            {
                var key = requestFiles.GetKey(i);
                if (key != bindingContext.ModelName)
                    continue;

                var httpPostedFile = requestFiles[i];
                if (httpPostedFile == null || httpPostedFile.ContentLength == 0 || string.IsNullOrWhiteSpace(httpPostedFile.FileName))
                    continue;

                result.Add(new StreamInfo
                {
                    Filename = httpPostedFile.FileName,
                    Stream = httpPostedFile.InputStream,
                });
            }

            return result;
        }

        private static object BindSingleFile(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var httpPostedFile = controllerContext.HttpContext.Request.Files[bindingContext.ModelName];
            if (httpPostedFile == null || httpPostedFile.ContentLength == 0 || string.IsNullOrWhiteSpace(httpPostedFile.FileName))
                return null;

            var streamInfo = (StreamInfo)bindingContext.Model ?? new StreamInfo();
            streamInfo.Filename = httpPostedFile.FileName;
            streamInfo.Stream = httpPostedFile.InputStream;

            return streamInfo;
        }
    };
}