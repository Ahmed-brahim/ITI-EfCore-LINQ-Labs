using BLL.Entity;
using BLL.EntityList;
using DAL;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.EntityManagers
{
    public class StudentManager
    {
        public static int AddStudent(Student newStudent)
        {
            if(newStudent != null)
            {
                return DbManager.ExecuteNonQuery($"insert into students values({newStudent.StId}, '{newStudent.StName}', {newStudent.DeptId})");
            }
            else
            {
                throw new ArgumentException();
            }
        }

        public static int DeleteStudent(int stId)
        {
            if (stId >= 0)
            {
                return DbManager.ExecuteNonQuery($"delete from students where st_id = {stId}");
            }
            else
            {
                throw new ArgumentException();
            }
        }

        public static Students GetAllStudents()
        {
            DataTable dt = DbManager.ExecuteQuery("select * from students");
            Students students = new Students();
            foreach (DataRow dr in dt.Rows)
            {
                students.Add(new Entity.Student { StId = (int)dr["st_id"], StName = dr["st_name"].ToString(), DeptId = (int)dr["dept_id"] });
            }
            return students;
        }
        public static int UpdateStudent(Student newStudent)
        {
            if (newStudent != null) {
                 return DbManager.ExecuteNonQuery($"update students set st_name = '{newStudent.StName}', dept_id = {newStudent.DeptId} where st_id = {newStudent.StId}");
            }
            else
            {
                throw new ArgumentException();
            }
            
        }
    }
}
