using System;
using System.Runtime.Serialization;

namespace KMA.APRZP2019.TextEditorProject.DBModels
{
    [Serializable]
    [DataContract(IsReference = true)]
    public class UserRequest : IDBModel
    {
        [DataMember]
        private Guid _guid;
        [DataMember]
        private string _filepath;
        [DataMember]
        private bool _isFileChanged;
        [DataMember]
        private DateTime _changedAt;


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

        public string Filepath
        {
            get
            {
                return _filepath;
            }
            private set
            {
                _filepath = value;
            }
        }

        public bool IsFileChanged
        {
            get
            {
                return _isFileChanged;
            }
            set
            {
                _isFileChanged = value;
            }
        }

        public DateTime ChangedAt
        {
            get
            {
                return _changedAt;
            }
            set
            {
                _changedAt = value;
            }
        }
        #endregion

        #region Constructors
        public UserRequest()
        {

        }

        public UserRequest(string filepath, bool isFileChanged, DateTime changedAt)
        {
            Guid = Guid.NewGuid();
            Filepath = filepath;
            IsFileChanged = isFileChanged;
            ChangedAt = changedAt;
        }
        #endregion
    }
}
