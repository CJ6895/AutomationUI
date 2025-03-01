using Microsoft.Extensions.Configuration;

namespace Tests.UI;

    public class ProjectSetup
    {
        private static readonly Lazy<ProjectSetup> _instance = new(() => new ProjectSetup());

        public static ProjectSetup Instance => _instance.Value;

        public string UserName { get; private set; }
        public string Password { get; private set; }
        public string LaunchUrl { get; private set; }
        public string InvalidUserName { get; private set; }
        public string InvalidPassword { get; private set; }

        private ProjectSetup()
        {
            string configPath = FindTestSettingsFile();
            LoadConfiguration(configPath);
        }

        private string FindTestSettingsFile()
        {
            // Start from the test execution directory (bin/Debug/netX.0)
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string fileName = "testsettings.json";

            // Traverse up to find the project root (max 5 levels to prevent infinite loops)
            for (int i = 0; i < 5; i++)
            {
                string potentialPath = Path.Combine(baseDirectory, fileName);
                if (File.Exists(potentialPath))
                {
                    Console.WriteLine($"Configuration file found: {potentialPath}");
                    return potentialPath;
                }

                baseDirectory = Directory.GetParent(baseDirectory)?.FullName;
                if (baseDirectory == null) break;
            }

            throw new FileNotFoundException($"Configuration file '{fileName}' not found.");
        }

        private void LoadConfiguration(string configPath)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile(configPath, optional: false, reloadOnChange: true)
                .Build();

            UserName = configuration["UserName"] ?? throw new Exception("Missing 'UserName' in testsettings.json");
            Password = configuration["Password"] ?? throw new Exception("Missing 'Password' in testsettings.json");
            LaunchUrl = configuration["LaunchUrl"] ?? throw new Exception("Missing 'LaunchUrl' in testsettings.json");
            InvalidUserName = configuration["InvalidUserName"] ?? throw new Exception("Missing 'LaunchUrl' in testsettings.json");
            InvalidPassword = configuration["InvalidPassword"] ?? throw new Exception("Missing 'LaunchUrl' in testsettings.json");
        }
    }
