using SQLite.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modulo1.Infraestructure
{
    public interface IDatabaseConnection
    {
        SQLiteConnection DbConnection();
    }
}
