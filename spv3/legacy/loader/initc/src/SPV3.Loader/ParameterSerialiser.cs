using System.Collections.Generic;
using System.Text;

namespace SPV3.Loader
{
    public class ParameterSerialiser
    {
        /// <summary>
        ///     Serialises the instance to a string that complies with the HCE startup arguments.
        /// </summary>
        /// <returns>
        ///     HCE-compliant startup string representation of this instance.
        /// </returns>
        public string Serialise(Parameters parameters)
        {
            var builder = new StringBuilder();

            // append the string values for toggles if they're enabled
            foreach (var toggle in new Dictionary<string, bool>
            {
                // disable overrides
                {"-nosound", parameters.DisableSound},
                {"-novideo", parameters.DisableVideo},
                {"-nojoystick", parameters.DisableJoystick},
                {"-nogamma", parameters.DisableGamma},

                // enable overrides
                {"-safemode", parameters.EnableSafeMode},
                {"-window", parameters.EnableWindowMode},
                {"-screenshot", parameters.EnableScreenshot},
                {"-console", parameters.EnableConsole},
                {"-devmode", parameters.EnableDeveloperMode}
            })
                if (toggle.Value)
                    builder.Append($"{toggle.Key} ");

            // shader overrides
            switch (parameters.CardType)
            {
                case CardType.FixedFunction:
                    builder.Append("-useff ");
                    break;
                case CardType.Shaders11Card:
                    builder.Append("-use11 ");
                    break;
                case CardType.Shaders14Card:
                    builder.Append("-use14 ");
                    break;
                case CardType.Shaders20Card:
                    builder.Append("-use20 ");
                    break;
                case CardType.Default:
                    builder.Append(string.Empty);
                    break;
                default:
                    builder.Append(string.Empty);
                    break;
            }

            // -vidmode
            if (parameters.VideoWidth != null && parameters.VideoHeight != null && parameters.VideoRefreshRate != null)
                builder.Append(
                    $"-vidmode {parameters.VideoWidth},{parameters.VideoHeight},{parameters.VideoRefreshRate} "
                );

            // -adapter
            if (parameters.VideoAdapterIndex != null)
                builder.Append($"-adapter {parameters.VideoAdapterIndex} ");

            // -port
            if (parameters.ServerPort != null)
                builder.Append($"-port {parameters.ServerPort} ");

            // -cport
            if (parameters.ClientPort != null)
                builder.Append($"-cport {parameters.ClientPort} ");

            // -ip
            if (!string.IsNullOrWhiteSpace(parameters.IpAddress))
                builder.Append($"-ip {parameters.IpAddress} ");

            return builder.ToString().TrimEnd();
        }
    }
}