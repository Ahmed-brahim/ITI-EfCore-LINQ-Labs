using BLL.Entity;
using BLL.EntityList;
using BLL.EntityManagers;
using Microsoft.Data.SqlClient;
using System.Dynamic;
using System.Windows.Forms;

namespace lab3._1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //step 1 connection
            //using (var connection = new SqlConnection("Data Source=LAPTOP-DC6PHRTK\\SQLEXPRESS;Initial Catalog=WFormDb;Integrated Security=True;Trust Server Certificate=True"))
            //{
            //    //step 2 command
            //    SqlCommand cmd = new SqlCommand("select * from department", connection);

            //    //step 3 open connection
            //    connection.Open();
            //    //execute
            //    List<Department> departments = new List<Department>();
            //    SqlDataReader reader = cmd.ExecuteReader();
            //    while (reader.Read())
            //    {
            //        departments.Add(new Department() { Id = reader.GetInt32(0), Name = reader.GetString(1) });
            //    }
            //    comboBox1.DataSource = departments;
            //    comboBox1.ValueMember = "Id";
            //    comboBox1.DisplayMember = "Name";

            //}
            comboBox1.DataSource = DepartmentManager.GetAllDepartments();
            comboBox1.ValueMember = "DeptId";
            comboBox1.DisplayMember = "DeptName";
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Department SelectedDept = (Department)comboBox1.SelectedItem;

            Students students = StudentManager.GetAllStudents();

            dataGridView1.DataSource = students;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dataGridView1.Rows[e.RowIndex].DataBoundItem is Student student)
            {
                int affectedRows = StudentManager.UpdateStudent(student);
                if (affectedRows > 0)
                    MessageBox.Show("Student updated successfully.");
                else
                    MessageBox.Show("Update failed.");
            }
        }

        private void dataGridView1_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            if (e.Row.Index >= 0 && dataGridView1.Rows[e.Row.Index].DataBoundItem is Student newStudent)
            {

                if (newStudent.StId == 0)
                {
                    int rows = StudentManager.AddStudent(newStudent);
                    if (rows > 0)
                    { }
                    else
                        MessageBox.Show("Failed to add student.");
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string name = txtStudentName.Text.Trim();
            Department selectedDept = comboBox1.SelectedItem as Department;
            int id = (int)numericUpDown1.Value;

            if (string.IsNullOrEmpty(name) || selectedDept == null || id == 0)
            {
                MessageBox.Show("Please enter a name, id and choose a department.");
                return;
            }

            Student newStudent = new Student
            {
                StName = name,
                DeptId = selectedDept.DeptId,
                StId = id
            };

            int rows = StudentManager.AddStudent(newStudent);
            if (rows > 0)
            {
                MessageBox.Show("Student added successfully.");
                txtStudentName.Clear();
                numericUpDown1.Value = 0;
                dataGridView1.DataSource = StudentManager.GetAllStudents(); // reload grid
            }
            else
            {
                MessageBox.Show("Failed to add student.");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null && dataGridView1.CurrentRow.DataBoundItem is Student selectedStudent)
            {
                var confirm = MessageBox.Show("Are you sure you want to delete this student?", "Confirm", MessageBoxButtons.YesNo);

                if (confirm == DialogResult.Yes)
                {
                    int rows = StudentManager.DeleteStudent(selectedStudent.StId);
                    if (rows > 0)
                    {
                        dataGridView1.DataSource = StudentManager.GetAllStudents();
                        MessageBox.Show("Student deleted successfully.");
                    }
                    else
                    {
                        MessageBox.Show("Delete failed.");
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a student to delete.");
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
