using System;
namespace CoreUI.Dom.Styles
{
    public struct LengthHint
    {
        public float Value { get; }

        public MeasureUnit Unit { get; }

        public LengthHint(float value, MeasureUnit unit = MeasureUnit.Pixels)
        {
            Value = value;
            Unit = unit;
        }

        public static implicit operator LengthHint(float value) => new LengthHint(value);

        public static LengthHint operator +(LengthHint lengthHint, float value) => new LengthHint(lengthHint.Value + value, lengthHint.Unit);

        public static LengthHint operator -(LengthHint lengthHint, float value) => new LengthHint(lengthHint.Value - value, lengthHint.Unit);
    }
}
