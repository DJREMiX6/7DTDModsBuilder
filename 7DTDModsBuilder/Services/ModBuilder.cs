using _7DTDModsBuilder.ObjectsModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace _7DTDModsBuilder.Services;

public class ModBuilder : IModBuilder
{

    #region READONLY DI FIELDS

    private readonly ILogger<ModBuilder> _logger;
    private readonly IConfiguration _configuration;
    private readonly IConfigurationOptionsLoader _configurationOptionsLoader;

    #endregion READONLY DI FIELDS

    #region CTORS

    public ModBuilder(ILogger<ModBuilder> logger, IConfiguration configuration, IConfigurationOptionsLoader configurationOptionsLoader)
    {
        _logger = logger;
        _configuration = configuration;
        _configurationOptionsLoader = configurationOptionsLoader;
    }

    #endregion CTORS

    #region IModBuilder IMPLEMENTATION

    public void Build()
    {
        var rootPath = Directory.GetCurrentDirectory();

        var destinationPath = ElaborateDestinationPath(_configurationOptionsLoader.Configuration);

        RemoveOldFiles(destinationPath);
        CreateRootDirectory(destinationPath);
        BuildRecursively(_configurationOptionsLoader.Configuration, rootPath, destinationPath);
    }

    #endregion IModBuilder IMPLEMENTATION

    #region PRIVATE METHODS

    private string ElaborateDestinationPath(IConfigurationOptions configurationOptions) => Path.Combine(configurationOptions.GameModsPath, configurationOptions.ModFolderName);

    private void RemoveOldFiles(string folderToRemove)
    {
        if (Directory.Exists(folderToRemove))
            Directory.Delete(folderToRemove, true);
    }

    private void CreateRootDirectory(string directoryToCreate) => Directory.CreateDirectory(directoryToCreate);

    private void BuildRecursively(IConfigurationOptions configurationOptions, string currentDirectoryPath, string destinationDirectoryPath)
    {
        BuildFiles(configurationOptions, currentDirectoryPath, destinationDirectoryPath);
        BuildDirectories(configurationOptions, currentDirectoryPath, destinationDirectoryPath);
    }

    private void BuildFiles(IConfigurationOptions configurationOptions, string currentDirectoryPath, string destinationDirectoryPath)
    {
        var files = Directory.GetFiles(currentDirectoryPath);

        foreach (var file in files)
        {
            if (configurationOptions.ExcludedFiles == null || !configurationOptions.ExcludedFiles.Contains(file))
            {
                var destinationFileName = Path.Combine(destinationDirectoryPath, Path.GetFileName(file));
                File.Copy(file, destinationFileName);
            }
        }
    }

    private void BuildDirectories(IConfigurationOptions configurationOptions, string currentDirectoryPath, string destinationDirectoryPath)
    {
        var directories = Directory.GetDirectories(currentDirectoryPath);

        foreach (var directory in directories)
        {
            var directoryToCreatePath = Path.Combine(destinationDirectoryPath, Path.GetFileName(directory));

            if (configurationOptions.ExcludedDirectories == null || !configurationOptions.ExcludedDirectories.Contains(directory))
            {
                Directory.CreateDirectory(directoryToCreatePath);
                BuildRecursively(configurationOptions, directory, directoryToCreatePath);
            }
        }
    }

    #endregion PRIVATE METHODS

}