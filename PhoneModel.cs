using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileApplication
{
    enum DataType
    {
        none,
        phoneModel,
        function,
        operation
    }


    public class DataObject
    {
        /// <summary> id in the database </summary>
        public int id;
    }

    /// <summary>
    /// Description of a phome model
    /// </summary>
    class PhoneModel : DataObject
    {
        /// <summary> Phone name </summary>
        public string name;
        /// <summary> Parent id. like iphone 6s --> iphone 6 --> iphone </summary>
        public int parentId;

        public override string ToString()
        {
            return name;
        }
    }

    /// <summary>
    /// Fixing functions, like "change a battary"
    /// </summary>
    class Function : DataObject
    {
        /// <summary> Function description like "change a glass" </summary>
        public string name;
        /// <summary> default price in cents </summary>
        public int price;

        public override string ToString()
        {
            return name;
        }
    }

    /// <summary>
    /// Specified functions to phone model. Like "change a battary on iphone 6"
    /// </summary>
    class Operation : DataObject
    {
        /// <summary> device id in the database </summary>
        public int deviceID;
        /// <summary> function id in the database </summary>
        public int functionID;
        /// <summary> default price in cents </summary>
        public int price;
    }

}
