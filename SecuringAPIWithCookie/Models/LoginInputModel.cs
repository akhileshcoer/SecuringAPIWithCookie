﻿namespace SecuringAPIWithCookie.Models
{
    public class LoginInputModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ReturnUrl { get; set; }
    }
}
