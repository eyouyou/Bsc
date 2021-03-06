﻿using System.IO;
using System.Web;

namespace Bsc.Dmtds.Sites.Web
{
    public class OutputTextWriterWrapper : StringWriter
    {
        public TextWriter Respone_Output_TextWriter { get; set; }

        bool rendered = false;
        public OutputTextWriterWrapper(TextWriter httpOutputWriter)
        {
            Respone_Output_TextWriter = httpOutputWriter;
        }
        public virtual TextWriter GetRawOuputWriter()
        {
            TextWriter textWriter = this;
            while (textWriter is OutputTextWriterWrapper && ((OutputTextWriterWrapper)textWriter).Respone_Output_TextWriter != null)
            {
                textWriter = ((OutputTextWriterWrapper)textWriter).Respone_Output_TextWriter;
            }
            return textWriter;
        }
        public override void Flush()
        {
            this.GetStringBuilder().Clear();
        }
        public virtual void Render(HttpResponseBase response)
        {
            if (!rendered)
            {
                var originalWriter = GetRawOuputWriter();
                response.Output = originalWriter;
                originalWriter.Write(this.ToString());
                rendered = true;
            }
        }
    }
    public static class RestoreToRawOutputTextWriterExtension
    {
        public static void RestoreRawOutput(this HttpResponseBase httpResponse)
        {
            if (httpResponse.Output is OutputTextWriterWrapper)
            {
                httpResponse.Output = ((OutputTextWriterWrapper)httpResponse.Output).GetRawOuputWriter();
            }
        }
    }
}