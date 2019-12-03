using System;
namespace CoreUI.Dom.Styles
{
    public struct LengthHint
    {
        public float Value { get; }

        public LengthUnit Units { get; }

        public LengthHint(float value, LengthUnit unit = LengthUnit.Pixels)
        {
            Value = value;
            Units = unit;
        }

        public static implicit operator LengthHint(float value) => new LengthHint(value);

        public static LengthHint operator +(LengthHint lengthHint, float value) => new LengthHint(lengthHint.Value + value, lengthHint.Units);

        public static LengthHint operator -(LengthHint lengthHint, float value) => new LengthHint(lengthHint.Value - value, lengthHint.Units);
    }
}
