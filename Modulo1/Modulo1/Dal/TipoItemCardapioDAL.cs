using Modulo1.Infraestructure;
using Modulo1.Modelo;
using SQLite.Net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Modulo1.Dal
{
    public class TipoItemCardapioDAL
    {
        private SQLiteConnection sqlConnection;

        public TipoItemCardapioDAL()
        {
            try
            {
                this.sqlConnection = DependencyService.Get<IDatabaseConnection>().DbConnection();
                this.sqlConnection.CreateTable<TipoItemCardapio>();
            }
            catch(Exception e)
            {
                string m = e.Message;
                m = "";
            }
        }

        public IEnumerable<TipoItemCardapio> GetAll()
        {
            return (from t in sqlConnection.Table<TipoItemCardapio>() select t).OrderBy(i => i.Nome).ToList();
        }

        public TipoItemCardapio GetItemById(long id)
        {
            return sqlConnection.Table<TipoItemCardapio>().FirstOrDefault(t => t.TipoItemCardapioId == id);
        }

        public void DeleteById(long id)
        {
            sqlConnection.Delete<TipoItemCardapio>(id);
        }

        public void Add(TipoItemCardapio tipoItemCardapio)
        {
            sqlConnection.Insert(tipoItemCardapio);
        }

        public void Update(TipoItemCardapio tipoItemCardapio)
        {
            sqlConnection.Update(tipoItemCardapio);
        }       
    }
}
