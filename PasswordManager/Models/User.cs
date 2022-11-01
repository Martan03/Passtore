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
    internal class User
    {
        private string _filePath { get; set; } = Path.Join(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Passtore/user-info.json");
        [JsonProperty]
        private string _password { get; set; }

        public User LoadFromJson()
        {
            if (!File.Exists(_filePath))
                return null;
            return JsonConvert.DeserializeObject<User>(_filePath) ?? null;
        }

        public void SaveToJson()
        {
            FileInfo fi = new(_filePath);
            if (!fi.Directory!.Exists)
                Directory.CreateDirectory(_filePath);
            File.WriteAllText(_filePath, JsonConvert.SerializeObject(this));
        }

        public bool CheckPassword(string password)
        {
            return string.Equals(_password, HashPassword(password));
        }

        private string HashPassword(string password)
        {
            byte[] salt = RandomNumberGenerator.GetBytes(128 / 8);
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8
            ));
        }
    }
}
