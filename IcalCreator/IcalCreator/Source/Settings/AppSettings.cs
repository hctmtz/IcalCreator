using System.IO;
using System.Web.Script.Serialization;
using IcalCreator.Util;

namespace IcalCreator.Settings
{
    /// <summary>
    /// Contiene utilidades para crear un archivo de configuración en JSON. 
    /// Esta clase no se usa directamente, sólo sirve cómo base para clases derivadas, 
    /// se implementa y se ajusta de acuerdo a lo que se necesita.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class AppSettings<T> where T : new()
    {
        private const string DEFAULT_FILENAME = "IcalCreatorSettings.json";

        public void Save(string fileName = DEFAULT_FILENAME)
        {
            File.WriteAllText(fileName, StringUtil.FormatJson((new JavaScriptSerializer()).Serialize(this)));
        }

        public static void Save(T pSettings, string fileName = DEFAULT_FILENAME)
        {
            File.WriteAllText(fileName, StringUtil.FormatJson((new JavaScriptSerializer()).Serialize(pSettings)));
        }

        public static T Load(string fileName = DEFAULT_FILENAME)
        {
            T t = new T();
            if (File.Exists(fileName))
                t = (new JavaScriptSerializer()).Deserialize<T>(File.ReadAllText(fileName));
            return t;
        }
    }
}
