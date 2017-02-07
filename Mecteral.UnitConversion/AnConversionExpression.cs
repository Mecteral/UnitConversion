﻿namespace Mecteral.UnitConversion
{
    public abstract class AnConversionExpression : IConversionExpressionWithValue
    {
        public IConversionExpression Parent { get; protected internal set; }
        public bool HasParent => null != Parent;
        public abstract void Accept(IConversionExpressionVisitor visitor);
        public abstract void ReplaceChild(IConversionExpression oldChild, IConversionExpression newChild);
        public abstract decimal Value { get; set; }
    }
}
