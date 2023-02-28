using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OCode.MVVM.Base.Interfaces
{
    public interface IValidator
    {
        Task<string> ValidateAsync(object value, CancellationToken cancellationToken = default);
        void SetParameters(object parameters);
    }
}
