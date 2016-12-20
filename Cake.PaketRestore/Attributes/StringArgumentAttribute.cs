using System;

namespace Cake.PaketRestore.Attributes
{
    /// <summary>
    /// Attribute used to decorate command line parameters that provide a string so their name is easily accessable
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class StringArgumentAttribute : ArgumentBaseAttribute
    {
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="argumentName"></param>
        /// <param name="argumentOrder">Where in the output the argument value should appear</param>
        public StringArgumentAttribute(string argumentName, int argumentOrder)
            : base(argumentName, argumentOrder)
        { }

        #endregion
    }
}