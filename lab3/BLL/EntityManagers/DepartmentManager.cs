using BLL.EntityList;
using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.EntityManagers
{
    public static class DepartmentManager
    {
        public static Departments GetAllDepartments()
        {
            DataTable dt = DbManager.ExecuteQuery("select * from department");
            Departments departments = new Departments();
            foreach(DataRow dr in dt.Rows)
            {
                departments.Add(new Entity.Department { DeptId = (int)dr["dept_id"], DeptName = dr["dept_name"].ToString() });    
            }
            return departments;
        }
    }
}
