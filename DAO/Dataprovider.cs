using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanlyquanCoffe.DAO
{
    public class Dataprovider
    {
        private static Dataprovider instance;
      

      public static Dataprovider Instance
        {
            get { if (instance == null) instance = new Dataprovider(); return instance; }
            set { instance = value; }
        }

        private Dataprovider() { }
        private string StrConnection = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=QuanLyQuanCoffe;Integrated Security=True;Encrypt=False";
        //Ham de thuc thi 1 cau lenh tuong tac voi db va no tra lai 1 datatable
        public DataTable ExcuteQuery(string query, object[] parameter=null) {
            DataTable data = new DataTable();

            using (SqlConnection connection = new SqlConnection(StrConnection))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();             

               
               if(parameter != null)
                {String[] listpara = query.Split(' ');
                    int i = 0;
                    foreach (string item in listpara)
                    {
                        if (item.Contains('@'))
                        {
                            command.Parameters.AddWithValue(item, parameter[i]);
                            i++;
                        }

                    }

                }
                SqlDataAdapter adapter = new SqlDataAdapter(command);

                adapter.Fill(data);
                connection.Close();
            }
            
           return data;
        }
        public int ExcuteNonQuery(string query, object[] parameter = null)
        {
          
            int data = 0;
            using (SqlConnection connection = new SqlConnection(StrConnection))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();


                if (parameter != null)
                {
                    String[] listpara = query.Split(' ');
                    int i = 0;
                    foreach (string item in listpara)
                    {
                        if (item.Contains('@'))
                        {
                            command.Parameters.AddWithValue(item, parameter[i]);
                            i++;
                        }

                    }

                }
                data = command.ExecuteNonQuery();
                connection.Close();
            }

            return data;
        }
        public object ExcuteScalar(string query, object[] parameter = null)
        {

            object data = 0;
            using (SqlConnection connection = new SqlConnection(StrConnection))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();


                if (parameter != null)
                {
                    String[] listpara = query.Split(' ');
                    int i = 0;
                    foreach (string item in listpara)
                    {
                        if (item.Contains('@'))
                        {
                            command.Parameters.AddWithValue(item, parameter[i]);
                            i++;
                        }

                    }

                }
                data = command.ExecuteScalar();
                connection.Close();
            }

            return data;
        }
    }
}
