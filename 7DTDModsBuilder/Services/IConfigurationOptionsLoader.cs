using _7DTDModsBuilder.ObjectsModels;

namespace _7DTDModsBuilder.Services;

public interface IConfigurationOptionsLoader
{
    public IConfigurationOptions? Configuration { get; }

    public void Load();
}