using KMA.APRZP2019.TextEditorProject.Tools;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

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

        #region Properties
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
        #endregion

        #region Constructors
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
        #endregion

        #region Password encryption
        /// <summary>
        /// Encypts password and sets the result as user password property 
        /// </summary>
        /// <param name="password">Passoword to encrypt</param>
        private void SetPassword(string password)
        {
            _password = Encryptor.GetMd5HashForString(password);
        }

        /// <summary>
        /// Encrypts the specified password ans then checks if it matches user password property
        /// </summary>
        /// <param name="password">Plain text password to check</param>
        /// <returns></returns>
        public bool CheckPassword(string password)
        {
            try
            {
                string res2 = Encryptor.GetMd5HashForString(password);
                return _password == res2;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion
    }
}
