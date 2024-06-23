using Speckle.Automate.Sdk.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

/// <summary>
/// This class describes the user specified variables that the function wants to work with.
/// </summary>
/// This class is used to generate a JSON Schema to ensure that the user provided values
/// are valid and match the required schema.
public struct FunctionInputs
{
    /// <summary>
    /// Layers to validate the model against
    /// </summary>
    [Required]
    public List<String> ValidLayers;
}
