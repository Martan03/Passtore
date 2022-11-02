using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Models
{
    internal class AllPasswords
    {
        public ObservableCollection<Password> Passwords { get; set; } = new ObservableCollection<Password>();

        public AllPasswords() =>
            LoadPasswords();

        public void LoadPasswords()
        {
            Passwords.Clear();

            string path = Path.Join(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Passtore");
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            dynamic pswds = JsonConvert.DeserializeObject(File.ReadAllText(Path.Combine(path, "passwords.json")));

            foreach (var pswd in pswds)
                Passwords.Add(pswd);
        }
    }
}
