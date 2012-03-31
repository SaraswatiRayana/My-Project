using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;


namespace MitraTech.Core
{
    public abstract class MapperBase<T> where T : ModelBase, new()
    {
        #region Class Variables
        private T _model = new T();
        protected List<QueryParameter> _queryParams = new List<QueryParameter>();
        public Errors Errors = new Errors();
        #endregion

        #region Class Property

        public T Model
        {
            get { return _model; }
            set { _model = value; }
        }

        #endregion

        #region Class Constructor

        public MapperBase()
        {
            Initialize();
        }

        private void Initialize()
        {
            _queryParams.Clear();
            Errors.Clear();
        }

        #endregion

        #region Transaction Method

        public virtual List<T> GetAll()
        {
            List<T> results = new List<T>();
            string[] columns = _model.ColumnCollections.Select(x => x.Value.ColumnName).ToArray<string>();

            string query = "SELECT " + string.Join(" ,", columns) + " FROM " + _model.TableName;

            IDataReader reader = GetData(new QueryParameter { commandText = query, parameters = new List<SqlParameter>() });

            while (reader.Read())
            {
                T model = new T();
                if (LoadFromReader(reader, ref model)) results.Add(model);
            }
            reader.Close();

            return results;
        }

        protected List<T> GetByCriteria(ColumnData[] criteriaColumns)
        {
            List<T> results = new List<T>();
            string[] columns = criteriaColumns.Select(x => x.ColumnName).ToArray<string>();
                        
            string query = "SELECT " + string.Join(",", columns) + "FROM " + _model.TableName + "WHERE " + ConstructWhereClause(criteriaColumns);
            IDataReader reader = GetData(new QueryParameter { commandText = query, parameters = GenerateParameterList(criteriaColumns) });

            while (reader.Read())
            {
                T model = new T();
                if (LoadFromReader(reader, ref model)) results.Add(model);
            }
            reader.Close();
            return results;
        }

        protected void ConstructCreateQuery(T insertRow)
        {
            if (_model != null)
            {
                string[] columnNames = _model.ColumnCollections.Select(x => x.Value.ColumnName).ToArray<string>();
                string[] paramList = _model.ColumnCollections.Select(x => "@" + x.Value.Name).ToArray<string>();
                
                string query = " INSERT INTO " + _model.TableName + "(" + string.Join(", ", columnNames) + ")" +
                               " VALUES" + "(" + string.Join(", ", paramList) + ")";

                _queryParams.Add(new QueryParameter { commandText = query, parameters = GenerateParameterList(insertRow.ColumnCollections.Values.ToArray())});
            }
        }

        protected void ConstructDeleteQuery(T deleteRow)
        {
            ColumnData[] keyColumns = deleteRow.ColumnCollections.Where(x => x.Value.IsPrimaryKey).Select(y => y.Value).ToArray<ColumnData>(); ;
                      
            string query = " DELETE " + _model.TableName + " WHERE " + ConstructWhereClause(keyColumns);
            _queryParams.Add(new QueryParameter { commandText = query, parameters = GenerateParameterList(keyColumns) });
        }

        protected void ConstructUpdateQuery(T updateRow)
        {
            string update = string.Empty;
            ColumnData[] updatedColumns = updateRow.ColumnCollections.Where(x => x.Value.IsUpdated).Select(y => y.Value).ToArray<ColumnData>(); ;
            ColumnData[] keyColumns = updateRow.ColumnCollections.Where(x => x.Value.IsPrimaryKey).Select(y => y.Value).ToArray<ColumnData>(); ;
            List<SqlParameter> paramCollection = new List<SqlParameter>();

            foreach (ColumnData col in updatedColumns)
            {
                update = string.Format("{0} = {1}, ", col.ColumnName, "'@" + col.Name + "'");                
            }

            paramCollection.AddRange(GenerateParameterList(updatedColumns));
            paramCollection.AddRange(GenerateParameterList(keyColumns));
            
            string query = " UPDATE " + _model.TableName +
                          " SET " + update.TrimEnd(',') +
                          " WHERE " + ConstructWhereClause(keyColumns);

            _queryParams.Add(new QueryParameter { commandText = query, parameters = paramCollection });
        }

        public virtual void Save()
        {
            if (_queryParams == null)
            {
                Errors.Add("No data saved.");
                return;
            }

            DBConnectionObject _dbConnection = new DBConnectionObject();
            try
            {
                _dbConnection.ExecuteNonQuery(_queryParams);
            }
            catch (SqlException ex)
            {
                Errors.Add(ex.Message.ToString());
            }
            finally
            {
                _queryParams.Clear();
            }
        }

        public virtual bool BulkSave(List<T> modelCollection)
        {
            ProcessRows(modelCollection);
            Save();
            return Errors.IsError();
        }

        #endregion

        #region Private Helper

        private void ProcessRows(List<T> models)
        {
            List<T> _insertRows = models.FindAll(x => x.RowAction == RowActions.New);
            foreach (T insertRow in _insertRows)
            {
                ConstructCreateQuery(insertRow);
            }

            List<T> _updateRows = models.FindAll(x => x.RowAction == RowActions.Update);
            foreach (T updateRow in _updateRows)
            {
                ConstructCreateQuery(updateRow);
            }

            List<T> _deleteRows = models.FindAll(x => x.RowAction == RowActions.Delete);
            foreach (T deleteRow in _deleteRows)
            {
                ConstructDeleteQuery(deleteRow);
            }
        }

        private IDataReader GetData(QueryParameter selectQueryParam)
        {
            DBConnectionObject dbConnection = new DBConnectionObject();
            SqlDataReader reader = null;
            try
            {
                reader = dbConnection.ExecuteReader(selectQueryParam);
            }
            catch (SqlException ex)
            {
                Errors.Add(ex.Message.ToString());
            }

            return (IDataReader)reader;
        }

        private string ConstructWhereClause(ColumnData[] whereColumn)
        {
            string condition = string.Empty;
            foreach (ColumnData key in whereColumn)
            {
                condition = string.Format("{0} = {1} AND ", key.ColumnName, "'@" + key.Name + "'");
            }
            condition = condition.Substring(0, condition.Length - 4);
            return condition;
        }

        private bool LoadFromReader(IDataReader reader, ref T model)
        {
            try
            {
                int indexColumn;

                foreach (ColumnData col in model.ColumnCollections.Values)
                {
                    indexColumn = reader.GetOrdinal(col.ColumnName);
                    switch (col.DataType)
                    {
                        case ColumnData.DataTypes.DateTime:
                            col.ColumnValue = reader.GetDateTime(indexColumn);
                            break;
                        case ColumnData.DataTypes.Boolean:
                            col.ColumnValue = reader.GetBoolean(indexColumn);
                            break;
                        case ColumnData.DataTypes.Decimal:
                            col.ColumnValue = reader.GetDecimal(indexColumn);
                            break;
                        case ColumnData.DataTypes.Integer:
                            col.ColumnValue = reader.GetInt32(indexColumn);
                            break;
                        case ColumnData.DataTypes.String:
                            col.ColumnValue = reader.GetString(indexColumn);
                            break;
                        default:
                            col.ColumnValue = reader.GetValue(indexColumn);
                            break;

                    }
                    //On Read always set IsUpdated property to false
                    col.IsUpdated = false;
                }
            }
            catch (Exception ex)
            {
                Errors.Add(ex.Message);
                return false;
            }
            return true;
        }

        private SqlDbType GetSqlDataType(ColumnData.DataTypes dataType)
        {
            switch (dataType)
            {
                case ColumnData.DataTypes.Boolean:
                    return SqlDbType.Bit;
                case ColumnData.DataTypes.DateTime:
                    return SqlDbType.DateTime;
                case ColumnData.DataTypes.Decimal:
                    return SqlDbType.Decimal;
                case ColumnData.DataTypes.Integer:
                    return SqlDbType.Int;
                case ColumnData.DataTypes.String:
                    return SqlDbType.NVarChar;
                default:
                    return SqlDbType.VarChar;
            }

        }

        private List<SqlParameter> GenerateParameterList(ColumnData[] columns)
        {
            List<SqlParameter> paramCollection = new List<SqlParameter>();
            foreach (ColumnData column in columns)
            {
                paramCollection.Add(new SqlParameter(column.Name, column.ColumnValue));
            }

            return paramCollection;
        }

        //private PropertyInfo test(ref Object obj, string propName)
        //{
        //    return obj.GetType().GetProperty(propName);
        //}

        #endregion
    }
}
