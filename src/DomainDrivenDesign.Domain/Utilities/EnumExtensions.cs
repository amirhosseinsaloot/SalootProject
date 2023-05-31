﻿using System.ComponentModel.DataAnnotations;

namespace DomainDrivenDesign.Domain.Utilities;

public static class EnumExtensions
{
    public static string? GetDisplayName(this Enum enumValue)
    {
        if (enumValue is null)
        {
            throw new ArgumentNullException($"{nameof(enumValue)} : {typeof(Enum)}");
        }

        var displayName = enumValue.ToString();
        var fieldInfo = enumValue.GetType().GetField(enumValue.ToString());

        var attrs = fieldInfo?.GetCustomAttributes(typeof(DisplayAttribute), true);
        if (attrs is { Length: > 0 })
        {
            displayName = ((DisplayAttribute)attrs[0]).Name;
        }

        return displayName;
    }
}
