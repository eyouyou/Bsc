﻿using System.ComponentModel.DataAnnotations;

namespace Bsc.Dmtds.Web.Areas.Account.Models
{
    public class LoginModel
    {

        [Required]
        [Display(Name = "电子邮件")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string Password { get; set; }

        [Display(Name = "记住我?")]
        public bool RememberMe { get; set; }
        
    }
}