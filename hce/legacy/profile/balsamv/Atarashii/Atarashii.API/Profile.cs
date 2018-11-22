using System;
using System.IO;
using Atarashii.Modules.Profile;

namespace Atarashii.API
{
    /// <summary>
    ///     Static API for the Atarashii Profile Module.
    /// </summary>
    public static class Profile
    {
        /// <summary>
        ///     Retrieves a Configuration-type representation of the provided blam.sav binary.
        /// </summary>
        /// <param name="blamPath">
        ///     Absolute path to a HCE profile blam.sav binary.
        /// </param>
        /// <returns>
        ///     Deserialised Configuration object representing the provided blam.sav binary.
        /// </returns>
        public static Configuration Parse(string blamPath)
        {
            using (var fs = File.Open(blamPath, FileMode.Open))
            {
                return ConfigurationFactory.GetFromStream(fs);
            }
        }

        /// <summary>
        ///     Serialises an inbound Configuration-type to the provided blam.sav binary path. 
        /// </summary>
        /// <param name="configuration">
        ///     Absolute path to a HCE profile blam.sav binary.
        /// </param>
        /// <param name="blamPath">
        ///     Deserialised Configuration object representing a blam.sav binary.
        /// </param>
        public static void Patch(Configuration configuration, string blamPath)
        {
            using (var ms = new MemoryStream())
            using (var fs = File.Open(blamPath, FileMode.Open))
            {
                fs.CopyTo(ms);
                new ConfigurationPatcher(configuration).PatchTo(ms);
                new ConfigurationForger().Forge(ms);

                ms.Position = 0;
                fs.Position = 0;
                
                ms.CopyTo(fs);
            }
        }

        /// <summary>
        ///     Attempts to detect the currently used HCE profile on the filesystem.
        /// </summary>
        /// <returns>
        ///     Currently used HCE profile, assuming the environment is valid.
        /// </returns>
        public static string Detect()
        {
            return LastprofFactory.Get(LastprofFactory.Type.Detect).Parse();
        }
    }
}