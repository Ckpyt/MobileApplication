using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Office.Interop.Word;
using word = Microsoft.Office.Interop.Word;

namespace MobileApplication
{
    /// <summary>
    /// The page for making invoices. Keywords should be get from database
    /// Result should be saved to database and invoice.pdf
    /// Later, parent class should be changed to tabPage
    /// </summary>
    public partial class MakeInvoicePage :
#if DEBUG
        Form
#else
        TabPage
#endif
    {

        bool isSelected = false;
        int selectedPos = -1;

        List<Function> functions;
        List<PhoneModel> phoneModels;
        List<Operation> operations;

        public static ObjectsPage objectsPage = null;

        public MakeInvoicePage()
        {
            InitializeComponent();
            qtyBox.Text = "1";
            priceBox.Text = "0";

            priceBox.Enabled = MainForm.currentUser.GetStringRights().Contains('P');

            FillBoxes();   
        }

        void FillBoxes()
        {
            deleteButton.Visible = false;

            functions = SQLWorker.GetInstance().ReadFunctions();
            phoneModels = SQLWorker.GetInstance().ReadPhones();
            operations = SQLWorker.GetInstance().ReadOperations();

            deviceBox.DataSource = phoneModels;
            descriptionBox.DataSource = functions;

            if (objectsPage != null)
            {
                objectsPage.addOperationEvent += delegate (Operation op) { operations.Add(op); };
                objectsPage.addFunctionEvent += delegate (Function func)
                {
                    descriptionBox.DataSource = null;
                    functions.Add(func);
                    descriptionBox.DataSource = functions;
                };
                objectsPage.addPhoneModelEvent += delegate (PhoneModel phone)
                {
                    deviceBox.DataSource = null;
                    phoneModels.Add(phone);
                    deviceBox.DataSource = phoneModels;
                };

                objectsPage.changeFunctionEvent += delegate (Function func) 
                {
                    ChangeObject(functions, func, descriptionBox);
                };

                objectsPage.changePhoneModelEvent += delegate (PhoneModel phone)
                {
                    ChangeObject(phoneModels, phone, deviceBox);
                };

                objectsPage.changeOperationEvent += delegate (Operation op)
                 {
                     ChangeObject(operations, op);
                 };

                objectsPage.deleteFunctionEvent += delegate (Function func)
                {
                    DeleteObject(functions, func, descriptionBox);
                };

                objectsPage.deletePhoneModelEvent += delegate (PhoneModel phone)
                {
                    DeleteObject(phoneModels, phone, deviceBox);
                };

                objectsPage.deleteOperationEvent += delegate (Operation op)
                {
                    DeleteObject(operations, op);
                };
            }
        }

        private void ChangeObject<T>(List<T> container, DataObject objct)
        {
            foreach(T obj in container)
            {
                DataObject dtobj = obj as DataObject;
                if (dtobj.id == objct.id)
                    dtobj.CopyFrom(objct);
            }
        }

        public void ChangeObject<T>(List<T> container, DataObject objct, ComboBox box)
        {
            box.DataSource = null;
            ChangeObject(container, objct);
            box.DataSource = container;
        }

        private void DeleteObject<T>(List<T> container, DataObject dataObject)
        {
            foreach(T obj in container)
            {
                DataObject dtobj = obj as DataObject;
                if (dtobj.id == dataObject.id)
                {
                    container.Remove(obj);
                    return;
                }
            }
        }

        private void DeleteObject<T>(List<T> container, DataObject dataObject, ComboBox box)
        {
            box.DataSource = null;
            DeleteObject(container, dataObject);
            box.DataSource = container;
        }



        private void Add_Click(object sender, EventArgs e)
        {
            ListViewItem itm = new ListViewItem(deviceBox.Text);
            var itms = itm.SubItems;
            itm.SubItems.Add(descriptionBox.Text);
            itm.SubItems.Add(qtyBox.Text);
            itm.SubItems.Add(priceBox.Text);

            if (qtyBox.Text.Length > 0 && priceBox.Text.Length > 0)
                itm.SubItems.Add((Int32.Parse(qtyBox.Text) * float.Parse(priceBox.Text)).ToString());
            else
                itm.SubItems.Add("0");

            if (isSelected)
            {
                addButton.Text = "Add";
                listView1.Items[selectedPos] = itm;
                isSelected = false;
            }
            else
            {
                listView1.Items.Add(itm);
            }

            deviceBox.Text = "";
            descriptionBox.Text = "";
            qtyBox.Text = "1";
            priceBox.Text = "0";
            deleteButton.Visible = false;
        }

        private void OnlyDecimalChecker(object sender, EventArgs e)
        {
            OnlyDecimalCheckerStatic(sender, e);
        }

        private void OnlyFloatChecker(object sender, EventArgs e)
        {
            OnlyFloatCheckerStatic(sender, e);
        }

        public static void OnlyFloatCheckerStatic(object sender, EventArgs e)
        {
            TextBox sd = sender as TextBox;
            if (sd.Text.Length > 0 && sd.Text[sd.Text.Length - 1] == '.') return;
            OnlyDecimalCheckerStatic(sender, e);
        }

        public static void OnlyDecimalCheckerStatic(object sender, EventArgs e)
        {
            TextBox sd = sender as TextBox;
            string txt = "";
            int pos = sd.SelectionStart;

            foreach (char t in sd.Text)
                if ((t >= '0' && t <= '9') || t=='.' )
                    txt += t;
                else
                    pos--;

            if (txt.Length > 0)
            {
                try
                {
                    double tmp = double.Parse(txt);
                    sd.Text = tmp.ToString();
                }
                catch(Exception ex)
                {
                   
                }
                sd.SelectionStart = pos;
                sd.SelectionLength = 0;
            }
            else
            {
                sd.Text = "0";
            }
        }

        private void ListView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListView lst = sender as ListView;

            if (lst.SelectedItems.Count > 0)
            {
                isSelected = true;
                selectedPos = lst.SelectedIndices[0];

                var itm = lst.SelectedItems[0];
                deviceBox.Text = itm.Text;
                descriptionBox.Text = itm.SubItems[1].Text;
                qtyBox.Text = itm.SubItems[2].Text;
                priceBox.Text = itm.SubItems[3].Text;
                addButton.Text = "Edit";
                deleteButton.Visible = true;
            }
            else
            {
                deleteButton.Visible = false;
                addButton.Text = "Add";
                isSelected = false;
            }

        }

        private void MakeInvoicePage_ResizeEnd(object sender, EventArgs e)
        {
            int Height = this.Height - 140;
            listView1.Height = Height;
        }

        private void DeviceBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            PhoneModel phone = deviceBox.SelectedItem as PhoneModel;
            Function func = descriptionBox.SelectedItem as Function;
            if(func != null)
            {
                float price = (float)func.price / 100;
                
                if(phone != null)
                {
                    foreach(Operation op in operations)
                        if(op.deviceID == phone.id && op.functionID == func.id)
                            price = (float)op.price / 100;
                }
                priceBox.Text = ((price).ToString());
            }
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {

            foreach (ListViewItem itm in listView1.SelectedItems)
                listView1.Items.Remove(itm);

            deleteButton.Visible = false;
        }

        void FindAndReplace(word.Application app, string textToFind, string textToReplace)
        {
            var matchCase = true;
            var matchWholeWord = true;
            var matchWildcards = false;
            var matchSoundsLike = false;
            var matchAllWordForms = false;
            var forward = true;
            var wrap = 1;
            var format = false;
            var replace = 2;
            try
            {
                app.Selection.Find.Execute(
                    textToFind,
                    matchCase,
                    matchWholeWord,
                    matchWildcards,
                    matchSoundsLike,
                    matchAllWordForms,
                    forward,
                    wrap,
                    format,
                    textToReplace,
                    replace);
            }catch(Exception ex)
            {
                int i = 0;
            }
        }

        void AddDeviceNames(word.Application app)
        {
            string deviceNames = "";
            List<string> devices = new List<string>();
            foreach (ListViewItem itm in listView1.Items)
            {
                string str = itm.Text;
                bool exists = false;
                foreach (string device in devices)
                    exists |= str == device;

                if (!exists)
                    devices.Add(str);
            }
            bool first = true;
            foreach (string str in devices)
                if (first)
                {
                    deviceNames = str;
                    first = false;
                }
                else
                    deviceNames += ", " + str;

            FindAndReplace(app, "Device :", "Device : " + deviceNames);
        }

        void AddDescription(word.Application app, Document doc)
        {
            Table dataTbl = null;
            foreach(Table tbl in doc.Tables)
            {
                Range rng = tbl.Range;
                word.Cell cell = tbl.Cell(1, 1);
                word.Cell cell2 = tbl.Cell(1, 2);

                if (cell.Range.Text.Contains("Device") && cell2.Range.Text.Contains("Description"))
                    dataTbl = tbl;
            }

            int count = listView1.Items.Count;
            float tblHeight = 0;
            for (int i = 1; i < dataTbl.Rows.Count; i++)
            {
                Row row = dataTbl.Rows[i];
                tblHeight += row.Height;
            }

            float rowHeight = tblHeight / count;

            if(count >= dataTbl.Rows.Count)
            {
                for (int i = dataTbl.Rows.Count; i <= count; i++)
                    dataTbl.Rows.Add(dataTbl.Rows[2]);
            }
            else
            {
                for (int i = dataTbl.Rows.Count; i > count + 1; i--)
                    dataTbl.Rows[i].Delete();
            }

            for (int i = 0; i < listView1.Items.Count; i++)
            {
                dataTbl.Rows[i + 2].Height = rowHeight;
                ListViewItem itm = listView1.Items[i];
                Cell device = dataTbl.Cell(i + 2, 1);
                device.Range.Text = itm.Text;
                dataTbl.Cell(i + 2, 2).Range.Text = itm.SubItems[1].Text;
                dataTbl.Cell(i + 2, 3).Range.Text = itm.SubItems[2].Text;
                dataTbl.Cell(i + 2, 4).Range.Text = itm.SubItems[3].Text;
                dataTbl.Cell(i + 2, 5).Range.Text = itm.SubItems[4].Text;

            }
        }

        void SaveSumm(word.Application app)
        {
            float total = 0;

            foreach (ListViewItem itm in listView1.Items)
                total += float.Parse(itm.SubItems[4].Text);

            int grd = (int)(total * 100f);
            grd = (grd * 15 / 100) + grd;

            FindAndReplace(app, "TotalSum", total.ToString() + "$");
            FindAndReplace(app, "GRDSum", ((float)grd / 100f).ToString() + "$");
        }

        private void SaveInvoiceButton_Click(object sender, EventArgs e)
        {
            try
            {
                string path = Directory.GetCurrentDirectory();
                string pdfPath = path + "\\" + InvoiceBox.Text + ".pdf";
                string tempPath = path + "\\" + "Invoice templete.docx";
                string tempBlockPath = path + "\\" + "~$voice templete.docx";

                if (File.Exists(tempBlockPath))
                    File.Delete(tempBlockPath);


                word.Application app = new word.Application();
                Document doc = app.Documents.Open(tempPath);

                FindAndReplace(app, "Name:", "Name: " + CustNameBox.Text);
                FindAndReplace(app, "Contact No :", "Contact No : " + CustPhoneBox.Text);
                FindAndReplace(app, "13. 7. 2019", DateTime.Now.ToString("dd. MM. yyyy"));
                FindAndReplace(app, "DateText", "000111");
                FindAndReplace(app, "InvoiceNum", InvoiceBox.Text);
                AddDeviceNames(app);
                AddDescription(app, doc);
                SaveSumm(app);

                doc.SaveAs2(pdfPath, word.WdSaveFormat.wdFormatPDF);
                doc.Close(false);
                app.Quit(false);
            }catch(Exception ex)
            {
                MessageBox.Show("Sorry, I couldn't save a file, becouse:\n" + ex.Message, "Word error happens", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            MessageBox.Show("File saved", "File saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
