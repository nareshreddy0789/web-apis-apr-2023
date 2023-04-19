namespace HrApi;

public class FeaturesOptions
{
    public const string FeatureName = "features";
    public bool demo { get; set; }

    public string trueMessage { get; set; } = string.Empty;
    public string falseMessage { get; set; } = string.Empty;
}
