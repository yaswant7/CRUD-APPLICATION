using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YASHTRY.Models;

namespace YASHTRY.DAY7_CRUD
{
    public partial class CRUD : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

                LoadAll();
                UpdateFun();


            }

        }

        private void UpdateFun()
        {
            CrudTryTable Ctt = new CrudTryTable();

            SqlConnection cn = null;
            SqlCommand cmd = null;
            int Counter = 0;

            try
            {
                cn = new SqlConnection();
                cn.ConnectionString = @"data source=DESKTOP-1R8GHJS\SQLEXPRESS; initial catalog =tsegrp_2_2022;integrated security=true";
                int counter = 0;

                cmd = new SqlCommand();
                cmd.Connection = cn;
                cn.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_UPDATECTT";

                cmd.Parameters.AddWithValue("id", TextBox4.Text);



                cmd.Parameters.AddWithValue("@Name", TextBox1.Text);
                cmd.Parameters.AddWithValue("@Age", TextBox2.Text);
                cmd.Parameters.AddWithValue("@City", TextBox3.Text);



                Counter = cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                Label6.Text = ex.ToString();
            }
            finally
            {
                if (cn != null)
                {
                    cn.Close();
                    if (Counter.Equals(1))
                    {

                      Label6.Text = "Updated Successfully";
                      LoadAll();
                    }
                    cn.Dispose();
                    cn = null;
                }
            }


            


        }

        private void GetFun()
        {


            SqlConnection cn = null;
            SqlCommand cmd = null;
            int Counter = 0;

            try
            {
                cn = new SqlConnection();
                cn.ConnectionString = @"data source=DESKTOP-1R8GHJS\SQLEXPRESS; initial catalog =tsegrp_2_2022;integrated security=true";
                cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_GETCTT";
                cn.Open();

                cmd.Parameters.AddWithValue("id", TextBox4.Text);





                SqlDataReader sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    TextBox1.Text = sdr["Name"].ToString();
                    TextBox2.Text = sdr["Age"].ToString();
                    TextBox3.Text = sdr["City"].ToString();

                }





            }
            catch (Exception ex)
            {
                Label6.Text = ex.ToString();
            }
            finally
            {
                if (cn != null)
                {
                    cn.Close();
                    cn.Dispose();
                    cn = null;
                }
            }


        }
        public void DeleteFun()
        {
            SqlConnection cn = null;
            SqlCommand cmd = null;


            try
            {
                cn = new SqlConnection();
                cn.ConnectionString = @"data source=DESKTOP-1R8GHJS\SQLEXPRESS; initial catalog =tsegrp_2_2022;integrated security=true";
                cmd = new SqlCommand();
                cmd.Connection = cn;
                cn.Open();
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.CommandText = "USP_DELETECTT";
                cmd.Parameters.AddWithValue("id", TextBox4.Text);
                cmd.ExecuteNonQuery();





            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (cn != null)
                {
                    cn.Close();

                    Label6.Text = "Record Deleted Successfully";
                    LoadAll();
                    cn.Dispose();
                    cn = null;
                }
            }
        }

        private void Clear()

        {
            TextBox2.Text = "";
            TextBox3.Text = "";
            TextBox1.Text = "";
            Label6.Text = "";
        }

        private void LoadAll()
        {
            CrudTryTable ctt = new CrudTryTable();
            DataSet dset = ctt.GetlistofAllData();
            GridView1.DataSource = dset;
            GridView1.DataBind();
        }


        protected void Button1_Click(object sender, EventArgs e)
        {
            CrudTryTable Ctt = new CrudTryTable();


            try
            {
                Ctt.Name = TextBox1.Text;
                Ctt.Age = TextBox2.Text;
                Ctt.City = TextBox3.Text;

                int Counter = Ctt.InsertFun(Ctt);

                if (Counter.Equals(1))
                {
                    Label6.Text = " !! Registered Successfully !! ";
                    LoadAll();

                }


            }
            catch (Exception ex)
            {
                Label6.Text = ex.ToString();
            }


        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            UpdateFun();

        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            DeleteFun();
        }

        protected void Button4_Click(object sender, EventArgs e)
        {

            Clear();

        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            GetFun();

        }

    } 
}




