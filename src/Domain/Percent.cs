using System;

namespace Domain    //TODO: proper namespace
{
    public struct Percent
    {
        private readonly decimal _value;
        private Percent(decimal value) => _value = value;

        public static implicit operator Percent(decimal value) => new Percent(value);

        public decimal Of(decimal annualSalary)
        {
            var multiplier = _value / 100;
            var product = multiplier * annualSalary;
            return Math.Round(product, 2);
        }
    }
}
