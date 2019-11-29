using System;

namespace KMA.APRZP2019.TextEditorProject.DBModels
{
    public interface IDBModel
    {
        /// <summary>
        /// Id of model
        /// </summary>
        Guid Guid { get; }
    }
}
