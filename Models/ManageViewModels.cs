﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;

namespace Interface.Models
{
    public class IndexViewModel
    {
        public bool HasPassword { get; set; }
        public IList<UserLoginInfo> Logins { get; set; }
        public string PhoneNumber { get; set; }
        public bool TwoFactor { get; set; }
        public bool BrowserRemembered { get; set; }
    }

    public class ManageLoginsViewModel
    {
        public IList<UserLoginInfo> CurrentLogins { get; set; }
        public IList<AuthenticationDescription> OtherLogins { get; set; }
    }

    public class FactorViewModel
    {
        public string Purpose { get; set; }
    }

    public class SetPasswordViewModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "Parola trebuie să aibă cel puțin {2} caractere.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Parola nouă")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Reintrodu parola nouă")]
        [Compare("NewPassword", ErrorMessage = "Parolele nu se potrivesc.")]
        public string ConfirmPassword { get; set; }
    }

    public class ChangePasswordViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Parola actuala")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Parola trebuie să aibă cel puțin {2} caractere.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Parola nouă")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Reintrodu parola nouă")]
        [Compare("NewPassword", ErrorMessage = "Parolele nu se potrivesc.")]
        public string ConfirmPassword { get; set; }
    }

    public class ChangeSettingsViewModel
    {
        [Display(Name = "Durata medie activitate")]
        public double ActivityLenght { get; set; }

        [Display(Name = "Ziua in care incep studiile")]
        public string FirstDayOfWeek { get; set; }

    }
    public class ChangeUserAccountSettingsViewModel
    {
        [Display(Name = "Nume")]
        public string FullName { get; set; }

        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        [Display(Name = "Telefon")]
        public string PhoneNumber { get; set; }

    }

    public class AddPhoneNumberViewModel
    {
        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
        public string Number { get; set; }
    }

    public class VerifyPhoneNumberViewModel
    {
        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }

        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
    }

    public class ConfigureTwoFactorViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
    }
}