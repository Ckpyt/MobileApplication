
namespace MobileApplication
{

    /// <summary>
    /// Parent class for all the objects. Has only id and base virtual methods
    /// </summary>
    public abstract class DataObject
    {
        /// <summary> id in the database </summary>
        public int id;

        /// <summary>
        /// virtual method for copying objects.
        /// should be override in any children class
        /// </summary>
        /// <param name="obj">source</param>
        public abstract void CopyFrom(DataObject obj);
    }
}
