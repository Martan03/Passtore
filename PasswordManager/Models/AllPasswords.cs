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
        public ObservableCollection<Password> Passwords { get; set; } = new ObservableCollection<Password>();

        public AllPasswords(string password) =>
            LoadPasswords(password);

        public void LoadPasswords(string password)
        {
            Passwords.Clear();

            string path = Path.Join(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Passtore");
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            if (!File.Exists(Path.Combine(path, "passwords.json")))
                return;
            dynamic pswds = JsonConvert.DeserializeObject(File.ReadAllText(Path.Combine(path, "passwords.json")));

            foreach (var pswd in pswds)
                Passwords.Add(pswd);
        }
    }
}
