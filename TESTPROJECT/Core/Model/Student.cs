using System;
using System.Collections.Generic;
using System.Linq;

namespace MitraTech.Core.Model
{
    public class Student:ModelBase
    {
        private enum Columns
        {
            StudentID,
            StudentName,
            BirthDate,
            BirthPlace,
            SexType,
            School,
            Class
        }

        public enum Sex
        {
            Male = 0,
            Female =1
        }

        public Student()
            : base("STUDENT"){ }

        #region "Public Properties"
        public string StudentID
        {
            get { return Convert.ToString(ColumnCollections[Columns.StudentID.ToString()].ColumnValue);}
            set { ColumnCollections[Columns.StudentID.ToString()].ColumnValue = value; }
        }

        public string StudentName
        {
            get { return Convert.ToString(ColumnCollections[Columns.StudentName.ToString()].ColumnValue); }
            set { ColumnCollections[Columns.StudentName.ToString()].ColumnValue = value; }
        }
        
        public DateTime BirthDate
        {
            get { return Convert.ToDateTime(ColumnCollections[Columns.BirthDate.ToString()].ColumnValue); }
            set { ColumnCollections[Columns.BirthDate.ToString()].ColumnValue = value; }
        }

        public string BirthPlace
        {
            get { return Convert.ToString(ColumnCollections[Columns.BirthPlace.ToString()].ColumnValue); }
            set { ColumnCollections[Columns.BirthPlace.ToString()].ColumnValue = value; }
        }

        public Sex SexType
        {
            get { if (Convert.ToBoolean(ColumnCollections[Columns.SexType.ToString()].ColumnValue)== false) return Sex.Male; else return Sex.Female; }
            set { ColumnCollections[Columns.SexType.ToString()].ColumnValue = value; }
        }

        public string School
        {
            get { return Convert.ToString(ColumnCollections[Columns.School.ToString()].ColumnValue); }
            set { ColumnCollections[Columns.School.ToString()].ColumnValue = value; }
        }

        public string Class
        {
            get { return Convert.ToString(ColumnCollections[Columns.Class.ToString()].ColumnValue); }
            set { ColumnCollections[Columns.Class.ToString()].ColumnValue = value; }
        }
        #endregion

        //#region "Internal Column Properties"
        //internal ColumnData StudentIDCol
        //{
        //    get { return ColumnCollections[Columns.StudentID.ToString()]; }
        //}

        //internal ColumnData StudentNameCol
        //{
        //    get { return ColumnCollections[Columns.StudentName.ToString()]; }
        //}        

        //internal ColumnData BirthDateCol
        //{
        //    get { return ColumnCollections[Columns.BirthDate.ToString()]; }
        //}

        //internal ColumnData BirthPlaceCol
        //{
        //    get { return ColumnCollections[Columns.BirthPlace.ToString()]; }
        //}

        //internal ColumnData SexCol
        //{
        //    get { return ColumnCollections[Columns.SexType.ToString()]; }
        //}

        //internal ColumnData SchoolCol
        //{
        //    get { return ColumnCollections[Columns.School.ToString()]; }
        //}

        //internal ColumnData ClassCol
        //{
        //    get { return ColumnCollections[Columns.Class.ToString()]; }
        //}
        //#endregion

        protected override void SetColumnCollection()
        {
            AddColumnCollection(Columns.StudentID.ToString(), "STUDENT_ID", ColumnData.DataTypes.String, true, true);
            AddColumnCollection(Columns.StudentName.ToString(), "STUDENT_NAME", ColumnData.DataTypes.String, false, true);
            AddColumnCollection(Columns.BirthDate.ToString(), "BIRTHDATE", ColumnData.DataTypes.DateTime, false, false);
            AddColumnCollection(Columns.BirthPlace.ToString(), "BIRTHPLACE", ColumnData.DataTypes.String, false, false);
            AddColumnCollection(Columns.SexType.ToString(), "SEX", ColumnData.DataTypes.Boolean, false, true);
            AddColumnCollection(Columns.School.ToString(), "SCHOOL", ColumnData.DataTypes.String, false, true);
            AddColumnCollection(Columns.Class.ToString(), "CLASS", ColumnData.DataTypes.String, false, true);
        }

        public override bool Equals(object obj)
        {
            Student that = (Student)obj;
            if (that != null && this.StudentID == that.StudentID) 
            {
                return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return StudentID.GetHashCode();
        }
    }
}
