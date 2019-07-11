using System;
using System.ComponentModel;
using System.Reflection;

namespace IcalCreator.Util
{
    /// <summary>
    /// Contiene métodos de apoyo y otras utilidades para trabajar estructuras de tipo <see cref="Enum"/>
    /// </summary>
    public class EnumUtil
    {
        /// <summary>
        /// Regresa la representación de tipo <see cref="string"/> del elemento de un <see cref="Enum"/>
        /// que tenga alguna propiedad de tipo <see cref="DescriptionAttribute"/>
        /// </summary>
        /// <param name="value">Elemento miembro de un <code>Enum</code></param>
        /// <returns>Representación <see cref="string"/> de un elemento de <code>Enum</code></returns>
        public static string GetEnumDescription(Enum value)
        {
            FieldInfo fieldInfo = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes != null && attributes.Length > 0)
                return attributes[0].Description;
            else
                return value.ToString();
        }
    }
}
