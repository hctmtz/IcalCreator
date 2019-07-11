using System;
using mx.com.medisist.util;

namespace IcalCreator.Data
{
    /// <summary>
    /// Clase de persistencia
    /// </summary>
    public class Persistance
    {
        public Persistance()
        {

        }

        public BD_SICYPROHEntities GetObjectContext()
        {
            string Error = String.Empty;

            BD_SICYPROHEntities DataContext = new BD_SICYPROHEntities(Sesion.GetConnectionString(true, "NotificacionesAlCorreo"));
            DataContext.CommandTimeout = 0;

            if (!(DB_Util.TestearConexion_DB(DataContext, ref Error)))
            {
                throw new Exception("Error de conexión a la base de datos: " + Error);
            }

            return DataContext;
        }
    }
}
