using Cake.Core;
using Cake.Core.IO;
using Cake.PaketRestore.Attributes;
using System.Linq;
using System.Reflection;

namespace Cake.PaketRestore.Helpers
{
    /// <summary>
    /// Assist in building Argument string from Settings
    /// </summary>
    public static class ArgumentStringBuilder
    {
        #region Public Methods

        /// <summary>
        /// Build command line argument from a setting file
        /// </summary>
        /// <typeparam name="T">Type of the setting file</typeparam>
        ///  /// <param name="argumentBuilder">Cake argument builder</param>
        /// <param name="settingFile">The setting file from which the value should be pulled</param>
        public static void BuildArgumentString<T>(this ProcessArgumentBuilder argumentBuilder, T settingFile)
        {
            var properties = settingFile.GetType()
                .GetProperties()
                .Where(t => t.CustomAttributes.Any())
                .OrderBy(t => t.GetCustomAttribute<ArgumentBaseAttribute>().ArgumentOrder);
            foreach (var propertyInfo in properties)
            {
                argumentBuilder
                    .AppendSwitch(settingFile, propertyInfo)
                    .AppendString(settingFile, propertyInfo)
                    .AppendStringArray(settingFile, propertyInfo);
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Checks if a property has the <see cref="StringArgumentAttribute"/> and if it does append the parameter and its value to the argument
        /// </summary>
        /// <typeparam name="T">Type of the setting file</typeparam>
        /// <param name="argumentBuilder">Cake argument builder</param>
        /// <param name="settingFile">The setting file from which the value should be pulled</param>
        /// <param name="property">PropertyInfo that should be inspected</param>
        /// <returns></returns>
        private static ProcessArgumentBuilder AppendString<T>(this ProcessArgumentBuilder argumentBuilder, T settingFile,
            PropertyInfo property)
        {
            var attribute = property.GetCustomAttribute<StringArgumentAttribute>();
            var value = property.GetValue(settingFile) as string;
            if (attribute == null || string.IsNullOrEmpty(value))
            {
                return argumentBuilder;
            }

            argumentBuilder.Append($"{attribute.ArgumentName} {value}");
            return argumentBuilder;
        }

        /// <summary>
        ///Checks if a property has the <see cref="StringArrayArgumentAttribute"/> and if it does append the parameter and its value to the argument
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="argumentBuilder">Cake argument builder</param>
        /// <param name="settingFile">The setting file from which the value should be pulled</param>
        /// <param name="property">PropertyInfo that should be inspected</param>
        /// <returns></returns>
        private static ProcessArgumentBuilder AppendStringArray<T>(this ProcessArgumentBuilder argumentBuilder,
            T settingFile, PropertyInfo property)
        {
            var attribute = property.GetCustomAttribute<StringArrayArgumentAttribute>();
            var value = property.GetValue(settingFile) as string[];
            if (attribute == null || value == null || value.Length == 0)
            {
                return argumentBuilder;
            }

            var argumentCollection = string.Join(" ", value);
            argumentBuilder.Append($"{attribute.ArgumentName} {argumentCollection}");

            return argumentBuilder;
        }

        /// <summary>
        /// Checks if a property has the <see cref="SwitchArgumentAttribute"/> and if it does append the switch to the argument list
        /// </summary>
        /// <typeparam name="T">Type of the setting file</typeparam>
        /// <param name="argumentBuilder">Cake argument builder</param>
        /// <param name="settingFile">The setting file from which the value should be pulled</param>
        /// <param name="property">PropertyInfo that should be inspected</param>
        private static ProcessArgumentBuilder AppendSwitch<T>(this ProcessArgumentBuilder argumentBuilder, T settingFile,
            PropertyInfo property)
        {
            var attribute = property.GetCustomAttribute<SwitchArgumentAttribute>();
            var value = property.GetValue(settingFile) as bool?;
            if (attribute == null || value == null || value == false)
            {
                return argumentBuilder;
            }

            argumentBuilder.Append(attribute.ArgumentName);
            return argumentBuilder;
        }

        #endregion
    }
}