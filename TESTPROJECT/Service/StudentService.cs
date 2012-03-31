using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MitraTech.Core;
using MitraTech.Core.Mapper;
using MitraTech.Core.Model;

namespace StudentService
{
    public class StudentService
    {
        private StudentMapper Mapper = new StudentMapper();
        public Errors Errors = new Errors();

        public void test()
        {
            //IMapper test = Mapper.GetMapper();
            Student test = new Student();
            //test.
        }

        public List<Student> DoSave(List<Student> Students)
        {
            if (IsValidate())
            {
                Mapper.BulkSave(Students);
                this.Errors.Append(Mapper.Errors);
                if (!Mapper.Errors.IsError())
                {
                    return Mapper.GetAll();
                }
            }
            
            return null;
        }

        public List<Student> DoRead()
        {
            Mapper = new StudentMapper();
            return Mapper.GetAll();
        }

        private bool IsValidate()
        {
            return true;
        }

    }
}
