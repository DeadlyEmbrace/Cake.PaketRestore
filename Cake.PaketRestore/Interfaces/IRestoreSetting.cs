namespace Cake.PaketRestore.Interfaces
{
    /// <summary>
    /// Interface for Setting classes that spesify whether or not Paket Bootstrapper and executable should be retrieved
    /// </summary>
    public interface IRestoreSetting
    {
        #region Properties

        /// <summary>
        /// Should the Paket Bootstrapper be retrieved
        /// </summary>
        bool RetrieveBootstrapper { get; set; }

        /// <summary>
        /// Should the Paket executable be retrieved
        /// </summary>
        bool RetrievePaketExecutable { get; set; }

        #endregion
    }
}