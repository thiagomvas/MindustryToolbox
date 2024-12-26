namespace MindustryToolbox.Core.ValueTypes;
/// <summary>
/// Represents different types of structures.
/// </summary>
public enum StructureType
{
    /// <summary>
    /// Represents a structure type that does not fall into any specific category.
    /// </summary>
    Other,

    /// <summary>
    /// Represents a drilling structure or one that produces without taking any inputs.
    /// </summary>
    Drill,

    /// <summary>
    /// Represents a factory structure or one that produces whilst requiring inputs to produce a resource.
    /// </summary>
    Factory
}
