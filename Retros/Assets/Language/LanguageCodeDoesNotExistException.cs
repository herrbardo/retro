using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LanguageCodeDoesNotExistsException : Exception
{
    public LanguageCodeDoesNotExistsException(string code, Exception innerException):base("Code: " + code + " does nos exist", innerException)
    {
        
    }

    public LanguageCodeDoesNotExistsException(string code):this(code, null){}
}
