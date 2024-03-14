﻿using System.Net;

namespace WEB.Utilities
{
    public class APIResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public bool IsSuccess { get; set; } = true;
        public List<string>? ErrorMessage { get; set; }
        public object? Result { get; set; }
    }
}
