using System;
using CarTax.Dto;

namespace CarTax.Validation
{
    public abstract class ValidationInput
    {
        public virtual double GetUserPrice(CarInput input)
        {
            throw new NotImplementedException();
        }
    }
}
