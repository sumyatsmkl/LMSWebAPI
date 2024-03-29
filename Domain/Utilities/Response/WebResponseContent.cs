﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Enums;

namespace Domain.Utilities.Response
{
    public class WebResponseContent
    {

        public WebResponseContent()
        {
        }
        public WebResponseContent(bool status)
        {
            this.Status = status;
        }
        public bool Status { get; set; }
        public string Code { get; set; }
        public string Message { get; set; }
       
        public object Data { get; set; }

        public string AccessToken { get; set; }
        public DateTime TokenExpired { get; set; }

        public WebResponseContent OK()
        {
            this.Status = true;
            return this;
        }

        public static WebResponseContent Instance
        {
            get { return new WebResponseContent(); }
        }
        public WebResponseContent OK(string statusCode,string message = null, object data = null)
        {
            this.Status = true;
            this.Code = statusCode;
            this.Message = message;
            this.Data = data;
            return this;
        }

        public WebResponseContent LoginOK(string loginToken,DateTime tokenExpired,string statusCode, string message = null, object data = null)
        {
            this.Status = true;
            this.Code = statusCode;
            this.Message = message;
            this.Data = data;
            this.TokenExpired = tokenExpired;
            this.AccessToken = loginToken;

            return this;
        }
        public WebResponseContent OK(ResponseType responseType)
        {
            return Set(responseType, true);
        }
        public WebResponseContent Error(string message = null)
        {
            this.Status = false;
            this.Message = message;
            return this;
        }

        public WebResponseContent Warning(string code, string message = null)
        {
            this.Status = false;
            this.Code = code;
            this.Message = message;
            return this;
        }
        public WebResponseContent Error(ResponseType responseType)
        {
            return Set(responseType, false);
        }
        public WebResponseContent Set(ResponseType responseType)
        {
            bool? b = null;
            return this.Set(responseType, b);
        }
        public WebResponseContent Set(ResponseType responseType, bool? status)
        {
            return this.Set(responseType, null, status);
        }
        public WebResponseContent Set(ResponseType responseType, string msg)
        {
            bool? b = null;
            return this.Set(responseType, msg, b);
        }
        public WebResponseContent Set(ResponseType responseType, string msg, bool? status)
        {
            if (status != null)
            {
                this.Status = (bool)status;
            }
            this.Code = ((int)responseType).ToString();
            if (!string.IsNullOrEmpty(msg))
            {
                Message = msg;
                return this;
            }
           // Message = responseType.GetMsg();
            return this;
        }
    }
}
