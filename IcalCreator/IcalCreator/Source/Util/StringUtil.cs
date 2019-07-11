using System;
using System.Globalization;
using System.Text;

namespace IcalCreator.Util
{
    /// <summary>
    /// Contiene utilidades para trabajar con variables de tipo <see cref="String"/>
    /// </summary>
    public class StringUtil
    {
        /// <summary>
        /// Removes any character that does not belong to standard ASCII symbol table.
        /// </summary>
        /// <param name="inputText">String with invalid ASCII symbols.</param>
        /// <returns>String with ASCII valid characters.</returns>
        public static string RemoveInvalidChar(string inputText)
        {
            var normalizedString = inputText.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);

                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }
            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }

        /// <summary>
        /// Convierte un <see cref="TimeSpan"/> a su representación en <see cref="string"/>
        /// </summary>
        /// <param name="span">Variable de tipo <see cref="TimeSpan"/> con la cantidad de tiempo</param>
        /// <returns>Representación de tipo <see cref="string"/> del <see cref="TimeSpan"/></returns>
        public static string TimeSpanToString(TimeSpan span)
        {
            string formatted = string.Format("{0}{1}{2}{3}",
                span.Duration().Days > 0 ? string.Format("{0:0} día{1}, ", span.Days, span.Days == 1 ? string.Empty : "s") : string.Empty,
                span.Duration().Hours > 0 ? string.Format("{0:0} hora{1}, ", span.Hours, span.Hours == 1 ? string.Empty : "s") : string.Empty,
                span.Duration().Minutes > 0 ? string.Format("{0:0} minuto{1}, ", span.Minutes, span.Minutes == 1 ? string.Empty : "s") : string.Empty,
                span.Duration().Seconds > 0 ? string.Format("{0:0} segundo{1}", span.Seconds, span.Seconds == 1 ? string.Empty : "s") : string.Empty);

            if (formatted.EndsWith(", ")) formatted = formatted.Substring(0, formatted.Length - 2);

            if (string.IsNullOrEmpty(formatted)) formatted = "0 segundos";

            return formatted;
        }

        /// <summary>
        /// Formatea una variable <see cref="string"/> con código JSON
        /// </summary>
        public static string FormatJson(string jsonString)
        {
            var stringBuilder = new StringBuilder();

            bool escaping = false;
            bool inQuotes = false;
            int indentation = 0;

            foreach (char character in jsonString)
            {
                if (escaping)
                {
                    escaping = false;
                    stringBuilder.Append(character);
                }
                else
                {
                    if (character == '\\')
                    {
                        escaping = true;
                        stringBuilder.Append(character);
                    }
                    else if (character == '\"')
                    {
                        inQuotes = !inQuotes;
                        stringBuilder.Append(character);
                    }
                    else if (!inQuotes)
                    {
                        if (character == ',')
                        {
                            stringBuilder.Append(character);
                            stringBuilder.Append("\r\n");
                            stringBuilder.Append('\t', indentation);
                        }
                        else if (character == '[' || character == '{')
                        {
                            stringBuilder.Append(character);
                            stringBuilder.Append("\r\n");
                            stringBuilder.Append('\t', ++indentation);
                        }
                        else if (character == ']' || character == '}')
                        {
                            stringBuilder.Append("\r\n");
                            stringBuilder.Append('\t', --indentation);
                            stringBuilder.Append(character);
                        }
                        else if (character == ':')
                        {
                            stringBuilder.Append(character);
                            stringBuilder.Append('\t');
                        }
                        else
                        {
                            stringBuilder.Append(character);
                        }
                    }
                    else
                    {
                        stringBuilder.Append(character);
                    }
                }
            }
            return stringBuilder.ToString();
        }

        /// <summary>
        /// Convierte la primera letra de una palabra 
        /// a mayúsculas y deja el resto en minúsculas
        /// </summary>
        /// <param name="input">Texto de entrada</param>
        /// <returns><see cref="string"/> con el formato de título</returns>
        public static string FirstLetterToUpper(string input)
        {
           return string.IsNullOrEmpty(input) ? string.Empty : CultureInfo.CurrentCulture.TextInfo.ToTitleCase(input.ToLower());
        }

    }
}
