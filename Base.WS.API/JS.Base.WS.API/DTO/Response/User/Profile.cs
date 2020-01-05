﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JS.Base.WS.API.DTO.Response.User
{
    public class Profile
    {
        public User user { get; set; }
        public Person person { get; set; }
    }

    public class User
    {
        public long Id { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string EmailAddress { get; set; }
        public string Image { get; set; }
        public string Token { get; set; }
        public string WelcomeMessage { get; set; }
    }

    public class Person
    {
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Surname { get; set; }
        public string secondSurname { get; set; }
        public string BirthDate { get; set; }
        public string FullName { get; set; }
        public string Gender { get; set; }
        public List<Locators> Locators { get; set; }
    }

    public class Locators
    {
        public string Description { get; set; }
        public bool IsMain { get; set; }
        public string Type { get; set; }
    }
}