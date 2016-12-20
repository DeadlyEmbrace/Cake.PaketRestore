using System;

namespace Cake.PaketRestore.Attributes
{
    /// <summary>
    /// Attribute base class to make building of other attribute classes easier and more composable
    /// </summary>
    public abstract class ArgumentBaseAttribute : Attribute
    {
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="argumentName">Name of the Argument</param>
        /// <param name="argumentOrder">Where in the output the argument value should appear</param>
        protected ArgumentBaseAttribute(string argumentName, int argumentOrder)
        {
            ArgumentName = argumentName;
            ArgumentOrder = argumentOrder;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Name of the Argument
        /// </summary>
        public string ArgumentName { get; set; }

        /// <summary>
        /// Order of the argument in the output string
        /// </summary>
        public int ArgumentOrder { get; set; }

        #endregion
    }
}