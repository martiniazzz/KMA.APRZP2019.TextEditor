using KMA.APRZP2019.TextEditorProject.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace KMA.APRZP2019.TextEditorProject.DBModels
{
    [Serializable()]
    [DataContract(IsReference = true)]
    public class User : IDBModel
    {
        [DataMember]
        private Guid _guid;
        [DataMember]
        private string _login;
        [DataMember]
        private string _firstName;
        [DataMember]
        private string _lastName;
        [DataMember]
        private string _email;
        [DataMember]
        private string _password;
        [NonSerialized()]
        [DataMember]
        private List<UserRequest> _requests;

        public Guid Guid
        {
            get
            {
                return _guid;
            }
            private set
            {
                _guid = value;
            }
        }

        public string Login
        {
            get
            {
                return _login;
            }
            private set
            {
                _login = value;
            }
        }

        public string FirstName
        {
            get
            {
                return _firstName;
            }
            private set
            {
                _firstName = value;
            }
        }
        public string LastName
        {
            get
            {
                return _lastName;
            }
            private set
            {
                _lastName = value;
            }
        }
        public string Email
        {
            get
            {
                return _email;
            }
            private set
            {
                _email = value;
            }
        }

        public string Password
        {
            get
            {
                return _password;
            }
            private set { _password = value; }
        }

        public List<UserRequest> Requests
        {
            get
            {
                return _requests;
            }
            private set
            {
                _requests = value;
            }
        }

        public User()
        {
            Requests = new List<UserRequest>();
        }
        public User(string login, string firstName, string lastName, string email, string password) : this()
        {
            _guid = Guid.NewGuid();
            _login = login;
            _firstName = firstName;
            _lastName = lastName;
            _email = email;
            SetPassword(password);
        }
        private void SetPassword(string password)
        {
            _password = Encryptor.GetMd5HashForString(password);
        }
        public bool CheckPassword(string password)
        {
            try
            {
                string res2 = Encryptor.GetMd5HashForString(password);
                return _password == res2;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
