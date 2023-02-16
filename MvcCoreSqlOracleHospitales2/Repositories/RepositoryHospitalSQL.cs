using Microsoft.AspNetCore.Http.HttpResults;
using MvcCoreSqlOracleHospitales2.Models;
using System.Data;
using System.Data.SqlClient;


#region
//CREATE PROCEDURE SP_DELETE_DOCTOR(@IDHOSPITAL INT)
//AS
//    delete from HOSPITAL
//	where HOSPITAL_COD = @IDHOSPITAL
//GO


//CREATE PROCEDURE SP_INSERT_HOSPITAL(@nombre NVARCHAR, @direccion NVARCHAR,
//@telefono NVARCHAR, @numcamas INT)
//as
//    DECLARE @IDHOSPITAL INT
//	SELECT @IDHOSPITAL =  MAX(HOSPITAL_COD) +1  FROM HOSPITAL
//	Insert into HOSPITAL values (@IDHOSPITAL, @nombre, @direccion
//    , @telefono, @numcamas )

//go


//CREATE PROCEDURE SP_UPADTE_HOSPITAL(@IDHOSPITAL INT, @nombre NVARCHAR, @direccion NVARCHAR,
//@telefono NVARCHAR, @numcamas INT)
//as
//    UPDATE  HOSPITAL SET NOMBRE = @nombre, DIRECCION = @direccion,
//    TELEFONO = @telefono, NUM_CAMA = @numcamas where HOSPITAL_COD = @IDHOSPITAL

//go


//CREATE PROCEDURE SP_FIND_HOSPITAL(
//@IDHOSPITAL INT)
//AS
//	select * from HOSPITAL where HOSPITAL_COD = @IDHOSPITAL
//GO

#endregion



namespace MvcCoreSqlOracleHospitales.Repositories
{
    public class RepositoryHospitalSQL : IRepository
    {
        SqlConnection cn;
        SqlCommand com;
        SqlDataAdapter adapter;
        private DataTable tablaHospital;

        public RepositoryHospitalSQL()
        {
            string connectionString =
               @"Data Source=LOCALHOST\DESARROLLO;Initial Catalog=HOSPITAL;Persist Security Info=True;User ID=SA;Password=MCSD2023";
            this.cn = new SqlConnection(connectionString);
            this.com = new SqlCommand();
            this.com.Connection = this.cn;
            string sql = "select * from hospital";
            this.adapter = new SqlDataAdapter(sql, connectionString);
            this.tablaHospital = new DataTable();
            this.adapter.Fill(this.tablaHospital);
        }


        public void Delete(int idhospital)
        {
            SqlParameter pamid = new SqlParameter("@idhospital", idhospital);
            this.com.Parameters.Add(pamid);
            

            this.com.CommandType = CommandType.StoredProcedure;
            this.com.CommandText = "SP_DELETE_HOSPITAL";
            this.cn.Open();
            this.com.ExecuteNonQuery();
            this.cn.Close();
            this.com.Parameters.Clear();
        }

    

        public Hospital FindHospital(int idhospital)
        {
            var consulta = from datos in this.tablaHospital.AsEnumerable()
                            where datos.Field<int>("HOSPITAL_COD") == idhospital
                           select new Hospital
                            {
                               IdHospital = datos.Field<int>("HOSPITAL_COD"),
                               Nombre = datos.Field<string>("NOMBRE"),
                               Direccion = datos.Field<string>("DIRECCION"),
                               Telefono = datos.Field<string>("TELEFONO"),
                               Num_Cama = datos.Field<int>("NUM_CAMA")
                           };
            return consulta.FirstOrDefault(); 
        }

        public List<Hospital> GetHospitales()
        {
            var consulta = from datos in this.tablaHospital.AsEnumerable()
                           select datos;
            List<Hospital> hospitales = new List<Hospital>();
            foreach (var row in consulta)
            {
                Hospital hosp = new Hospital
                {
                    IdHospital = row.Field<int>("HOSPITAL_COD"),
                    Nombre = row.Field<string>("NOMBRE"),
                    Direccion = row.Field<string>("DIRECCION"),
                    Telefono = row.Field<string>("TELEFONO"),
                    Num_Cama = row.Field<int>("NUM_CAMA")
                };
                hospitales.Add(hosp);
            }
            return hospitales;
        }

      
        public void InsertHosp(string nombre, string direccion, string telefono, int camas)
        {
            
            //int maximo = this.GetMaximoHospital();
            //SqlParameter pamid = new SqlParameter("@idhospital", maximo);
            //this.com.Parameters.Add(pamid);
            SqlParameter pamnom = new SqlParameter("@nombre", nombre);
            this.com.Parameters.Add(pamnom);
            SqlParameter pamdir = new SqlParameter("@direccion", direccion);
            this.com.Parameters.Add(pamdir);
            SqlParameter pamtel = new SqlParameter("@telefono", telefono);
            this.com.Parameters.Add(pamtel);
            SqlParameter pamcam = new SqlParameter("@numcamas", camas);
            this.com.Parameters.Add(pamcam);

            this.com.CommandType = CommandType.StoredProcedure;
            this.com.CommandText = "SP_INSERT_HOSPITAL";
            this.cn.Open();
            this.com.ExecuteNonQuery();
            this.cn.Close();
            this.com.Parameters.Clear();
        }

        public void Update(int idhospital, string nombre, string direccion, string telefono, int camas)
        {
           
            SqlParameter pamid = new SqlParameter("@idhospital", idhospital);
            this.com.Parameters.Add(pamid);
            SqlParameter pamnom = new SqlParameter("@nombre", nombre);
            this.com.Parameters.Add(pamnom);
            SqlParameter pamdir = new SqlParameter("@direccion", direccion);
            this.com.Parameters.Add(pamdir);
            SqlParameter pamtel = new SqlParameter("@telefono", telefono);
            this.com.Parameters.Add(pamtel);
            SqlParameter pamcam = new SqlParameter("@numcamas", camas);
            this.com.Parameters.Add(pamcam);

            this.com.CommandType = CommandType.StoredProcedure;
            this.com.CommandText = "SP_UPADTE_HOSPITAL";
            this.cn.Open();
            this.com.ExecuteNonQuery();
            this.cn.Close();
            this.com.Parameters.Clear();
        }
    }
}
