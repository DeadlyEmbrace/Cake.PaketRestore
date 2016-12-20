using System;

namespace Cake.PaketRestore.Attributes
{
    /// <summary>
    /// Attribute used to decorate command line switches so their name is easily accessable
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class SwitchArgumentAttribute : ArgumentBaseAttribute
    {
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="argumentName">Name of the Argument</param>
        /// <param name="argumentOrder">Where in the output the argument value should appear</param>
        public SwitchArgumentAttribute(string argumentName, int argumentOrder)
            : base(argumentName, argumentOrder)
        {
        }

        #endregion
    }
}