using System;
using System.Collections.Generic;


namespace MitraTech.Core
{
    public enum RowActions
    {
        Unchanged = 0,
        New = 1,
        Update = 2,
        Delete = 3,
        NotFound = 4,
    }

    public class ColumnData
    {        
        public enum DataTypes
        {
            Boolean = 1,
            Integer = 2,
            String = 3,
            DateTime = 4,
            Decimal = 5,            
        }

        private bool _isPrimaryKey;
        private bool _isUpdated;
        private bool _isMandatory;
        private string _columnName;
        private string _name;
        private object _value = null;
        private DataTypes _dataType;


        public bool IsMandatory
        {
            get { return _isMandatory; }
            set { _isMandatory = value; }
        }

        internal bool IsUpdated
        {
            get { return _isUpdated; }
            set { _isUpdated = value; }
        }

        internal bool IsPrimaryKey
        {
            get { return _isPrimaryKey; }
            set { _isPrimaryKey = value; }
        }

        internal string ColumnName
        {
            get { return _columnName; }
            set { _columnName = value; }
        }

        internal string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        internal object ColumnValue
        {
            get
            {
                if (_value == null) return null;
                switch (DataType)
                {
                    case DataTypes.Boolean:
                        return Convert.ToBoolean(_value);
                    case DataTypes.DateTime:
                        return Convert.ToDateTime(_value);
                    case DataTypes.Decimal:
                        return Convert.ToDecimal(_value);
                    case DataTypes.Integer:
                        return Convert.ToInt32(_value);
                    case DataTypes.String:
                        return Convert.ToString(_value);
                    default:
                        return _value;
                }

            }
            set 
            {
                //switch (_dataType)
                //{
                //    case DataTypes.Boolean:
                //        bool bVal = Convert.ToBoolean(value);
                //        break;
                //    case DataTypes.DateTime:
                //        DateTime dtVal = Convert.ToDateTime(value);
                //        break;
                //    case DataTypes.Decimal:
                //        decimal dVal = Convert.ToDecimal(value);
                //        break;
                //    case DataTypes.Integer:
                //        int iVal =  Convert.ToInt32(value);
                //        break;
                //    default:
                //        string sVal = Convert.ToString(value);
                //        break;
                //}
                if (_value != value)
                {
                    _value = value;
                    this.IsUpdated = true;
                }
            }

        }

        internal DataTypes DataType
        {
            get { return _dataType; }
            set { _dataType = value; }
        }

        internal string GenerateKey()
        {
            return this.Name;
        }

        public ColumnData()
        {
            ColumnValue = new object();
            DataType = new DataTypes();
        }

    }

    public class Errors
    {
        public List<string> Message = new List<string>();

        public bool IsError()
        {
            return Message.Count > 0;
        }

        public void Clear()
        {
            Message.Clear();
        }

        public void Append(Errors errors)
        {
            this.Message.AddRange(errors.Message);
        }

        public void Add(string errorMessage)
        {
            Message.Add(errorMessage);
        }

    }

}
