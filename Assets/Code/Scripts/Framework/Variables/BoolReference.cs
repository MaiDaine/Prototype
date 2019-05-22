using System;

namespace Prototype
{
    [Serializable]
    public class BoolReference
    {
        public bool useConstant = true;
        public bool constantValue;
        public BoolVariable variable;

        public BoolReference() { }

        public BoolReference(bool value)
        {
            useConstant = true;
            constantValue = value;
        }

        public bool Value
        {
            get { return useConstant ? constantValue : variable.value; }
        }

        public static implicit operator bool(BoolReference reference)
        {
            return reference.Value;
        }
    }
}