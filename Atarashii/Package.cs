using System.IO;
using System.IO.Compression;
using Atarashii.Exceptions;

namespace Atarashii
{
    /// <summary>
    ///     Archive installer and verifier.
    /// </summary>
    public class Package
    {
        /// <summary>
        ///     Directory containing the expected packages.
        /// </summary>
        public const string Directory = "Packages";

        /// <summary>
        ///     Package archive extension.
        /// </summary>
        public const string Extension = "pkg";

        public Package(string archiveName, string description, string destination)
        {
            ArchiveName = archiveName + $".{Extension}";
            Description = description;
            Destination = destination;
        }

        /// <summary>
        ///     Name of the archive file without any extensions or paths.
        /// </summary>
        public string ArchiveName { get; }

        /// <summary>
        ///     Informative line about the package.
        /// </summary>
        public string Description { get; }

        /// <summary>
        ///     Destination directory path for the installed contents.
        /// </summary>
        public string Destination { get; }

        /// <summary>
        ///     Applies the files in the package to the destination on the filesystem.
        /// </summary>
        /// <param name="logger">
        ///     Logging instance for appending package installation progress.
        /// </param>
        /// <exception cref="PackageException">
        ///     Package archive does not exist.
        ///     - or -
        ///     Destination directory does not exist.
        /// </exception>
        public void Install(ILogger logger)
        {
            if (!File.Exists(ArchiveName))
                throw new PackageException("Cannot install specified package. Package archive does not exist.");

            if (!System.IO.Directory.Exists(Destination))
                throw new PackageException("Cannot install specified package. Destination does not exist.");

            try
            {
                ZipFile.ExtractToDirectory(ArchiveName, Destination);
            }
            catch (IOException)
            {
                logger.Log($"{Description} data already exists. This is fine!");
            }

            logger.Log($"{Description} data has been installed successfully to the filesystem.");
        }
    }
}