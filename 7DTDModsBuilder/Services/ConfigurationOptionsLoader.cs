using System.Text.Json;
using _7DTDModsBuilder.Extensions;
using _7DTDModsBuilder.ObjectsModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace _7DTDModsBuilder.Services;

public class ConfigurationOptionsLoader : IConfigurationOptionsLoader
{

    #region READONLY DI FIELDS

    private ILogger<ConfigurationOptionsLoader> _logger;
    private IConfiguration _configuration;

    #endregion READONLY DI FIELDS

    #region PROPS

    public IConfigurationOptions? Configuration { get; private set; }

    #endregion PROPS

    #region CTORS

    public ConfigurationOptionsLoader(ILogger<ConfigurationOptionsLoader> logger, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
        Load();
    }

    #endregion CTORS

    #region IConfigurationOptionsLoader IMPLEMENTATION

    public void Load()
    {
        CheckConfigurationFileExistence();
        LoadConfiguration();
        UpdateExcludedFilesPathsToAbsolute();
        UpdateExcludedDirectoriesPathsToAbsolute();
        LoadDefaultConfigs();
    }

    #endregion IConfigurationOptionsLoader IMPLEMENTATION

    #region PRIVATE METHODS

    private void CheckConfigurationFileExistence()
    {
        if (!File.Exists(_configuration.GetBuildFileFullName()))
            throw new FileNotFoundException("The build.json file was not found.");
    }

    private void LoadConfiguration()
    {
        var buildFileContent = File.ReadAllText(_configuration.GetBuildFileFullName());
        IConfigurationOptions? tempConfig = JsonSerializer.Deserialize<ConfigurationOptions>(buildFileContent);
        if (tempConfig is null)
            throw new IOException("The content of the build.json file is not right.");
        Configuration = tempConfig;
    }

    private void UpdateExcludedFilesPathsToAbsolute()
    {
        for (var i = 0; i < Configuration!.ExcludedFiles!.Count; i++)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), Configuration.ExcludedFiles[i]);
            Configuration.ExcludedFiles[i] = filePath;
        }
    }

    private void UpdateExcludedDirectoriesPathsToAbsolute()
    {
        for (var i = 0; i < Configuration!.ExcludedDirectories!.Count; i++)
        {
            var dirPath = Path.Combine(Directory.GetCurrentDirectory(), Configuration.ExcludedDirectories[i]);
            Configuration.ExcludedDirectories[i] = dirPath;
        }
    }

    private void LoadDefaultConfigs()
    {
        Configuration!.ExcludedFiles ??= new List<string>();
        Configuration!.ExcludedFiles.AddRange(_configuration.GetDefaultExcludedFullFiles());

        Configuration!.ExcludedDirectories ??= new List<string>();
        Configuration!.ExcludedDirectories.AddRange(_configuration.GetDefaultExcludedFullDirectories());
    }

    #endregion PRIVATE METHODS

}