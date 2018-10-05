using System.ComponentModel;
using System.Runtime.CompilerServices;
using Atarashii.Executable;

namespace Atarashii.GUI.Loader
{
    /// <summary>
    /// HCE Atarashii GUI main entity
    /// </summary>
    public class Main : INotifyPropertyChanged
    {
        private readonly Executable.Loader _loader = new Executable.Loader();
        private readonly Verifier _verifier = new Verifier();

        private string _hcePath;
        private string _logs;
        private bool _canLoad;

        /// <summary>
        /// HCE executable path.
        /// </summary>
        public string HcePath
        {
            get => _hcePath;
            set
            {
                if (value == _hcePath) return;
                _hcePath = value;
                OnPropertyChanged();

                if (string.IsNullOrWhiteSpace(value))
                {
                    AppendToLog("Cleared selection.");
                }
                else
                {
                    AppendToLog($"Selected {value}.");
                    CheckIfCanLoad();
                }
            }
        }

        /// <summary>
        /// Log messages to output to the GUI.
        /// </summary>
        public string Logs
        {
            get => _logs;
            set
            {
                if (value == _logs) return;
                _logs = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Specified HCE executable can be loaded.
        /// </summary>
        public bool CanLoad
        {
            get => _canLoad;
            set
            {
                if (value == _canLoad) return;
                _canLoad = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Attempt to load the HCE executable.
        /// </summary>
        public void Load()
        {
            try
            {
                _loader.Execute(HcePath, _verifier);
                AppendToLog($"Successfully loaded {HcePath}");
            }
            catch (LoaderException e)
            {
                AppendToLog(e.Message);
            }
        }

        /// <summary>
        /// Checks if the HCE executable can be loaded.
        /// </summary>
        private void CheckIfCanLoad()
        {
            CanLoad = _verifier.Verify(HcePath);
            AppendToLog(CanLoad
                ? "Executable ready to load."
                : "Executable is not valid.");
        }

        /// <summary>
        /// Adds a given message to the log property.
        /// </summary>
        /// <param name="message">
        /// Message to append to the log.
        /// </param>
        private void AppendToLog(string message)
        {
            Logs = $"{message}\n\n{Logs}";
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}