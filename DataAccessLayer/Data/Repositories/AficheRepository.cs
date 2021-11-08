using Dapper;
using ImprentaAPI.BusinessLogicLayer.Models;
using ImprentaAPI.Data;
using ImprentaAPI.DataAccessLayer.Data.Repositories.Interfaces;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImprentaAPI.DataAccessLayer.Data.Repositories
{
    public class AficheRepository : IAficheRepository
    {
        private PostgreSQLConfiguration _connection;
        private NpgsqlConnection _baseAbierta;

        protected NpgsqlConnection AbreConexion()
        {
            return _connection.AbreConexion();
        }
        public AficheRepository(PostgreSQLConfiguration connectionString)
        {
            _connection = connectionString;
            _baseAbierta = AbreConexion();
        }
        public int getCantidadPlacas(string placa)
        {
            switch (placa)
            {
                case "Full Color":
                    return 4;
                case "Duo Tono":
                    return 2;
                case "Mono Color":
                    return 1;
                default:
                    return 1;
            }
        }
        public async Task<PrecioModel> GetPrecioGramajeBDAsync(string nombre,string Gramaje)
        {
            var sql = @"SELECT * FROM public.precios WHERE nombre='"+nombre+"' and tipo='"+Gramaje+"'";
            var res = await _baseAbierta.QueryFirstOrDefaultAsync<PrecioModel>(sql, new PrecioModel { });
            return res; 
        }
        public double GetPrecioImpresion(int cant)
        {
            if (cant<=1000)
                return 35;
            
            int res = (cant / 1000)+1;
            var res2 = 35 * res;
            return res2;
        }
        public double getPrecioTamPlaca (string TamPlaca)
        {
            switch (TamPlaca)
            {
                case "GTO 52x33":
                    return 15;
                case "ROLAND 52X72":
                    return 40;     
                default:
                    return 1;
            }
        }
        public double RedondeoImprenta(int n)
        {
            var num = n.ToString();
            var res= Int32.Parse(num[0].ToString()) + (0.1*float.Parse(num[1].ToString()));
            res = res * Math.Pow(10,num.Length-2);
            return res;
        }
        public string Tipogramaje(string nombre,string gramaje)
        {
            return gramaje.Remove(0,nombre.Length +1 );
        }
        public async Task<CotizarAficheModel> GetCotizacionAsync(AficheModel aficheModel)
        {
            var tipogramaje = Tipogramaje(aficheModel.Tipo_papel,aficheModel.Gramaje_papel);

            var cotizacion= new CotizarAficheModel();
            var cantidad_Placas = getCantidadPlacas(aficheModel.Color);
            var papel_requerido = aficheModel.Cantidad / cantidad_Placas ; // ya no hay redondeo



            var precio_pliego = await GetPrecioGramajeBDAsync(aficheModel.Tipo_papel, tipogramaje);
                
            var Total_precio_Papel = papel_requerido * precio_pliego.precioneto;
            var precio_impresion = GetPrecioImpresion(aficheModel.Cantidad)* cantidad_Placas;
            var TotalPlaca = getPrecioTamPlaca(aficheModel.Tam_Placa)* cantidad_Placas;
            var TotalCosto = Total_precio_Papel + precio_impresion + TotalPlaca + aficheModel.Precio_Design + aficheModel.Precio_Acabado;
            cotizacion.Neto = TotalCosto;
            cotizacion.IVA = TotalCosto*0.16;
            cotizacion.Con_Factura = TotalCosto +cotizacion.IVA;
            cotizacion.Total = Math.Round((double)((aficheModel.Ganancia * cotizacion.Con_Factura) + cotizacion.Con_Factura));
            
            return cotizacion;
        }

    }
}
