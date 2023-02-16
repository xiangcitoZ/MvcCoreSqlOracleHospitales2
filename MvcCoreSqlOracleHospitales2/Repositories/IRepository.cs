using MvcCoreSqlOracleHospitales2.Models;

namespace MvcCoreSqlOracleHospitales.Repositories
{
    public interface IRepository
    {
        List<Hospital> GetHospitales();

        Hospital FindHospital(int idhospital);
        void InsertHosp(string nombre, string direccion,
            string telefono, int camas);
        void Update(int idhospital, string nombre, string direccion,
            string telefono, int camas);
        void Delete(int idhospital);


    }
}
