﻿namespace ServerBackendHooliTees.Models.Dtos;

public class CreateUser
{
    public string Name { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }

    public string Address { get; set; }
}
