using Newtonsoft.Json;
using SolarPanel.Types;

namespace SolarPanel.Data
{
    public class InstallerProvider
    {
        #region Singleton Implementation

        private static readonly Lazy<InstallerProvider> lazy =
        new Lazy<InstallerProvider>(() => new InstallerProvider());

        public static InstallerProvider Instance { get { return lazy.Value; } }

        #endregion

        #region Constructors
        
        private InstallerProvider()
        {
            var dataFileLocation = AppDomain.CurrentDomain.BaseDirectory + "Installers.json";

            var dataFile = File.ReadAllText(dataFileLocation);

            var data = JsonConvert.DeserializeObject<List<Installer>>(dataFile);
            
            Installers = data ?? new List<Installer>();
        }

        #endregion

        public void SaveToFile(string path)
        {
            var file = JsonConvert.SerializeObject(Installers);
        }

        /// <summary>
        /// All the Installers available
        /// </summary>
        public List<Installer> Installers { get; }
    }
}