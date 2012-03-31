using System;
using System.Collections.Generic;
using System.Linq;



namespace MitraTech.Core
{
    public abstract class ModelBase
    {

        private Dictionary<string, ColumnData> _columnCollections = new Dictionary<string,ColumnData>();

        internal Dictionary<string, ColumnData> ColumnCollections
        {
            get { return _columnCollections; }
        }
        private string _tableName;
        private RowActions _rowAction;

        internal string TableName
        {
            get { return _tableName; }
            set { _tableName = value; }
        }

        public RowActions RowAction
        {
            get { return _rowAction; }
            set { _rowAction = value; }
        }

        public ModelBase(string tableName)
        {
            _tableName = tableName;
            _rowAction = RowActions.New;
            SetColumnCollection();
        }

        protected void AddColumnCollection(string name, string columnName, ColumnData.DataTypes dataType, bool isPrimaryKey, bool isMandatory)
        {
            ColumnData newColumn = new ColumnData { Name = name, ColumnName = columnName, DataType = dataType, IsPrimaryKey = isPrimaryKey, IsMandatory = isMandatory, IsUpdated = false, ColumnValue = null};
            _columnCollections.Add(newColumn.GenerateKey(), newColumn);
        }

        protected abstract void SetColumnCollection();
    }

 
}
