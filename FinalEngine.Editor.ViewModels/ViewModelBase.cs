// <copyright file="ViewModelBase.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.CompilerServices;
    using FinalEngine.Editor.ViewModels.Converters;

    public abstract class ViewModelBase : INotifyPropertyChanged, IDataErrorInfo
    {
        private readonly IList<string?> errors;

        public ViewModelBase()
        {
            this.errors = new List<string?>();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public string? Error { get; }

        [TypeConverter(typeof(BooleanInverseConverter))]
        public bool HasErrors
        {
            get { return this.errors.Count > 0; }
        }

        public string this[string columnName]
        {
            get
            {
                this.ValidateProperty(columnName, out string? errorMessage);
                return errorMessage ?? string.Empty;
            }
        }

        protected void NotifyPropertyChanged(string propertyName)
        {
            if (propertyName == null)
            {
                throw new ArgumentNullException(nameof(propertyName));
            }

            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetProperty<T>(ref T field, T value, bool validate = false, [CallerMemberName] string? propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value))
            {
                return false;
            }

            field = value;
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

            if (validate)
            {
                this.ValidateProperty(propertyName!);
            }

            return true;
        }

        protected bool ValidateProperty(string propertyName, out string? errorMessage)
        {
            var propertyInfo = this.GetType().GetProperty(propertyName);

            if (propertyInfo == null)
            {
                throw new ArgumentException($"The specified {nameof(propertyName)} parameter couldn't be located.", nameof(propertyName));
            }

            object[] attributes = propertyInfo.GetCustomAttributes(true);

            foreach (object attribute in attributes)
            {
                if (attribute is not ValidationAttribute validator)
                {
                    continue;
                }

                if (validator.IsValid(propertyInfo.GetValue(this)))
                {
                    if (this.errors.Contains(validator.ErrorMessage))
                    {
                        this.errors.Remove(validator.ErrorMessage);
                        this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.HasErrors)));
                    }

                    continue;
                }

                this.errors.Add(validator.ErrorMessage);
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.HasErrors)));

                errorMessage = validator.ErrorMessage;
                return false;
            }

            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.HasErrors)));

            errorMessage = null;
            return true;
        }

        private bool ValidateProperty(string propertyName)
        {
            return this.ValidateProperty(propertyName, out _);
        }
    }
}
