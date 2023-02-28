using OCode.MVVM.Base.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace OCode.MVVM.Base.Attributes
{
    public class ValidateAttributeBase : Attribute
    {
        public IValidator Validator { get; set; }

        public ValidateAttributeBase(Type validatorType)
        {
            Validator = (IValidator)Activator.CreateInstance(validatorType);
        }
    }
}
