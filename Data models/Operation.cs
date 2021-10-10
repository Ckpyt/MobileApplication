
namespace MobileApplication
{
    /// <summary>
    /// Specified functions to phone model. Like "change a battery on iphone 6"
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

