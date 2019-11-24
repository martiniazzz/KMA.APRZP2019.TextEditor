﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

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
    }
}
