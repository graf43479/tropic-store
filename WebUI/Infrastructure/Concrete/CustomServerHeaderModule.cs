﻿using System;
using System.Web;

namespace WebUI.Infrastructure.Concrete
{
    public class CustomServerHeaderModule : IHttpModule
    {
        public void Init(HttpApplication context)
        {
            context.PreSendRequestHeaders += OnPreSendRequestHeaders;
        }

        public void Dispose() { }

        private void OnPreSendRequestHeaders(object sender, EventArgs e)
        {
            HttpContext.Current.Response.Headers.Set("Server", "Apache 2000 Server");
        }
    }

}