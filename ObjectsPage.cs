using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MobileApplication
{
    public partial class ObjectsPage : Form
    {
        /// <summary> container for holding phones </summary>
        List<PhoneModel> allPhones;
        /// <summary> container for holding Operations </summary>
        List<Operation> allOperations;
        /// <summary> container for holding Functions </summary>
        List<Function> allFunctions;
        /// <summary> root node of phones </summary>
        TreeNode phones;
        /// <summary> root node of operations </summary>
        TreeNode operations;
        /// <summary> selected node. If nothing was selected == null </summary>
        TreeNode selectedNode = null;
        /// <summary> type of object in selected node. Object holds in Tag  </summary>
        DataType selectedType = DataType.none;


        int lastPhoneId = 0;
        int lastOperationId = 0;
        int lastFunctionId = 0;


        public ObjectsPage()
        {
            InitializeComponent();
            FillTables();
            deleteButton.Visible = false;
        }

        /// <summary>
        /// seaching phone in the TreeView by id
        /// </summary>
        /// <param name="id"> phone id </param>
        /// <param name="itm"> current node </param>
        /// <returns> phone's node </returns>
        TreeNode FindNodeByIdInNode(int id, TreeNode itm, Type type)
        {
            foreach (TreeNode node in itm.Nodes)
            {
                if (node.Tag != null && ((DataObject)node.Tag).id == id && node.Tag.GetType() == type)
                    return node;

                if (node.Nodes != null)
                {
                    var newNode = FindNodeByIdInNode(id, node, type);
                    if (newNode != null)
                        return newNode;
                }
            }
            return null;
        }

        /// <summary>
        /// seaching phone in the TreeView by id
        /// </summary>
        /// <param name="id"> phone id </param>
        /// <returns> phone's node </returns>
        TreeNode FindNodeById(int id)
        {
            return id == 0 ? phones : FindNodeByIdInNode(id, phones, typeof(PhoneModel));
        }

        /// <summary>
        /// add a phone into treeVeiw and parent combobox
        /// </summary>
        /// <param name="model"> phone </param>
        void AddPhone(PhoneModel model)
        {
            parentPhoneBox.Items.Add(model);
            var parent = FindNodeById(model.parentId);

            TreeNode node = new TreeNode(model.name);
            node.Tag = model;
            parent.Nodes.Add(node);
            lastPhoneId = model.id > lastPhoneId ? model.id : lastPhoneId;
        }

        /// <summary>
        /// add operation into treeview and operation combobox
        /// </summary>
        /// <param name="func"></param>
        void AddFunction(Function func)
        {
            lastFunctionId = func.id > lastFunctionId ? func.id : lastFunctionId;

            operationNameBox.Items.Add(func);
            var node = operations.Nodes.Add(func.name);
            node.Tag = func;
        }

        /// <summary>
        /// add operation into treeview
        /// </summary>
        /// <param name="op"></param>
        void AddOperation(Operation op)
        {
            TreeNode phone = FindNodeById(op.deviceID);
            if (phone == null) return;

            TreeNode oper = null;
            foreach (TreeNode node in phone.Nodes)
                if (node.Text == "Operations")
                {
                    oper = node;
                    break;
                }

            if (oper == null)
                oper = phone.Nodes.Add("Operations");


            Function func = null;
            foreach (Function fnc in allFunctions)
                if (fnc.id == op.functionID)
                {
                    func = fnc;
                }

            var nodeOp = oper.Nodes.Add(func.name);
            nodeOp.Tag = op;

            lastOperationId = lastOperationId > op.id ? lastOperationId : op.id;

        }

        /// <summary>
        /// Prepearing. Filling comboboses and treeView
        /// </summary>
        void FillTables()
        {
            //fill functions
            allFunctions = SQLWorker.GetInstance().ReadFunctions();
            operations = treeView1.Nodes.Add("Operations");
            foreach (Function func in allFunctions)
                AddFunction(func);

            //fill phones
            allPhones = SQLWorker.GetInstance().ReadPhones();
            phones = treeView1.Nodes.Add("Phones");
            foreach (var phone in allPhones)
                AddPhone(phone);

            //fill operations
            allOperations = SQLWorker.GetInstance().ReadOperations();
            foreach (Operation op in allOperations)
                AddOperation(op);

            treeView1.ExpandAll();
        }

        /// <summary>
        /// Add new function from the form.
        /// Also, save it in the database
        /// </summary>
        void AddNewFunction()
        {
            if (operationNameBox.Text.Length == 0)
            {
                MessageBox.Show("Sorry, operation or phone model should be typed",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (operationPriceBox.Text.Length == 0)
            {
                MessageBox.Show("Sorry, price of operation should be typed",
                   "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Function func = new Function();
            func.id = lastFunctionId + 1;
            func.name = operationNameBox.Text;
            func.price = (int)(float.Parse(operationPriceBox.Text) * 100.0f);

            AddFunction(func);
            allFunctions.Add(func);
            SQLWorker.GetInstance().SqlComm("insert into tblFunctions values(" + func.id + ",'" +
            func.name + "'," + func.price + ");");
        }

        /// <summary>
        /// Add new phone from the form.
        /// Also, save it in the database
        /// </summary>
        /// <param name="parent"> parent phone </param>
        void AddNewPhone(int parent)
        {
            PhoneModel model = new PhoneModel();
            model.id = lastPhoneId + 1;
            model.parentId = parent;
            model.name = phoneNameBox.Text;
            SQLWorker.GetInstance().SqlComm("insert into tblPhoneModels values("
                    + model.id + ",'" + model.name + "'," + model.parentId + ")");
            AddPhone(model);
        }

        /// <summary>
        /// Add new operation from the form.
        /// Also, save it in the database
        /// </summary>
        /// <param name="parent">parent phone</param>
        void AddNewOperation(int parent)
        {
            if (parent == 0)
                return;

            int opId = 0;
            string opName = operationNameBox.Text; //it could be changed
            Function basefnc;

            if (operationNameBox.SelectedIndex >= 0 )
            {
                basefnc = (Function)operationNameBox.SelectedItem;
                opId = basefnc.id;
            }
            else
            {
                foreach (TreeNode node in operations.Nodes)
                {
                    if (node.Text == opName)
                    {
                        basefnc = node.Tag as Function;
                        opId = basefnc.id;
                        break;
                    }
                }
            }

            if (opId == 0)
            {
                AddNewFunction();
                AddNewOperation(parent);
                return;
            }

            Operation op = new Operation();
            op.id = lastOperationId + 1;
            op.deviceID = parent;
            op.functionID = opId;
            op.price = (int)(float.Parse(operationPriceBox.Text) * 100.0f);

            SQLWorker.GetInstance().SqlComm("insert into tblOperations values("
             + op.id + "," + op.deviceID + "," + op.functionID + "," + op.price + ")");

            AddOperation(op);
            allOperations.Add(op);
        }

        /// <summary>
        /// add new object. All the data god from form's fields
        /// </summary>
        void AddNewObject()
        {
            int parentId = 0;

            if (parentPhoneBox.SelectedIndex >= 0)
            {
                PhoneModel parent = parentPhoneBox.SelectedItem as PhoneModel;
                parentId = parent.id;
            }

            if (phoneNameBox.TextLength == 0 && parentId == 0)
            {
                AddNewFunction();
            }
            else if (operationNameBox.Text.Length == 0)
            {
                AddNewPhone(parentId);
            }
            else
            {
                AddNewOperation(parentId);
            }
        }

        /// <summary>
        /// Set description of operation the same as it holds in the function.
        /// It should be called after function description was changed
        /// </summary>
        /// <param name="tree"> node there is looking the operations </param>
        /// <param name="func"> parent function </param>
        void ChangeOperationDescription(TreeNode tree, Function func)
        {
            foreach(TreeNode nd in tree.Nodes)
            {
                if (nd.Text == "Operations")
                {
                    foreach (TreeNode opNode in nd.Nodes)
                        if ((opNode.Tag as Operation).functionID == func.id)
                            opNode.Text = func.name;
                }
                else
                    ChangeOperationDescription(nd, func);
            }


        }

        /// <summary>
        /// Change function description and default price
        /// Also, defaily description will be changed in the all operations
        /// </summary>
        void EditFunction()
        {
            Function func = selectedNode.Tag as Function;
            func.name = operationNameBox.Text;
            func.price = (int)(float.Parse(operationPriceBox.Text) * 100.0f);
            selectedNode.Text = func.name;

            ChangeOperationDescription(phones, func);

            SQLWorker.GetInstance().SqlComm("update tblFunctions set Name='" + func.name +
                "', Price=" + func.price + " where Id=" + func.id + "; ");
        }

        /// <summary>
        /// Change phone's name and parent model
        /// </summary>
        void EditPhoneModel()
        {
            PhoneModel phone = selectedNode.Tag as PhoneModel;
            phone.name = phoneNameBox.Text;

            var node = selectedNode;

            PhoneModel parentModel = (parentPhoneBox.SelectedItem as PhoneModel);
            if (parentModel.id != phone.parentId)
            {
                var parentNode = FindNodeById(parentModel.id);
                if (parentNode != node)
                {
                    selectedNode.Parent.Nodes.Remove(node);
                    phone.parentId = parentModel.id;
                    parentNode.Nodes.Add(node);
                }
                else
                    return;
            }

            node.Text = phone.name;

            SQLWorker.GetInstance().SqlComm("update tblPhoneModels set Name='" + phone.name + "' " +
                (phone.parentId > 0 ? ", ParentId=" + phone.parentId.ToString() : " ") +
                " where Id=" + phone.id + "; ");

        }

        /// <summary>
        /// change operation phone model and price 
        /// </summary>
        void EditOperation()
        {
            Operation op = selectedNode.Tag as Operation;
            op.price = (int)(float.Parse(operationPriceBox.Text) * 100.0f);

            PhoneModel parentModel = (parentPhoneBox.SelectedItem as PhoneModel);
            if (op.deviceID != parentModel.id)
            {
                op.deviceID = parentModel.id;
                var parNode = FindNodeById(op.deviceID);

                var node = selectedNode;
                selectedNode.Parent.Nodes.Remove(node);

                TreeNode oper = null;
                foreach (TreeNode opNode in parNode.Nodes)
                    if (opNode.Text == "Operations")
                    {
                        oper = opNode;
                        break;
                    }

                if (oper == null)
                    oper = parNode.Nodes.Add("Operations");

                oper.Nodes.Add(node);

            }

            Function func = operationNameBox.SelectedItem as Function;
            if (op.functionID != func.id)
            {
                op.functionID = func.id;
                selectedNode.Text = func.name;
            }

            phoneNameBox.Enabled = true; ;
            operationNameBox.DropDownStyle = ComboBoxStyle.DropDown;

            SQLWorker.GetInstance().SqlComm("update tblOperations set DeviceId=" + op.deviceID +
                ", FunctionId=" + op.functionID +
                ", Price=" + op.price + " where Id=" + op.id + "; ");

        }

        /// <summary>
        /// Editing selected node
        /// </summary>
        void EditObject()
        {
            switch (selectedType)
            {
                case DataType.function:
                    EditFunction();
                    break;
                case DataType.phoneModel:
                    EditPhoneModel();
                    break;
                case DataType.operation:
                    EditOperation();
                    break;
            }
        }


        private void AddButton_Click(object sender, EventArgs e)
        {
            if (selectedNode != null)
            {
                EditObject();
            }
            else
            {
                AddNewObject();
            }

            ClearFormBoxes();
        }

        private void OperationPriceBox_TextChanged(object sender, EventArgs e)
        {
            MakeInvoicePage.OnlyDecimalCheckerStatic(sender, e);
        }

        private void OperationNameBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (selectedType == DataType.operation && operationNameBox.Text != selectedNode.Text)
            {
                selectedType = DataType.none;
                addButton.Text = "Add";
                selectedNode = null;
                deleteButton.Visible = false;
            }
            Function func = operationNameBox.SelectedItem as Function;
            operationPriceBox.Text = ((float)func.price/100).ToString();
        }

        /// <summary>
        /// Find the first node of the tree
        /// </summary>
        /// <param name="current"> started node </param>
        /// <returns>first node</returns>
        TreeNode FindParent(TreeNode current)
        {
            return current.Parent != null ? FindParent(current.Parent) : current;
        }

        /// <summary>
        /// Set default values to boxes
        /// </summary>
        void ClearFormBoxes()
        {
            deleteButton.Visible = false;

            selectedType = DataType.none;
            addButton.Text = "Add";
            selectedNode = null;


            operationNameBox.Enabled = true;
            phoneNameBox.Enabled = true;
            operationNameBox.DropDownStyle = ComboBoxStyle.DropDown;
            operationPriceBox.Enabled = true;
            parentPhoneBox.Enabled = true;

            phoneNameBox.Text = "";
            operationNameBox.Text = "";
            operationPriceBox.Text = "";
            parentPhoneBox.SelectedIndex = -1;

            treeView1.ExpandAll();
        }

        private void TreeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            //preparations and clearing

            ClearFormBoxes();

            if (treeView1.SelectedNode == null)
                return;

            selectedNode = treeView1.SelectedNode;
            var parentRoot = FindParent(selectedNode);
            if (parentRoot == selectedNode || selectedNode.Text == "Operations")
            {
                selectedNode = null;
                return;
            }

            deleteButton.Visible = true;
            addButton.Text = "Edit";

            if (parentRoot.Text == "Operations")
            {//it is function
                Function func = selectedNode.Tag as Function;

                operationPriceBox.Text = ((float)func.price / 100.0f).ToString();
                operationNameBox.Text = func.name;

                selectedType = DataType.function;

                phoneNameBox.Enabled = false;
                parentPhoneBox.Enabled = false;

            }
            else if (parentRoot.Text == "Phones")
            {
                if (selectedNode.Parent.Text == "Operations")
                {
                    //it is operation
                    selectedType = DataType.operation;
                    if (selectedNode.Parent.Parent != parentRoot)
                        parentPhoneBox.Text = selectedNode.Parent.Parent.Text;
                    operationNameBox.Text = selectedNode.Text;

                    Operation op = selectedNode.Tag as Operation;

                    operationPriceBox.Text = ((float)op.price / 100.0f).ToString();

                    phoneNameBox.Enabled = false;
                    operationNameBox.DropDownStyle = ComboBoxStyle.DropDownList;
                }
                else
                {
                    //it is phone model
                    selectedType = DataType.phoneModel;
                    if (selectedNode.Parent != parentRoot)
                        parentPhoneBox.Text = selectedNode.Parent.Text;
                    phoneNameBox.Text = selectedNode.Text;
                    operationNameBox.Enabled = false;
                    operationPriceBox.Enabled = false;
                }
            }
        }

        private void ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var msev = treeView1.PointToClient(Cursor.Position);
            treeView1.SelectedNode = treeView1.GetNodeAt(msev.X, msev.Y - 5);
        }

        private void DeselectNodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            selectedNode = null;
            ClearFormBoxes();
        }

        /// <summary>
        /// delete current function from all the phones
        /// </summary>
        /// <param name="parNode"> start node </param>
        /// <param name="func"> parent's function </param>
        void DeleteFunctionFromPhone(TreeNode parNode, Function func)
        {
            foreach (TreeNode node in parNode.Nodes)
            {
                //it could be only operations or phone
                if (node.Text == "Operations")
                {
                    List<TreeNode> remNodes = new List<TreeNode>();

                    //find current operation
                    foreach (TreeNode opNode in node.Nodes)
                    {
                        Operation op = opNode.Tag as Operation;
                        if (op != null && op.functionID == func.id)
                        {
                            DeleteOperation(opNode);
                            remNodes.Add(opNode);
                        }
                    }

                    foreach (TreeNode nd in remNodes)
                        node.Nodes.Remove(nd);
                }
                else
                {
                    DeleteFunctionFromPhone(node, func);
                }
            }
        }

        /// <summary>
        /// delete selected function
        /// </summary>
        void DeleteFunction()
        {
            Function func = selectedNode.Tag as Function;
            DeleteFunctionFromPhone(phones, func);

            SQLWorker.GetInstance().SqlComm("delete from tblFunctions where id=" + func.id);
            operationNameBox.Items.Remove(func);
        }

        /// <summary>
        /// delete operation
        /// </summary>
        /// <param name="opNode"> target operation </param>
        void DeleteOperation(TreeNode opNode)
        {
            Operation op = opNode.Tag as Operation;
            SQLWorker.GetInstance().SqlComm("delete from tblOperations where id=" + op.id + ";");   
        }

        /// <summary>
        /// delete phone and all the childrens and operations
        /// </summary>
        /// <param name="phoneNode"> target phone node </param>
        void DeletePhone(TreeNode phoneNode)
        {
            foreach(TreeNode node in phoneNode.Nodes)
            {
                if(node.Text == "Operations")
                {
                    List<TreeNode> rem = new List<TreeNode>();
                    foreach (TreeNode opNode in node.Nodes)
                    {
                        DeleteOperation(opNode);
                        rem.Add(opNode);
                    }

                    foreach (TreeNode nds in rem)
                        node.Nodes.Remove(nds);
                }
                else
                {
                    DeletePhone(node);
                }
            }

            phoneNode.Nodes.Clear();

            PhoneModel phone = phoneNode.Tag as PhoneModel;
            SQLWorker.GetInstance().SqlComm("delete from tblPhoneModels where id=" + phone.id);
            parentPhoneBox.Items.Remove(phone);
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            switch (selectedType)
            {
                case DataType.function:
                    DeleteFunction();
                    break;
                case DataType.operation:
                    DeleteOperation(selectedNode);
                    break;
                case DataType.phoneModel:
                    DeletePhone(selectedNode);
                    break;
            }
            selectedNode.Parent.Nodes.Remove(selectedNode);

            ClearFormBoxes();
        }

        private void ObjectsPage_SizeChanged(object sender, EventArgs e)
        {
            treeView1.Height = this.Height - 140;
        }
    }
}
