using System.Reflection;
using FaucetSharp.Shared.attributes;
using FaucetSharp.Shared.packets;

namespace FaucetSharp.Shared.utils;

/// <summary>
///     Represents an object containing the ordered packet hierarchy.
/// </summary>
internal sealed class HierarchyNode
{
    public Type Type { get; set; } = null!;
    public List<HierarchyNode> Children { get; } = [];
    public List<Type> ConcreteList { get; } = [];
}

/// <summary>
///     Represents an object containing the packet abstract hierarchy with its concrete implementations if many.
/// </summary>
internal sealed class PacketNode
{
    public required List<Type> AbstractList { get; init; }
    public required List<Type> ConcreteList { get; init; }
}

/// <summary>
///     A utility class for resolving and building packet hierarchies for protobuf serialization.
/// </summary>
internal static class PacketResolver
{
    /// <summary>
    ///     A utility method to collect concrete packet implementation within the assembly.
    /// </summary>
    /// <remarks>
    ///     Only packets decorated with the following attribute: <see cref="PacketIdentifierAttribute" /> will be
    ///     recognized.
    /// </remarks>
    internal static List<Type> Resolve(Assembly assembly)
    {
        return assembly.GetTypes()
            .Where(t => t.GetCustomAttribute<PacketIdentifierAttribute>() != null)
            .ToList();
    }

    /// <summary>
    ///     A utility method to build a class dependency tree of packets based on their inheritance schema.
    /// </summary>
    internal static HierarchyNode BuildHierarchy(List<Type> types)
    {
        var nodes = types
            .GroupBy(x => x.BaseType)
            .ToList()
            .Select(x => new PacketNode
            {
                AbstractList = GetBaseTypes(x.First()).Reverse().ToList(),
                ConcreteList = x.ToList()
            })
            .ToList();

        HierarchyNode? rootNode = null;
        var lookup = new Dictionary<Type, HierarchyNode>();

        foreach (var node in nodes)
        {
            HierarchyNode? parentNode = null;

            foreach (var abstractType in node.AbstractList)
            {
                if (!lookup.TryGetValue(abstractType, out var currentNode))
                {
                    currentNode = new HierarchyNode { Type = abstractType };
                    lookup[abstractType] = currentNode;

                    if (parentNode == null) rootNode = currentNode;
                    else parentNode.Children.Add(currentNode);
                }

                parentNode = currentNode;
            }

            parentNode?.ConcreteList.AddRange(node.ConcreteList);
        }

        return rootNode!;
    }

    /// <summary>
    ///     A utility method to retrieve all base types of a given packet, including custom ones.
    /// </summary>
    private static IEnumerable<Type> GetBaseTypes(Type type)
    {
        while (type != typeof(AbstractPacket))
        {
            type = type.BaseType!;
            yield return type;
        }
    }
}