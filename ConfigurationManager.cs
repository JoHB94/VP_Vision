using System;
using System.IO;
using Newtonsoft.Json.Linq;

namespace VP_Vision
{
    internal class ConfigurationManager
    {
        /*
            시스템 설정값 관리
         */
        private JObject configuration;

        /// <summary>
        /// Load the configuration from the specified JSON file.
        /// </summary>
        /// <param name="configFilePath">The path to the configuration file.</param>
        public void LoadConfiguration(string configFilePath = "appsettings.json")
        {
            if (!File.Exists(configFilePath))
            {
                throw new FileNotFoundException($"설정파일을 찾지 못하였습니다.: {configFilePath}");
            }

            try
            {
                string jsonContent = File.ReadAllText(configFilePath);
                configuration = JObject.Parse(jsonContent);
                Console.WriteLine("설정 파일을 불러오는데 성공했습니다.");
            }
            catch (Exception ex)
            {
                throw new Exception("설정파일을 불러오는데 실패했습니다. 포맷을 확인해주세요.", ex);
            }
        }

        /// <summary>
        /// Get a string setting by its key from the loaded configuration.
        /// </summary>
        /// <param name="key">The key of the setting to retrieve.</param>
        /// <returns>The value of the setting.</returns>
        public string GetSetting(string key)
        {
            if (configuration == null)
            {
                throw new InvalidOperationException("설정파일이 로드되지 않았습니다. LoadConfiguration() 를 먼저 호출하세요.");
            }

            JToken token = configuration.SelectToken(key);
            if (token == null)
            {
                throw new KeyNotFoundException($"Setting not found for key: {key}");
            }

            return token.ToString();
        }

        /// <summary>
        /// Get an integer setting by its key from the loaded configuration.
        /// </summary>
        /// <param name="key">The key of the setting to retrieve.</param>
        /// <returns>The integer value of the setting.</returns>
        public int GetIntSetting(string key)
        {
            string value = GetSetting(key);
            if (int.TryParse(value, out int result))
            {
                return result;
            }

            throw new FormatException($"Invalid integer value for key: {key}");
        }
    }
}
