using Microsoft.Extensions.Configuration;

namespace _7DTDModsBuilder.Extensions;

public static class ConfigurationExtensions
{

    #region CONSTANTS

    private const string BuildFileNameKey = "BuildFileName";
    private const string DefaultExcludedDirectoriesKey = "DefaultExcludedDirectories";
    private const string DefaultExcludedFilesKey = "DefaultExcludedFiles";

    #endregion CONSTANTS

    public static string GetBuildFileName(this IConfiguration configuration) =>
        configuration.GetValue<string>(BuildFileNameKey);

    public static string GetBuildFileFullName(this IConfiguration configuration) =>
        Path.Combine(Directory.GetCurrentDirectory(), configuration.GetBuildFileName());

    public static IConfigurationSection GetDefaultExcludedDirectoriesSection(this IConfiguration configuration) =>
        configuration.GetSection(DefaultExcludedDirectoriesKey);

    public static string[] GetDefaultExcludedDirectories(this IConfiguration configuration) =>
        configuration.GetDefaultExcludedDirectoriesSection().Get<string[]>() ?? Array.Empty<string>();

    public static string[] GetDefaultExcludedFullDirectories(this IConfiguration configuration)
    {
        var fullDirs = new string[configuration.GetDefaultExcludedDirectories().Length];
        for (var i = 0; i < configuration.GetDefaultExcludedDirectories().Length; i++)
        {
            fullDirs[i] = Path.Combine(Directory.GetCurrentDirectory(),
                configuration.GetDefaultExcludedDirectories()[i]);
        }
        return fullDirs;
    }

    public static IConfigurationSection GetDefaultExcludedFilesSection(this IConfiguration configuration) =>
        configuration.GetSection(DefaultExcludedFilesKey);

    public static string[] GetDefaultExcludedFiles(this IConfiguration configuration) =>
        configuration.GetDefaultExcludedFilesSection().Get<string[]>() ?? Array.Empty<string>();

    public static string[] GetDefaultExcludedFullFiles(this IConfiguration configuration)
    {
        var fullFiles = new string[configuration.GetDefaultExcludedFiles().Length];
        for (var i = 0; i < configuration.GetDefaultExcludedFiles().Length; i++)
        {
            fullFiles[i] = Path.Combine(Directory.GetCurrentDirectory(),
                configuration.GetDefaultExcludedFiles()[i]);
        }
        return fullFiles;
    }

}