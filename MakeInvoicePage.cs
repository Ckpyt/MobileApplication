using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
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
#if DEBSYMB
        Form
#else
        TabPage
#endif
    {
        /// <summary> is invoice selected? </summary>
        bool isSelected = false;
        /// <summary> selected invoice position </summary>
        int selectedPos = -1;
        /// <summary> last invoice id </summary>
        int maxInvoice = 0;

        /// <summary> all the functions in the database. Source for description combobox </summary>
        List<Function> functions;
        /// <summary> all the phonemodel in the database. Source for phone combobox </summary>
        List<PhoneModel> phoneModels;
        /// <summary> all the operations. Used for storing specified price </summary>
        List<Operation> operations;

        /// <summary> Link to objectsPage. Used for making events </summary>
        public static ObjectsPage objectsPage = null;

        public MakeInvoicePage()
        {
            InitializeComponent();
            qtyBox.Text = "1";
            priceBox.Text = "0";

            priceBox.Enabled = MainForm.currentUser.GetStringRights().Contains('P');

            FillBoxes();
            maxInvoice = SQLWorker.GetInstance().GetMaxId("tblInvoices");
            InvoiceBox.Text = (maxInvoice + 1).ToString();
        }

        /// <summary>
        /// Fiiling comboboxes and making events
        /// </summary>
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

        /// <summary>
        /// part of events. Change phonemodel, function, operations
        /// </summary>
        /// <typeparam name="T">phonemodel, function, operations</typeparam>
        /// <param name="container">list of objects</param>
        /// <param name="dataObject"> source object </param>
        private void ChangeObject<T>(List<T> container, DataObject dataObject)
        {
            foreach(T obj in container)
            {
                DataObject dtobj = obj as DataObject;
                if (dtobj.id == dataObject.id)
                    dtobj.CopyFrom(dataObject);
            }
        }

        /// <summary>
        /// part of events. Change  phonemodel and function
        /// </summary>
        /// <typeparam name="T"> phonemodel or function </typeparam>
        /// <param name="container">list of objects </param>
        /// <param name="dataObject"> link to source </param>
        /// <param name="box"> combobox of phonemodel or function</param>
        public void ChangeObject<T>(List<T> container, DataObject dataObject, ComboBox box)
        {
            box.DataSource = null;
            ChangeObject(container, dataObject);
            box.DataSource = container;
        }

        /// <summary>
        /// delete object from list. Part of events. 
        /// used for deleting phonemodel, function, operations
        /// </summary>
        /// <typeparam name="T">phonemodel, function, operations</typeparam>
        /// <param name="container"> link to list </param>
        /// <param name="dataObject"> link to deleting object </param>
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

        /// <summary>
        /// delete object from list. Part of events. 
        /// used for deleting phonemodel, function, operations
        /// </summary>
        /// <typeparam name="T">phonemodel, function</typeparam>
        /// <param name="container"> link to list </param>
        /// <param name="dataObject"> link to deleting object </param>
        /// <param name="box"> combobox of phonemodel or function</param>
        private void DeleteObject<T>(List<T> container, DataObject dataObject, ComboBox box)
        {
            box.DataSource = null;
            DeleteObject(container, dataObject);
            box.DataSource = container;
        }

        /// <summary>
        /// add string to invoice form.
        /// </summary>
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

        /// <summary>
        /// used for checking entered values in textboxes
        /// </summary>
        private void OnlyDecimalChecker(object sender, EventArgs e)
        {
            OnlyDecimalCheckerStatic(sender, e);
        }

        /// <summary>
        /// used for checking entered values in textboxes
        /// </summary>
        private void OnlyFloatChecker(object sender, EventArgs e)
        {
            OnlyFloatCheckerStatic(sender, e);
        }

        /// <summary>
        /// used for checking entered values in textboxes
        /// Check float structure
        /// </summary>
        public static void OnlyFloatCheckerStatic(object sender, EventArgs e)
        {
            TextBox sd = sender as TextBox;
            if (sd.Text.Length > 0 && sd.Text[sd.Text.Length - 1] == '.') return;
            OnlyDecimalCheckerStatic(sender, e);
        }

        /// <summary>
        /// used for checking entered values in textboxes
        /// check decimal structure
        /// </summary>
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

        /// <summary>
        /// select a string for editing into invoice list view
        /// </summary>
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

        /// <summary>
        /// holding the same percentage of using space for listview1
        /// </summary>
        private void MakeInvoicePage_ResizeEnd(object sender, EventArgs e)
        {
            int Height = this.Height - 140;
            listView1.Height = Height;
        }

        /// <summary>
        /// looking for a price then device is changed
        /// </summary>
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

        /// <summary>
        /// delete string from invoice list view
        /// </summary>
        private void DeleteButton_Click(object sender, EventArgs e)
        {

            foreach (ListViewItem itm in listView1.SelectedItems)
                listView1.Items.Remove(itm);

            deleteButton.Visible = false;
        }

        /// <summary>
        /// Find and replace text everythere in word document
        /// </summary>
        /// <param name="doc"> link to document </param>
        /// <param name="Findtext"> source text </param>
        /// <param name="Replacetext"> replace text </param>
        void FindAndReplaceIntoTextBoxes(Document doc, string Findtext, string Replacetext)
        {
            Logger.GetInstance().SaveLog("FindAndReplaceIntoTextBoxes entered ");

            //First search the main document using the Selection
            {
                var myStoryRange = doc.Range();
                myStoryRange.Find.Text = Findtext;
                myStoryRange.Find.Replacement.Text = Replacetext;
                myStoryRange.Find.Forward = true;
                myStoryRange.Find.Wrap = word.WdFindWrap.wdFindContinue;
                myStoryRange.Find.Format = false;
                myStoryRange.Find.MatchCase = false;
                myStoryRange.Find.MatchWholeWord = false;
                myStoryRange.Find.MatchWildcards = false;
                myStoryRange.Find.MatchSoundsLike = false;
                myStoryRange.Find.MatchAllWordForms = false;

                if (myStoryRange.Find.Execute(Findtext, false, false, false, false, true, true, word.WdFindWrap.wdFindContinue
                    , false, Replacetext, word.WdReplace.wdReplaceAll))
                    return;
            }
            Logger.GetInstance().SaveLog("FindAndReplaceIntoTextBoxes 1 ");
            //Now search all other stories using Ranges
            {
                foreach (Range myStoryRange in doc.StoryRanges)
                {
                    if (myStoryRange.StoryType != word.WdStoryType.wdMainTextStory)
                    {
                        {
                            myStoryRange.Find.Text = Findtext;
                            myStoryRange.Find.Replacement.Text = Replacetext;
                            myStoryRange.Find.Wrap = word.WdFindWrap.wdFindContinue;
                            myStoryRange.Find.Execute(Findtext, false, false, false, false, true, true, word.WdFindWrap.wdFindContinue
                    , false, Replacetext, word.WdReplace.wdReplaceAll);
                        }
                        Range rng = myStoryRange;
                        while ((rng.NextStoryRange != null))
                        {
                            rng = rng.NextStoryRange;
                            {
                                rng.Find.Text = Findtext;
                                rng.Find.Replacement.Text = Replacetext;
                                rng.Find.Wrap = word.WdFindWrap.wdFindContinue;
                                rng.Find.Execute(Findtext, false, false, false, false, true, true, word.WdFindWrap.wdFindContinue
                    , false, Replacetext, word.WdReplace.wdReplaceAll);
                            }
                        }
                    }
                }
            }
            Logger.GetInstance().SaveLog("FindAndReplaceIntoTextBoxes exit ");
        }

        /// <summary>
        /// Find and replace text everythere in word document
        /// </summary>
        /// <param name="doc"> link to document </param>
        /// <param name="Findtext"> source text </param>
        /// <param name="Replacetext"> replace text </param>
        /// <param name="app"> link to application </param>
        void FindAndReplace(word.Application app, string textToFind, string textToReplace, Document doc)
        {
            Logger.GetInstance().SaveLog("FindAndReplace entered " + textToReplace);
            try
            {
                //options
                object matchCase = false;
                object matchWholeWord = true;
                object matchWildCards = false;
                object matchSoundsLike = false;
                object matchAllWordForms = true;
                object forward = true;
                object format = false;
                object matchKashida = false;
                object matchDiacritics = false;
                object matchAlefHamza = false;
                object matchControl = true;
                object read_only = true;
                object visible = true;
                object replace = 2;
                object wrap = word.WdFindWrap.wdFindContinue;
                object text = textToFind;
                object replText = textToReplace;
                //execute find and replace
                if ( !app.Selection.Find.Execute(ref text, ref matchCase, ref matchWholeWord,
                    ref matchWildCards, ref matchSoundsLike, ref matchAllWordForms, ref forward, ref wrap, ref format, ref replText, ref replace,
                    ref matchKashida, ref matchDiacritics, ref matchAlefHamza, ref matchControl))
                {
                    FindAndReplaceIntoTextBoxes(doc, textToFind, textToReplace);
                }
            }catch(Exception ex)
            {
                int i = 0;
            }
            Logger.GetInstance().SaveLog("FindAndReplace exit ");
        }

        /// <summary>
        /// Get device names from invoice list view and store them into document
        /// </summary>
        /// <param name="app"> Word application </param>
        /// <param name="doc"> opened word document </param>
        /// <returns> string with all the devices </returns>
        string AddDeviceNames(word.Application app, Document doc)
        {
            Logger.GetInstance().SaveLog("AddDeviceNames entered ");
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

            FindAndReplace(app, "DeviceText", deviceNames, doc);
            Logger.GetInstance().SaveLog("AddDeviceNames exit ");
            return deviceNames;
        }

        /// <summary>
        /// filling the table in the document from invoice listview
        /// </summary>
        /// <param name="app"> word application </param>
        /// <param name="doc"> opened document </param>
        void AddDescription(word.Application app, Document doc)
        {
            Logger.GetInstance().SaveLog("AddDescription entered ");
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
            Logger.GetInstance().SaveLog("AddDescription exit ");
        }

        /// <summary>
        /// claculate total summ of all the items in invoice list view and save it in the document
        /// </summary>
        /// <param name="app"> word application </param>
        /// <param name="doc"> opened document </param>
        /// <returns> return summ with grd </returns>
        int SaveSumm(word.Application app, Document doc)
        {
            Logger.GetInstance().SaveLog("SaveSumm entered ");
            float total = 0;

            foreach (ListViewItem itm in listView1.Items)
                total += float.Parse(itm.SubItems[4].Text);

            int grd = (int)(total * 100f);
            grd = (grd * 15 / 100) + grd;

            FindAndReplace(app, "TotalSum", total.ToString() + "$", doc);
            FindAndReplace(app, "GRDSum", ((float)grd / 100f).ToString() + "$", doc);
            Logger.GetInstance().SaveLog("SaveSumm exit ");
            return grd;
        }

        /// <summary>
        /// save invoice from the form to database
        /// </summary>
        /// <param name="price"> summ with grd </param>
        /// <param name="devices">  string with all the devices </param>
        void SaveInvoiceToDatabase(int price, string devices)
        {
            Logger.GetInstance().SaveLog("SaveInvoiceToDatabase entered ");
            int invoiceId = int.Parse(InvoiceBox.Text);

            {
                string comm = "insert into tblInvoices values( @InvoiceId, @Date, @CustomName, @UserID, @TotalPrice, @devices)";
                SqlCommand command = new SqlCommand(comm);
                command.Parameters.Add(new SqlParameter("InvoiceId", invoiceId));
                command.Parameters.Add(new SqlParameter("Date", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")));
                command.Parameters.Add(new SqlParameter("CustomName", CustNameBox.Text));
                command.Parameters.Add(new SqlParameter("UserID", MainForm.currentUser.id));
                command.Parameters.Add(new SqlParameter("TotalPrice", price));
                command.Parameters.Add(new SqlParameter("devices", devices));

                SQLWorker.GetInstance().SqlComm(command);
            }

            int subId = SQLWorker.GetInstance().GetMaxId("tblSubInvoices");

            foreach(ListViewItem itm in listView1.Items)
            {
                subId++;
                string subInvoice = "insert into tblSubInvoices values(@Id,@InvoiceId, @device,@description,@price, @count);";
                SqlCommand command = new SqlCommand(subInvoice);
                command.Parameters.Add(new SqlParameter("Id", subId));
                command.Parameters.Add(new SqlParameter("InvoiceId", invoiceId));
                command.Parameters.Add(new SqlParameter("device", itm.Text));
                command.Parameters.Add(new SqlParameter("description", itm.SubItems[1].Text));
                command.Parameters.Add(new SqlParameter("price", (float.Parse(itm.SubItems[3].Text) * 100)));
                command.Parameters.Add(new SqlParameter("count", itm.SubItems[2].Text));

                SQLWorker.GetInstance().SqlComm(command);
            }
            Logger.GetInstance().SaveLog("SaveInvoiceToDatabase exit ");
        }

        /// <summary>
        /// Saving invoice to pdf file with preselected word template
        /// </summary>
        private void SaveInvoiceButton_Click(object sender, EventArgs e)
        {
            Logger.GetInstance().SaveLog("SaveInvoiceButton_Click entered ");
            try
            {
                Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                string path = configuration.AppSettings.Settings["outputDirectory"].Value;
                string pdfPath = path + "\\" + InvoiceBox.Text + ".pdf";
                string tempPath = configuration.AppSettings.Settings["invoiceFile"].Value;
                string tmpfilename = "~&" + Path.GetFileName(path).Substring(2);
                
                string tempBlockPath = path + "\\" + tmpfilename;

                if (File.Exists(tempBlockPath))
                    File.Delete(tempBlockPath);

                string tmpdocFile = "";
                do
                {
                    Random rnd = new Random(DateTime.Now.Millisecond);
                    int k = rnd.Next(1000);
                    tmpdocFile = path + "\\" + InvoiceBox.Text + k + ".docx";
                } while (File.Exists(tmpdocFile));

                File.Copy(tempPath, tmpdocFile);

                word.Application app = new word.Application();
                Document doc = app.Documents.Open(tmpdocFile);

                FindAndReplace(app, "NameText", CustNameBox.Text, doc);
                FindAndReplace(app, "ContactText", CustPhoneBox.Text, doc);
                FindAndReplace(app, "DateText", DateTime.Now.ToString("dd. MM. yyyy"), doc);
                FindAndReplace(app, "InvoiceText", InvoiceBox.Text, doc);
                string devices = AddDeviceNames(app, doc);
                AddDescription(app, doc);
                int summ = SaveSumm(app, doc);
                bool saved = false;

                //different methods for differnt versions of Word ;-(

                try
                {
                    Logger.GetInstance().SaveLog("word version = " + app.Version);
                    float ver = 0;
                    bool res = float.TryParse(app.Version, out ver);
                    Logger.GetInstance().SaveLog("word version = " + ver);

                    if(ver > 14)
                        doc.SaveAs2(pdfPath, word.WdSaveFormat.wdFormatPDF);
                    else
                    {
                        Object objpath = pdfPath;
                        object oMissing = System.Reflection.Missing.Value;
                        doc.SaveAs(ref objpath, WdSaveFormat.wdFormatPDF,
                        ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing,
                        ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing,
                        ref oMissing, ref oMissing, ref oMissing, ref oMissing);
                    }
                    saved = true;
                }catch(AccessViolationException ex) { }
                try
                {
                    if(!saved)
                        doc.SaveAs(pdfPath, word.WdSaveFormat.wdFormatPDF);
                }
                catch (AccessViolationException ex) { }

                doc.Close(false);
                app.Quit(false);

                SaveInvoiceToDatabase(summ, devices);

                File.Delete(tmpdocFile);

            }
            catch(Exception ex)
            {
                MessageBox.Show("Sorry, I couldn't save a file, becouse:\n" + ex.Message, "Word error happens", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            MessageBox.Show("File saved", "File saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            int invoiceID = Int32.Parse(InvoiceBox.Text);
            maxInvoice = (maxInvoice > invoiceID ? maxInvoice : invoiceID);

            Logger.GetInstance().SaveLog("SaveInvoiceButton_Click exit ");
        }

        /// <summary>
        /// clear all the form for making new invoice
        /// </summary>
        public void Button1_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            CustNameBox.Text = "";
            CustPhoneBox.Text = "";
            InvoiceBox.Text = (maxInvoice + 1).ToString();
        }

        /// <summary>
        /// Saving invoice to pdf and print it
        /// </summary>
        private void SaveAndPrintButton_Click(object sender, EventArgs e)
        {
            SaveInvoiceButton_Click(sender, e);

            Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            string path = configuration.AppSettings.Settings["outputDirectory"].Value;
            string pdfPath = path + "\\" + InvoiceBox.Text + ".pdf";

            Process p = new Process();
            p.StartInfo = new ProcessStartInfo()
            {
                CreateNoWindow = true,
                Verb = "print",
                FileName = pdfPath 
            };
            p.Start();
        }
    }
}
