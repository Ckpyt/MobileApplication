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
        TreeNode selectedNode = null;
        DataType selectedType = DataType.none;

        int lastPhoneId = 0;
        int lastOperationId = 0;
        int lastFunctionId = 0;


        public ObjectsPage()
        {
            InitializeComponent();
            FillTables();
        }

        /// <summary>
        /// seaching phone in the TreeView by id
        /// </summary>
        /// <param name="id"> phone id </param>
        /// <param name="itm"> current node </param>
        /// <returns> phone's node </returns>
        TreeNode FindNodeByIdInNode(int id, TreeNode itm)
        {
            foreach (TreeNode node in itm.Nodes)
            {
                if (node.Tag != null && (int)node.Tag == id)
                    return node;
                if(node.Nodes != null)
                {
                    var newNode = FindNodeByIdInNode(id, node);
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
            return id == 0 ? phones : FindNodeByIdInNode(id, phones);
        }

        /// <summary>
        /// add a phone into treeVeiw and parent combobox
        /// </summary>
        /// <param name="model"> phone </param>
        void AddPhone(PhoneModel model)
        {
            parentPhoneBox.Items.Add(model.name);
            var parent = FindNodeById(model.parentId);

            TreeNode node = new TreeNode(model.name);
            node.Tag = model.id;
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

            operationNameBox.Items.Add(func.name);
            var node = operations.Nodes.Add(func.name);
            node.Tag = func.id;
            
            
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
            foreach(TreeNode node in phone.Nodes)
                if(node.Text == "Operations")
                {
                    oper = node;
                    break;
                }

            if (oper == null)
                oper = phone.Nodes.Add("Operations");


            Function func = null;
            foreach(Function fnc in allFunctions)
                if(fnc.id == op.functionID)
                {
                    func = fnc;
                }
            
            var nodeOp = oper.Nodes.Add(func.name);
            nodeOp.Tag = op.id;

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
            foreach(var phone in allPhones)
                AddPhone(phone);

            //fill operations
            allOperations = SQLWorker.GetInstance().ReadOperations();
            foreach (Operation op in allOperations)
                AddOperation(op);

            treeView1.ExpandAll();
        }

        void AddNewFunction()
        {
            if (operationNameBox.Text.Length == 0)
            {
                MessageBox.Show("Sorry, operation or phone model should be typed",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if(operationPriceBox.Text.Length == 0)
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

        void AddNewOperation(int parent)
        {
            if (parent == 0)
                return;

            int opId = 0;
            string opName = operationNameBox.Text; //it could be changed
            if (operationNameBox.SelectedIndex >= 0)
            {
                opName = (string)operationNameBox.Items[operationNameBox.SelectedIndex];
            }

            foreach (TreeNode node in operations.Nodes)
            {
                if (node.Text == opName)
                {
                    opId = (int)node.Tag;
                    break;
                }
            }

            if ( opId == 0)
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

        void AddNewObject()
        {
            int parent = 0;
            if (parentPhoneBox.Text.Length != 0)
            {
                foreach (var phone in allPhones)
                    if (phone.name == parentPhoneBox.Text)
                    {
                        parent = phone.id;
                        break;
                    }
            }

            if (phoneNameBox.TextLength == 0 && parent == 0)
            {
                AddNewFunction();
            }
            else if (operationNameBox.Text.Length == 0)
            {
                AddNewPhone(parent);
            }
            else
            {
                AddNewOperation(parent);
            }
        }

        void EditFunction()
        {

        }

        void EditPhoneModel()
        {

        }

        void EditOperation()
        {

        }

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
            if(selectedNode != null)
            {
                EditObject();
            }else
            {
                AddNewObject();
            }

            phoneNameBox.Text = "";
            operationNameBox.Text = "";
            operationPriceBox.Text = "";
            parentPhoneBox.Text = "";
            addButton.Text = "Add";
            selectedType = DataType.none;
            selectedNode = null;
        }

        private void OperationPriceBox_TextChanged(object sender, EventArgs e)
        {
            MakeInvoicePage.OnlyDecimalCheckerStatic(sender, e);
        }

        private void OperationNameBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(selectedType == DataType.operation && operationNameBox.Text != selectedNode.Text)
            {
                selectedType = DataType.none;
                addButton.Text = "Add";
                selectedNode = null;
            }
        }

        TreeNode FindParent(TreeNode current)
        {
            return current.Parent != null ? FindParent(current.Parent) : current;
        }

        private void TreeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            selectedNode = treeView1.SelectedNode;

            selectedType = DataType.none;
            addButton.Text = "Add";

            if (treeView1.SelectedNode == null)
            {
                return;
            }

            phoneNameBox.Text = "";
            operationNameBox.Text = "";
            operationPriceBox.Text = "";
            parentPhoneBox.SelectedIndex = -1;

            var parentRoot = FindParent(selectedNode);
            if (parentRoot == selectedNode || selectedNode.Text == "Operations") return;

            addButton.Text = "Edit";

            if(parentRoot.Text == "Operations")
            {//it is function
                foreach(Function func in allFunctions)
                    if(func.id == (int)selectedNode.Tag)
                    {
                        operationPriceBox.Text = ((float)func.price / 100.0f).ToString();
                        operationNameBox.Text = func.name;
                        break;
                    }
                selectedType = DataType.function;

            }
            else if(parentRoot.Text == "Phones")
            {
                if(selectedNode.Parent.Text == "Operations")
                {
                    //it is operation
                    selectedType = DataType.operation;
                    if(selectedNode.Parent.Parent != parentRoot)
                        parentPhoneBox.Text = selectedNode.Parent.Parent.Text;
                    operationNameBox.Text = selectedNode.Text;

                    foreach(Operation op in allOperations)
                        if(op.id == (int)selectedNode.Tag)
                        {
                            operationPriceBox.Text = ((float)op.price / 100.0f).ToString();
                            break;
                        }
                }
                else
                {
                    //it is phone model
                    selectedType = DataType.phoneModel;
                    if(selectedNode.Parent != parentRoot)
                        parentPhoneBox.Text = selectedNode.Parent.Text;
                    phoneNameBox.Text = selectedNode.Text;
                }
            }

        }
    }
}
