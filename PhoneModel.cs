using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    /// Parent class for all the objects. Has only id and base virtual methods
    /// </summary>
    public class DataObject
    {
        /// <summary> id in the database </summary>
        public int id;

        /// <summary>
        /// virtual method for copying objects
        /// </summary>
        /// <param name="obj">source</param>
        public virtual void CopyFrom(DataObject obj) { }
    }

    /// <summary>
    /// Description of a phome model
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

    /// <summary>
    /// Fixing functions, like "change a battary"
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

    /// <summary>
    /// Specified functions to phone model. Like "change a battary on iphone 6"
    /// </summary>
    public class Operation : DataObject
    {
        /// <summary> device id in the database </summary>
        public int deviceID;
        /// <summary> function id in the database </summary>
        public int functionID;
        /// <summary> default price in cents </summary>
        public int price;


        /// <summary>
        /// virtual method for copying objects
        /// </summary>
        /// <param name="obj">source</param>
        public override void CopyFrom(DataObject obj)
        {
            Operation op = obj as Operation;
            deviceID = op.deviceID;
            functionID = op.functionID;
            price = op.price;
        }
    }

}
