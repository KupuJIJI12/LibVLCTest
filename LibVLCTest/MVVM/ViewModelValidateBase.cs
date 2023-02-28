using OCode.MVVM.Base.Attributes;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OCode.MVVM.Base
{
    public class ViewModelValidateBase : ViewModelBase, INotifyDataErrorInfo
    {
        #region INotifyDataErrorInfo
        private ConcurrentDictionary<string, string> _errors = new ConcurrentDictionary<string, string>();

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public void OnErrorsChanged([CallerMemberName] string propertyName = null)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }

        public IEnumerable GetErrors(string propertyName)
        {
            _errors.TryGetValue(propertyName, out var errors);
            return new[] { errors };
        }

        public bool HasErrors => _errors.Any();

        protected void SetError(string propertyName, string errorMessage)
        {
            if (_errors.TryGetValue(propertyName, out var res))
            {
                _errors.TryUpdate(propertyName, errorMessage, res);
            }
            else
            {
                _errors.TryAdd(propertyName, errorMessage);
            }
            OnErrorsChanged(propertyName);
        }

        protected void AddError(string propertyName, string errorMessage)
        {
            _errors.TryAdd(propertyName, errorMessage);
            OnErrorsChanged(propertyName);
        }

        protected void RemoveError(string propertyName)
        {
            _errors.TryRemove(propertyName, out var res);
            OnErrorsChanged(propertyName);
        }
        #endregion

        protected async Task ValidatePropertyAsync(object value, [CallerMemberName] string propertyName = null,
            CancellationToken cancellationToken = default)
        {
            var attributes = this.GetType().GetProperty(propertyName)
                .GetCustomAttributes()
                .OfType<ValidateAttributeBase>()
                .ToList();

            foreach (var attr in attributes)
            {
                var validateMessage = await attr.Validator.ValidateAsync(value);
                if (validateMessage != null)
                {
                    SetError(propertyName, validateMessage);
                    return;
                }
                else
                {
                    RemoveError(propertyName);
                }
            }
        }
    }
}
