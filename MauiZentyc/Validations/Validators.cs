using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MauiZentyc.Validations;

public static class Validators
{
    public static bool ValidateEmail(string email)
    {
        return new EmailAddressAttribute().IsValid(email);
    }

    public static bool ValidatePhone(string phone)
    {
        return !string.IsNullOrWhiteSpace(phone) && phone.Length >= 8;
    }

    public static bool ValidateRequired(string value)
    {
        return !string.IsNullOrWhiteSpace(value);
    }

    public static bool ValidateNumber(decimal value, decimal min = 0)
    {
        return value >= min;
    }
}
