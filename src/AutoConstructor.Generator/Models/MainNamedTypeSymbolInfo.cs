using System.Collections.Immutable;
using AutoConstructor.Generator.Core;
using AutoConstructor.Generator.Extensions;
using Microsoft.CodeAnalysis;

namespace AutoConstructor.Generator.Models;

internal sealed record MainNamedTypeSymbolInfo(
    string Name,
    bool IsStatic,
    EquatableArray<string> TypeParameters,
    NamespaceInfo Namespace,
    EquatableArray<NamedTypeSymbolInfo> ContainingTypes,
    bool HasParameterlessConstructor,
    string Filename,
    string Accessibility,
    InitializerMethodInfo? InitializerMethod)
    : NamedTypeSymbolInfo(Name, IsStatic, TypeParameters)
{
    public MainNamedTypeSymbolInfo(
        INamedTypeSymbol namedTypeSymbol,
        bool hasParameterlessConstructor,
        string filename,
        string accessibility,
        InitializerMethodInfo? initializerMethod)
        : this(
            namedTypeSymbol.Name,
            namedTypeSymbol.IsStatic,
            namedTypeSymbol.TypeParameters.Select(t => t.Name).ToImmutableArray(),
            new(namedTypeSymbol.ContainingNamespace.IsGlobalNamespace, namedTypeSymbol.ContainingNamespace.ToDisplayString()),
            namedTypeSymbol.GetContainingTypes().Select(c => new NamedTypeSymbolInfo(c)).ToImmutableArray(),
            hasParameterlessConstructor,
            filename,
            accessibility,
            initializerMethod)
    {
    }
}
