namespace Cake.PaketRestore.Attributes
{
    /// <summary>
    /// Attribute used to decorate command line parameters that provide a list of strings so their name is easily accessable
    /// </summary>
    public class StringArrayArgumentAttribute : ArgumentBaseAttribute
    {
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="argumentName">Name of the String Array argument</param>
        /// <param name="argumentOrder">Where in the output the argument value should appear</param>
        public StringArrayArgumentAttribute(string argumentName, int argumentOrder)
            : base(argumentName, argumentOrder)
        { }

        #endregion
    }
}