using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using SourceGeneratorToolkit;

namespace QuantitiesDotNet.Generators;

[Generator(LanguageNames.CSharp)]
public partial class Generator : IIncrementalGenerator
{
    private const string _attrName = "QuantitiesDotNet.QuantityAttribute";
    private const string _unitAttrName = "QuantitiesDotNet.QuantityUnitAttribute";
    private const string _operationAttrName = "QuantitiesDotNet.QuantityOperationAttribute";

    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var source = context.SyntaxProvider.ForAttributeWithMetadataName(
            _attrName,
            static (node, _) => node is StructDeclarationSyntax,
            static (cxt, _) =>
            {
                var symbol = (INamedTypeSymbol)cxt.TargetSymbol;
                var typeName = symbol.Name;
                var isRefLike = symbol.IsRefLikeType;
                var dimension = Dimension.GetDimension(cxt.Attributes[0]);
                var unitSymbols = UnitSymbol.GetUnitSymbols(cxt.GetAttributes(_unitAttrName));
                var operations = QuantityOperation.GetOperations(cxt.GetAttributes(_operationAttrName));
                return new QuantityDef(typeName, isRefLike, dimension, unitSymbols, operations);
            });
        context.RegisterSourceOutput(source, GenerateUnitTypeImplements);
    }


    private void GenerateUnitTypeImplements(
        SourceProductionContext context,
        QuantityDef quantityDef)
    {
        var canceller = context.CancellationToken;
        canceller.ThrowIfCancellationRequested();

        var (nonGeneric, generic) = QuantityImplementBuilderBase.Create(quantityDef);
        var sb = new SourceBuilderSlim();
        sb.AppendLine("""
            #nullable enable
            using System;
            using System.Collections;
            using System.Collections.Generic;
            using System.Collections.Immutable;
            using System.Diagnostics.CodeAnalysis;
            using System.Globalization;
            using System.Numerics;
            using System.Runtime.InteropServices;
            using System.Text;
            
            namespace QuantitiesDotNet
            {
            """);
        sb.PushIndent("    ");
        nonGeneric.Generate(sb);
        sb.PopIndent();
        sb.AppendLine("""
            }
            
            #if NET7_0_OR_GREATER
            namespace QuantitiesDotNet.Generic
            {
            """);
        sb.PushIndent("    ");
        generic.Generate(sb);
        sb.PopIndent();
        sb.AppendLine("}");
        sb.AppendLine("#endif");
        var source = sb.Build();
        context.AddSource(
            $"{quantityDef.TypeName}.g.cs",
            source);
    }
}
