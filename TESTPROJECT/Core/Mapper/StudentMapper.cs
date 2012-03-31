using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using MitraTech.Core.Model;

namespace MitraTech.Core.Mapper
{
    public class StudentMapper : MapperBase<Student>
    {        

        #region Class Constructor

            public StudentMapper()
            {
                Initialize();
            }            

            private void Initialize()
            {                
                Model = new Student();
            }

        #endregion

        #region Public Method

            public Student GetByID(object PK)
            {
                Student result = new Student { StudentID = PK.ToString(), StudentName = string.Empty, RowAction = RowActions.NotFound };
                ColumnData[] keyColumn = { result.ColumnCollections["StudentID"]};
                if (base.GetByCriteria(keyColumn).Count > 0)
                {
                    result = (Student)base.GetByCriteria(keyColumn)[0];
                    result.RowAction = RowActions.Unchanged;
                }
                
                return result;
            }

            public override List<Student> GetAll()
            {
                List<Student> results = new List<Student>();
                results = base.GetAll();
                return results;
            }

            public void Create(Student createRow)
            {
                base.ConstructCreateQuery(createRow);
            }

            public void Update(Student updateRow)
            {
                base.ConstructUpdateQuery(updateRow);
            }

            public void Delete(Student deleteRow)
            {
                base.ConstructDeleteQuery(deleteRow);
            }

            public override void Save()
            {
                base.Save();
            }

            public override bool BulkSave(List<Student> modelCollection)
            {
                return base.BulkSave(modelCollection);
            }

        #endregion

         
    }
}
