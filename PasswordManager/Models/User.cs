using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace PasswordManager.Models
{
    public class User
    {
        private string _dataPath { get; } = Path.Join(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Passtore");
        private string _filePath { get; } = Path.Combine(Path.Join(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Passtore"), "user-info.json");
        [JsonProperty]
        private string _password { get; set; }

        public User()
        {
            Directory.CreateDirectory(_dataPath);
        }

        public User LoadFromJson()
        {
            if (!File.Exists(_filePath) || !GetStorageReadRight())
                return null;

            return JsonConvert.DeserializeObject<User>(File.ReadAllText(_filePath)) ?? null;
        }

        public void SaveToJson()
        {
            if (!GetStorageWriteRight())
                return;

            File.WriteAllText(_filePath, JsonConvert.SerializeObject(this));
        }

        public void CreateUser(string password)
        {
            _password = HashPassword(password);
            SaveToJson();
        }

        public bool CheckPassword(string password)
        {
            return string.Equals(_password, HashPassword(password));
        }

        private string HashPassword(string password)
        {
            byte[] salt = Encoding.ASCII.GetBytes("Cda6ZgNVluChtzseyq9uMQ==");
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8
            ));
        }

        private static bool GetStorageReadRight()
        {
            var rt = Permissions.RequestAsync<Permissions.StorageRead>();
            rt.Wait();
            return rt.Result != PermissionStatus.Denied;
        }

        private static bool GetStorageWriteRight()
        {
            var rt = Permissions.RequestAsync<Permissions.StorageWrite>();
            rt.Wait();
            return rt.Result != PermissionStatus.Denied;
        }
    }
}
