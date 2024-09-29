﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Surfs_Up.Models
{
    [Index(nameof(Email), IsUnique = true)]
    public class User : IdentityUser
    {
        public int UserId { get; set; }

        [StringLength(100)]
        [MaxLength(100, ErrorMessage = "Navn må maks være 50 tegn")]
        [Required(ErrorMessage = "Indtast gyldigt navn")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Indtats venligst et gyldigt Email")]
        [MaxLength(50, ErrorMessage = "Email må maks være 50 tegn")]
        public string Email { get; set; }

        [MaxLength(25, ErrorMessage = "Adgangskode må maks være 25 tegn")]
        [MinLength(8, ErrorMessage = "Adgangskode skal være minimum 8 tegn")]
        public string? Password { get; set; }

        //public User()
        //{
        //    if (Password == null)
        //    {
        //        Password = generateCustomerPassword();
        //    }
        //}

        //public string generateCustomerPassword()
        //{
        //    const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
        //    StringBuilder res = new StringBuilder();
        //    Random rnd = new Random();
        //    int length = 10;
        //    while (0 < length--)
        //    {
        //        res.Append(valid[rnd.Next(valid.Length)]);
        //    }
        //    return res.ToString();
        //}
    }
}