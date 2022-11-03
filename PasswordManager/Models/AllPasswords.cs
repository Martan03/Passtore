using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Models
{
    public class AllPasswords
    {
        private string password { get; set; }

        [JsonProperty]
        public ObservableCollection<Password> Passwords { get; set; } = new ObservableCollection<Password>();

        public AllPasswords(string password) =>
            this.password = password;

        public void LoadPasswords()
        {
            Passwords.Clear();

            string path = Path.Join(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Passtore");
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            if (!File.Exists(Path.Combine(path, "passwords.json")))
                return;

            string decrypted = StringCipher.Decrypt(File.ReadAllText(Path.Combine(path, "passwords.json")), password);
            var pswds = JsonConvert.DeserializeObject<AllPasswords>(decrypted);

            foreach (var pswd in pswds.Passwords)
                Passwords.Add(pswd);
        }

        public void AddPassword(Password pswd)
        {
            Passwords.Add(pswd);

            string path = Path.Join(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Passtore");
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            string encrypted = StringCipher.Encrypt(JsonConvert.SerializeObject(this), password);

            File.WriteAllText(Path.Combine(path, "passwords.json"), encrypted);
        }

        public Password GetPassword(string name)
        {
            return Passwords.FirstOrDefault(p => p.Name == name);
        }
    }
}
