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

        // List with all passwords
        [JsonProperty]
        public ObservableCollection<Password> Passwords { get; set; } = new ObservableCollection<Password>();

        public AllPasswords(string password) =>
            this.password = password;

        /// <summary>
        /// Loads all passwords from encrypted file
        /// </summary>
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

        /// <summary>
        /// Adds passwords to the list and saves to file
        /// </summary>
        /// <param name="pswd">Password to be add</param>
        public void AddPassword(Password pswd)
        {
            if (Passwords.Count == 0)
                pswd.Id = 0;
            else
                pswd.Id = Passwords.Last().Id + 1;
            Passwords.Add(pswd);

            SavePasswords();
        }

        /// <summary>
        /// Edits password, if passwords with ID is not found, adds it
        /// </summary>
        /// <param name="pswd">Password to be edited</param>
        public void EditPassword(Password pswd)
        {
            for (int i = 0; i < Passwords.Count; ++i)
            {
                if (Passwords[i].Id == pswd.Id)
                {
                    Passwords[i] = pswd;
                    SavePasswords();
                    return;
                }
            }

            AddPassword(pswd);
        }

        /// <summary>
        /// Removes passwords from saved passwords
        /// </summary>
        /// <param name="pswd">Passwords to be removed</param>
        public void RemovePassword(Password pswd)
        {
            for (int i = 0; i < Passwords.Count; ++i)
            {
                if (Passwords[i].Id == pswd.Id)
                {
                    Passwords.RemoveAt(i);
                    SavePasswords();
                    return;
                }
            }
        }

        /// <summary>
        /// Saves passwords to file and encrypts it
        /// </summary>
        private void SavePasswords()
        {
            string path = Path.Join(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Passtore");
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            string encrypted = StringCipher.Encrypt(JsonConvert.SerializeObject(this), password);

            File.WriteAllText(Path.Combine(path, "passwords.json"), encrypted);
        }

        // Gets password with given ID
        public Password GetPassword(int Id)
        {
            return Passwords.FirstOrDefault(p => p.Id == Id);
        }
    }
}
