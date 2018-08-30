using Modulo1.Infraestructure;
using Modulo1.Modelo;
using SQLite.Net;
using SQLiteNetExtensions.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Modulo1.Dal
{
    public class ItemCardapioDAL
    {
        private SQLite.Net.SQLiteConnection sqlConnection;

        public ItemCardapioDAL()
        {
            this.sqlConnection = DependencyService.Get<IDatabaseConnection>().DbConnection();
            try
            {
                this.sqlConnection.CreateTable<ItemCardapio>();
            }
            catch (Exception e)
            {
                string m = e.Message;
                m = "";
            }
        }

        public void Add(ItemCardapio itemCardapio)
        {
            sqlConnection.Insert(itemCardapio);
        }

        public void DeleteById(long id)
        {
            sqlConnection.Delete<ItemCardapio>(id);
        }

        //public IEnumerable<ItemCardapio> GetAllWithChildren()
        //{
        //    return sqlConnection.GetAllWithChildren<ItemCardapio>().OrderBy(i => i.Nome).ToList();
        //}

        //public ItemCardapio GetItemById(long id)
        //{
        //    return sqlConnection.GetAllWithChildren<ItemCardapio>().FirstOrDefault(i => i.ItemCardapioId == id);
        //}
    }
}
