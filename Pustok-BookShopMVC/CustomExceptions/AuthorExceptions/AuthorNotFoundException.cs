﻿namespace Pustok_BookShopMVC.CustomExceptions.AuthorExceptions;

public class AuthorNotFoundException:Exception
{
    public AuthorNotFoundException() { }
    public AuthorNotFoundException(string? message) : base(message) { }
}
