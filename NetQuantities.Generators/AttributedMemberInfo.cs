﻿using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetQuantities.Generators;

internal record AttributedMemberInfo<TSymbol>
    where TSymbol : ISymbol
{
    public TSymbol TargetSymbol { get; }
    public INamedTypeSymbol AttributeSymbol { get; }

    public AttributedMemberInfo(TSymbol target, INamedTypeSymbol attribute)
    {
        TargetSymbol = target;
        AttributeSymbol = attribute;
    }
}
