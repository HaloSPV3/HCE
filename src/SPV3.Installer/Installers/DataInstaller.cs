using System.IO;
using System.IO.Compression;
using System.Linq;
using SPV3.Domain;
using SPV3.Installer.Domain;
using Directory = SPV3.Domain.Directory;

namespace SPV3.Installer.Installers
{
    /// <inheritdoc />
    public class DataInstaller : Common.Installer
    {
        /// <summary>
        ///     Name for the initial data package. (Meta) + (Core) = 2.
        /// </summary>
        private const int InitialDataPackage = 2;

        /// <inheritdoc />
        public DataInstaller(Directory target, Directory backup, IStatus status = null) : base(target, backup, status)
        {
            //
        }

        /// <inheritdoc />
        public override void Install(Manifest manifest)
        {
            Notify("----------------------------");
            Notify("Invoked core installation...");
            Notify("----------------------------");

            foreach (var package in manifest.Packages.Where(package => package.Name != "0x01.bin"))
            {
                Notify("Migrating existing for pack:" + $"{package.Name} => {package.Directory}");
                Migrate(package);

                Notify("Installing data from package:" + $"{package.Name} => {package.Directory}");
                var target = Path.Combine(Target, package.Directory);
                ZipFile.ExtractToDirectory(package.Name, target);
            }

            Notify("----------------------------");
            Notify("Resolve data installation...");
            Notify("----------------------------");
        }
    }
}