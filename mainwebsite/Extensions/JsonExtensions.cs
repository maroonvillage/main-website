using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace MainWebsite.Extensions
{
    public class JsonNetResult : JsonResult
    {
        public JsonNetResult()
        {
            Settings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Error
            };
        }
        public JsonNetResult(JsonResult existing)
        {
            this.ContentEncoding = existing.ContentEncoding;
            this.ContentType = !string.IsNullOrWhiteSpace(existing.ContentType) ? existing.ContentType : "application/json";
            this.Data = existing.Data;
            this.JsonRequestBehavior = existing.JsonRequestBehavior;
            Settings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Error
            };
        }

        public JsonSerializerSettings Settings { get; private set; }

        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            var response = context.HttpContext.Response;

            response.ContentType = !String.IsNullOrEmpty(ContentType) ? ContentType : "application/json";

            if (ContentEncoding != null)
                response.ContentEncoding = ContentEncoding;

            if (Data == null)
                return;

            // If you need special handling, you can call another form of SerializeObject below

            var serializedObject = JsonConvert.SerializeObject(Data); //JsonConvert.SerializeObject(Data, Formatting.Indented);
            response.Write(serializedObject);
        }

    }
}