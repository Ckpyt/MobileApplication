
namespace MobileApplication
{
    /// <summary>
    /// Non-specified fixing functions, like "change a battery".
    /// </summary>
    public class Function : DataObject
    {
        /// <summary> Function description like "change a glass" </summary>
        public string name;
        /// <summary> default price in cents </summary>
        public int price;

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
            Function func = obj as Function;
            name = func.name;
            price = func.price;
        }
    }
}
