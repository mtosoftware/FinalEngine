// <copyright file="EntityToIdentifierConverter.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Desktop.Converters
{
    using System;
    using System.ComponentModel;
    using System.Globalization;
    using FinalEngine.ECS;

    public class EntityToIdentifierConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return false;
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return destinationType == typeof(string);
        }

        public override object? ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (value is Entity entity)
            {
                return entity.Identifier.ToString().ToUpper(CultureInfo.InvariantCulture);
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}
