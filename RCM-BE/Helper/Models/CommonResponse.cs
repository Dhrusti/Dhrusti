﻿using System.Net;

namespace Helper
{
    public class CommonResponse
    {
        public bool Status { get; set; } = false;
        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.InternalServerError;
        public string Message { get; set; } = CommonConstant.Something_Went_Wrong;
        public dynamic Data { get; set; } = null;
    }
}
