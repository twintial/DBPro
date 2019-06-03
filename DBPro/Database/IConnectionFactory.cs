using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DBPro.Database
{
    public interface IConnectionFactory
    {
        OracleConnection CreateConnection();
    }
}
