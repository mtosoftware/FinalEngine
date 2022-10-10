// <copyright file="DarkDialogError.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Desktop.Views
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Windows.Forms;
    using DarkUI.Forms;

    public partial class DarkDialogError : DarkDialog, IDataErrorInfo
    {
        private const int ErrorIconPadding = 10;

        public DarkDialogError()
        {
            this.InitializeComponent();
        }

        public string? Error { get; }

        public string this[string columnName]
        {
            get
            {
                var propertyInfo = this.GetType().GetProperty(columnName);

                if (propertyInfo == null)
                {
                    return string.Empty;
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
                        continue;
                    }

                    return validator.ErrorMessage ?? string.Empty;
                }

                return string.Empty;
            }
        }

        private void DarkDialogError_ControlAdded(object sender, ControlEventArgs e)
        {
            if (this.ErrorProvider == null)
            {
                return;
            }

            this.InitializeDefaultIconPadding(e.Control);
        }

        private void InitializeDefaultIconPadding(Control control)
        {
            foreach (object? child in control.Controls)
            {
                if (child is not Control childControl)
                {
                    continue;
                }

                this.InitializeDefaultIconPadding(childControl);
            }

            this.ErrorProvider.SetIconPadding(control, ErrorIconPadding);
        }
    }
}
