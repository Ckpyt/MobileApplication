
namespace MobileApplication
{
    /// <summary>
    /// Enumeration of objects types
    /// </summary>
    enum DataType
    {
        none,
        phoneModel,
        function,
        operation
    }

    /// <summary>
    /// Description of a phone model
    /// </summary>
    public class PhoneModel : DataObject
    {
        /// <summary> Phone name </summary>
        public string name;
        /// <summary> Parent id. like iphone 6s --> iphone 6 --> iphone </summary>
        public int parentId;

        /// <summary>
        /// convert object to string. Returns name. used into comboboxes and treeview
        /// </summary>
        /// <returns> name </returns>
        public override string ToString()
        {
            return name;
        }

        /// <summary>
        /// virtual method for copying objects
        /// </summary>
        /// <param name="obj">source</param>
        public override void CopyFrom(DataObject obj)
        {
            PhoneModel phone = obj as PhoneModel;
            name = phone.name;
            parentId = phone.parentId;
        }
    }



}
