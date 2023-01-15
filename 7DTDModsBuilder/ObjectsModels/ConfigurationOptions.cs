namespace _7DTDModsBuilder.ObjectsModels;

public class ConfigurationOptions : IConfigurationOptions
{
    public string GameModsPath { get; set; }
    public string ModFolderName { get; set; }
    public List<string>? ExcludedDirectories { get; set; }
    public List<string>? ExcludedFiles { get; set; }
}