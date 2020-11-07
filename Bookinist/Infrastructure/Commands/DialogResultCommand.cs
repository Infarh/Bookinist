using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace Bookinist.Infrastructure.Commands
{
    class DialogResultCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool? DialogResult { get; set; }

        public bool CanExecute(object parameter) => App.CurrentWindow != null;

        public void Execute(object parameter)
        {
            if (!CanExecute(parameter)) return;

            var window = App.CurrentWindow;

            var dialog_result = DialogResult;
            if (parameter != null)
                dialog_result = Convert.ToBoolean(parameter);

            window.DialogResult = dialog_result;
            window.Close();
        }
    }
}
