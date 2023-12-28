using System.Data.SqlClient;
using System.Data;
using System.Data.Common;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using QLSieuThiBHX.DTO;

namespace QLSieuThiBHX.DAO
{
    internal class DataProvider
    {
        private static DataProvider _instance;
        private static List<DTO_TKSP_Ngay> dto_TKSP_Ngays;
        private readonly string connectionSTR;

        public static DataProvider Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new DataProvider();
                }
                return _instance;
            }
        }

        internal static List<DTO_TKSP_Ngay> Dto_TKSP_Ngays { get => dto_TKSP_Ngays; set => dto_TKSP_Ngays = value; }

        /// <summary>
        /// Cách truy cập CSDL do dòng này quyết định.
        /// Source=Tên Máy Chủ, Catalog=Tên CSDL
        /// </summary>
        private DataProvider()
        {
            connectionSTR = "Data Source=HP-PAVILION-15;Initial Catalog=QLSieuThiBHX;Integrated Security=True";
        }

        /// <summary>
        /// Truy xuất CSDL có KQ trả về. Dùng để đọc KQ sau khi Thực Thi codeSQL.
        /// </summary>
        /// <returns>Một Bảng Dữ Liệu Table(Row-Column)</returns>
        public DataTable ExecuteQuery(string query, object[] parameter = null)
        {
            DataTable data = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionSTR))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);

                if (parameter != null)
                {
                    string[] listParams = query.Split(' ');
                    int i = 0;

                    foreach (string item in listParams)
                    {
                        if (item.StartsWith("@"))
                        {
                            command.Parameters.AddWithValue(item, parameter[i]);
                            i += 1;
                        }
                    }
                }

                SqlDataAdapter adapter = new SqlDataAdapter(command);

                adapter.Fill(data);

                connection.Close();
            }

            return data;
        }

        /// <summary>
        /// Truy xuất CSDL không có KQ trả về. DÙng để Thực Thi Code SQL.
        /// </summary>
        public int ExecuteNonQuery(string query, object[] parameter = null)
        {
            int numRowEffected = 0;

            using (SqlConnection connection = new SqlConnection(connectionSTR))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);

                if (parameter != null)
                {
                    string[] listParams = query.Split(' ');
                    int i = 0;

                    foreach (string item in listParams)
                    {
                        if (item.StartsWith("@"))
                        {
                            command.Parameters.AddWithValue(item, parameter[i]);
                            i += 1;
                        }
                    }
                }

                numRowEffected = command.ExecuteNonQuery();

                connection.Close();
            }

            return numRowEffected;
        }

        //public object ExecuteScalar(string query, object[] parameter = null)
        //{
        //    object data = null;

        //    using (SqlConnection connection = new SqlConnection(connectionSTR))
        //    {
        //        connection.Open();

        //        SqlCommand command = new SqlCommand(query, connection);

        //        if (parameter != null)
        //        {
        //            string[] listParams = query.Split(' ');
        //            int i = 0;

        //            foreach (string item in listParams)
        //            {
        //                if (item.StartsWith("@"))
        //                {
        //                    command.Parameters.AddWithValue(item, parameter[i]);
        //                    i += 1;
        //                }
        //            }
        //        }

        //        data = command.ExecuteScalar();

        //        connection.Close();
        //    }

        //    return data;
        //}
    }
}
