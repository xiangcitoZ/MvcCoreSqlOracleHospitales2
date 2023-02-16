﻿using MvcCoreSqlOracleHospitales2.Models;
using Oracle.ManagedDataAccess.Client;
using System.Data;


#region

//CREATE OR REPLACE PROCEDURE SP_DELETE_HOSPITAL
//(P_IDHOSPITAL HOSPITAL.HOSPITAL_COD%TYPE)
//AS
//BEGIN
//  delete from HOSPITAL where HOSPITAL_cOD = p_iddoctor;
//commit;
//END;



#endregion

namespace MvcCoreSqlOracleHospitales.Repositories
{
    public class RepositoryHospitalOracle : IRepository
    {
        private OracleConnection cn;
        private OracleCommand com;
        private OracleDataAdapter adapter;
        private DataTable tablaHospital;


        public RepositoryHospitalOracle()
        {

            string connectionString = "User Id=SYSTEM;Password=oracle; Data Source=localhost:1521/XE";


            this.cn = new OracleConnection(connectionString);
            this.com = new OracleCommand();
            this.com.Connection = this.cn;
            string sql = "select * from hospital";
            this.adapter = new OracleDataAdapter(sql, connectionString);
            this.tablaHospital = new DataTable();
            this.adapter.Fill(this.tablaHospital);
        }

        public void Delete(int idhospital)
        {
            OracleParameter pamid = new OracleParameter(":p_idhospital", idhospital);
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
                           select new Hospital
                           {
                               IdHospital = datos.Field<int>("HOSPITAL_COD"),
                               Nombre = datos.Field<string>("NOMBRE"),
                               Direccion = datos.Field<string>("DIRECCION"),
                               Telefono = datos.Field<string>("TELEFONO"),
                               Num_Cama = datos.Field<int>("NUM_CAMA")

                           };

            return consulta.ToList();
        }

        private int GetMaxIdHosptial()
        {
            var maximo =
                (from datos in this.tablaHospital.AsEnumerable()
                 select datos).Max(x => x.Field<int>("HOSPITAL_COD")) + 1;
            return maximo;
        }

        public void InsertHosp(string nombre, string direccion, string telefono, int numcamas)
        {
           

           
            int maximo = this.GetMaxIdHosptial();
            OracleParameter pamid = new OracleParameter("p_idhospital", maximo);
            this.com.Parameters.Add(pamid);
            OracleParameter pamape = new OracleParameter(":p_nombre", nombre);
            this.com.Parameters.Add(pamape);
            OracleParameter pamesp = new OracleParameter(":p_direccion", direccion);
            this.com.Parameters.Add(pamesp);
            OracleParameter pamsal = new OracleParameter(":p_telefono", telefono);
            this.com.Parameters.Add(pamsal);
            OracleParameter pamcam = new OracleParameter(":p_camas", numcamas);
            this.com.Parameters.Add(pamcam);


            this.com.CommandType = CommandType.StoredProcedure;
            this.com.CommandText = "SP_INSERT_HOSPITAL";
            this.cn.Open();
            this.com.ExecuteNonQuery();
            this.cn.Close();
            this.com.Parameters.Clear();
        }

        public void Update(int idhospital, string nombre, string direccion, string telefono, int numcamas)
        {
            
            OracleParameter pamid = new OracleParameter("p_idhospital", idhospital);
            this.com.Parameters.Add(pamid);
            OracleParameter pamape = new OracleParameter(":p_nombre", nombre);
            this.com.Parameters.Add(pamape);
            OracleParameter pamesp = new OracleParameter(":p_direccion", direccion);
            this.com.Parameters.Add(pamesp);
            OracleParameter pamsal = new OracleParameter(":p_telefono", telefono);
            this.com.Parameters.Add(pamsal);
            OracleParameter pamcam = new OracleParameter(":p_camas", numcamas);
            this.com.Parameters.Add(pamcam);


            this.com.CommandType = CommandType.StoredProcedure;
            this.com.CommandText = "SP_UPDATE_HOSPITAL";
            this.cn.Open();
            this.com.ExecuteNonQuery();
            this.cn.Close();
            this.com.Parameters.Clear();
        }
    }
}
