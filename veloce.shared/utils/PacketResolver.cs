using System.Reflection;
using veloce.shared.attributes;
using veloce.shared.packets;

namespace veloce.shared.utils;

/// <summary>
/// Represents an object containing the packet ordered hierarchy
/// </summary>
public sealed class HierarchyNode
{
    public Type Type { get; set; } = null!;
    public List<HierarchyNode> Children { get; } = [];
    public List<Type> ConcreteList { get; } = [];
}

/// <summary>
/// Represents an object containing the packet abstract hierarchy with its concrete implementations if many
/// </summary>
public class Node
{
    public required List<Type> AbstractList { get; init; }
    public required List<Type> ConcreteList { get; init; }
}

/// <summary>
/// A utility class for automatic compatibility between protobuf and packets.
/// </summary>
public static class PacketResolver
{
    /// <summary>
    /// A utility method to collect concrete packet implementation within the assembly.
    /// </summary>
    /// <remarks>Only packets decorated with the following attribute: <see cref="PacketIdentifierAttribute"/> will be recognized.</remarks>
    internal static List<Type> Resolve(Assembly assembly)
    {
        return assembly.GetTypes()
            .Where(t => t.GetCustomAttribute<PacketIdentifierAttribute>() != null)
            .ToList();
    }
    
    /// <summary>
    /// A utility method to build a hierarchy of packets based on their inheritance schema for protobuf serialization and deserialization.
    /// </summary>
    internal static HierarchyNode BuildHierarchy(List<Type> types)
    {
        var nodes = types
            .GroupBy(x => x.BaseType)
            .ToList()
            .Select(x => new Node {
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
    /// A utility method to retrieve all base types of the packet type, including custom abstract ones.
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