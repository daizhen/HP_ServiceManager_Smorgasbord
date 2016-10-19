using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SM.Smorgasbord.Communication.Sessions;
using SM.Smorgasbord.Communication.Common;
using SM.Smorgasbord.Communication.Lib;
using Aspose.Words;
using Aspose.Words.Tables;
using Aspose.Words.Reporting;
using System.IO;
using System.Text.RegularExpressions;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Collections.ObjectModel;
using System.Threading;

namespace SM.Smorgasbord.PackageGenerator
{
    public partial class PackageForm : Form
    {
        private bool isQCR = false;
        private bool isPD = false;

        private string auditFileName = "";
        private string auditFileTitle = "";
        private string releaseManagerName = "";
        private string unloadName = "";
        private string fileDir = "";
        private string moveInstructionFileLocation = "";

        private bool needCreateMoveInstruction = false;
        private bool needCreateAuditlog = false;
        private bool needCreateUnloadRecord = false;

        private ConnectionInfo currentConnectionInfo;

        private string moveInstructionName = "";
        private string moveInstructionTitle = "";

        private delegate void UpdateUI();

        public PackageForm()
        {
            Aspose.Words.License license = new Aspose.Words.License();

            license.SetLicense("Aspose.Words.lic");

            InitializeComponent();
            BindServers();

            checkBoxAuditlog.Checked = true;
            checkBoxMoveInstruction.Checked = true;
        }

        #region Private methods

        private void BindServers()
        {
            ServerConnections connectionsInfo = ServerConnections.Load();

            foreach (ConnectionInfo connection in connectionsInfo.Connections)
            {
                comboBoxServer.Items.Add(connection);
            }
        }

        private AuditLogInfo GetAuditInfo(DataBus dataBus, string releaseName)
        {
            JSCodeRunner codeRunner = new JSCodeRunner();
            codeRunner.Include("JSCode\\JsonEncode.js");
            codeRunner.Include("JSCode\\GetAuditInfo.js");
            string codeToRun = " return GetAuditInfo(\"" + releaseName + "\");";
            AuditLogInfo auditLogInfo = codeRunner.Run<AuditLogInfo>(dataBus, codeToRun);
            return auditLogInfo;
        }

        private UnloadEntity GetUnloadEntity(DataBus dataBus, string unloadName)
        {
            JSCodeRunner codeRunner = new JSCodeRunner();
            codeRunner.Include("JSCode\\JsonEncode.js");
            codeRunner.Include("JSCode\\GetUnloadEntity.js");
            string codeToRun = " return GetUnloadEntity(\"" + unloadName + "\");";
            UnloadEntity unloadEntity = codeRunner.Run<UnloadEntity>(dataBus, codeToRun);
            return unloadEntity;
        }

        private void CreateUnloadFile(DataBus dataBus, string releaseName, string unloadName)
        {
            JSCodeRunner codeRunner = new JSCodeRunner();
            codeRunner.Include("JSCode\\JsonEncode.js");
            codeRunner.Include("JSCode\\CreateUnloadRecord.js");
            string codeToRun = " return CreateUnloadRecord(\"" + unloadName + "\",\"" + releaseName + "\");";
            JsonRaw unloadEntity = codeRunner.Run<JsonRaw>(dataBus, codeToRun);
        }

        private void GenerateAuditLog(AuditLogInfo auditLogInfo, string fileName)
        {
            Document document = new Document("Templates\\Audit Log File Template.docx");

            //Construct the datatable
            DataTable auditTable = new DataTable("Audit");
            auditTable.Columns.Add("ObjectName");
            auditTable.Columns.Add("Query");
            auditTable.Columns.Add("CheckInDate");
            auditTable.Columns.Add("Description");

            foreach (AuditItem item in auditLogInfo.Items)
            {
                auditTable.Rows.Add(item.ElementName, item.Query, item.Date, item.Description);
            }

            document.MailMerge.MergeField += new MergeFieldEventHandler(HandleMergeField);
            //IFieldMergingCallback
            document.MailMerge.ExecuteWithRegions(auditTable);
            document.Range.Bookmarks["Title"].Text = auditLogInfo.Title;
            document.Range.Bookmarks["Auther"].Text = auditLogInfo.Author;
            document.Save(textBoxFolder.Text.Trim() + "\\" + fileName);
            //auditTable.Rows.Add(
        }

        private void GenerateMoveInstructions(MoveInstructionInfo moveInstructionInfo, string fileName)
        {
            Document document = new Document("Templates\\Move Instructions Template.docx");
            document.Range.Bookmarks["Title"].Text = moveInstructionInfo.Title;
            document.Range.Bookmarks["Author"].Text = moveInstructionInfo.Author;
            //document. Range. Bookmarks["Date"]. Text = moveInstructionInfo;
            document.Range.Bookmarks["UnlName"].Text = moveInstructionInfo.Entity.Name;


            //Insert the html format screeshot
            string htmlString = CreateHtmlForScreenShot(moveInstructionInfo.Entity);
            DocumentBuilder docBuilder = new DocumentBuilder(document);
            docBuilder.MoveToBookmark("ScreenShot");
            Image screenShot = CreateUnloadImage(moveInstructionInfo.Entity);
            MemoryStream imageStream = new MemoryStream();
            screenShot.Save(imageStream, ImageFormat.Jpeg);
            imageStream.Position = 0;
            Collection<byte> imageData = new Collection<byte>();

            byte[] bufferData = new byte[1024];
            int readLength = imageStream.Read(bufferData, 0, 1024);
            while (readLength > 0)
            {
                //Copy the data.
                for (int i = 0; i < readLength; i++)
                {
                    imageData.Add(bufferData[i]);
                }
                readLength = imageStream.Read(bufferData, 0, 1024);
            }

            imageStream.Dispose();

            docBuilder.InsertImage(imageData.ToArray());
            //docBuilder. InsertHtml(htmlString);
            document.Save(textBoxFolder.Text.Trim() + "\\" + fileName);
            //Insert the unload file to doc.
            InsertUnloadFileToDocument("FileObject", textBoxFolder.Text.Trim() + "\\" + fileName, moveInstructionInfo.UnloadFileLocation);
        }

        private string CreateHtmlForScreenShot(UnloadEntity entity)
        {
            //Params:
            //0: Unload Name
            //1: unload?
            //2: purge?
            //3: Show Unload Records 
            //4: Show Protected Formats 
            const string HeaderFormat = @"<label style='font-family: Arial, Tahoma, Helvetica, sans-serif; font-size: 11px; color: rgb(34, 34, 34); margin-top: 0px; margin-right: 0px; margin-bottom: 0px; margin-left: 2px; white-space: nowrap;'>Unload Script:</label>
                    <input style='font-family: Arial, Tahoma, Helvetica, sans-serif; font-size: 11px; margin-top: 0px; margin-right: 0px; margin-bottom: 0px; margin-left: 0px; padding-top: 0px; padding-right: 0px; padding-bottom: 0px; padding-left: 0px; width: 459px; height: 19px; ' value='{0}'/>
                    <p/>
                    <span>
                    <input sctype='CheckBox' style='font-family: Arial, Tahoma, Helvetica, sans-serif; font-size: 11px; margin-top: 0px; margin-right: 0px; margin-bottom: 0px; margin-left: 0px; padding-top: 0px; padding-right: 0px; padding-bottom: 0px; padding-left: 0px; ' type='checkbox' {1}  /><label style='font-family: Arial, Tahoma, Helvetica, sans-serif; font-size: 11px; color: rgb(34, 34, 34); margin-top: 0px; margin-right: 0px; margin-bottom: 0px; margin-left: 2px; white-space: nowrap;'>
                    Unload?</label>
                    <input sctype='CheckBox' style='font-family: Arial, Tahoma, Helvetica, sans-serif; font-size: 11px; margin-top: 0px; margin-right: 0px; margin-bottom: 0px; margin-left: 0px; padding-top: 0px; padding-right: 0px; padding-bottom: 0px; padding-left: 0px; ' type='checkbox' {2} /><label style='font-family: Arial, Tahoma, Helvetica, sans-serif; font-size: 11px; color: rgb(34, 34, 34); margin-top: 0px; margin-right: 0px; margin-bottom: 0px; margin-left: 2px; white-space: nowrap;'>Purge?</label>
                    </span>
                    <p>
                    <input checked='{3}' style='font-family: Arial, Tahoma, Helvetica, sans-serif; font-size: 11px; margin-top: 0px; margin-right: 0px; margin-bottom: 0px; margin-left: 0px; padding-top: 0px; padding-right: 0px; padding-bottom: 0px; padding-left: 0px; top: 0px; left: 0px; ' type='radio' />
                    <label style='font-family: Arial, Tahoma, Helvetica, sans-serif; font-size: 11px; color: rgb(34, 34, 34); margin-top: 0px; margin-right: 0px; margin-bottom: 0px; margin-left: 2px; white-space: nowrap;'> Show Unload Records</label>
                    <input checked='{4}' style='font-family: Arial, Tahoma, Helvetica, sans-serif; font-size: 11px; margin-top: 0px; margin-right: 0px; margin-bottom: 0px; margin-left: 0px; padding-top: 0px; padding-right: 0px; padding-bottom: 0px; padding-left: 0px; top: 0px; left: 0px; ' type='radio' />
                    <label  style='font-family: Arial, Tahoma, Helvetica, sans-serif; font-size: 11px; color: rgb(34, 34, 34); margin-top: 0px; margin-right: 0px; margin-bottom: 0px; margin-left: 2px; white-space: nowrap;'> Show Protected Formats</label>
                     <input type='checkbox'/>";

            const string TableHeader = @"
                    <table border='0' cellpadding='0' cellspacing='0' style='background-color: rgb(153, 153, 153); border-top-width: 1px; border-right-width: 1px; border-bottom-width: 1px; border-left-width: 1px; border-top-style: solid; border-right-style: solid; border-bottom-style: solid; border-left-style: solid; border-top-color: rgb(153, 187, 232); border-right-color: rgb(153, 187, 232); border-bottom-color: rgb(153, 187, 232); border-left-color: rgb(153, 187, 232); ' width='100%'>
                     <colgroup>
	                     <col width='204' />
	                     <col width='872' />
	                     <col width='171' />
                     </colgroup>	                
                    <tr>	
                    <th style='margin-top: 0px; margin-right: 0px; margin-bottom: 0px; margin-left: 0px; padding-top: 3px; padding-right: 8px; padding-bottom: 3px; padding-left: 8px; font-style: normal; font-weight: normal; text-align: left; border-left-color: rgb(201, 201, 201); border-left-width: 1px; border-left-style: solid; border-bottom-color: rgb(201, 201, 201); border-bottom-width: 1px; border-bottom-style: solid;  background-attachment: initial; background-origin: initial; background-clip: initial; background-color: rgb(249, 249, 249); height: 20px; white-space: nowrap; background-position: 0% 0%; background-repeat: repeat no-repeat;'>
                		
		                <span style='font-family: Arial, Tahoma, Helvetica, sans-serif; font-size: 11px; font-weight: bold; color: rgb(34, 34, 34); margin-top: 0px; margin-right: 0px; margin-bottom: 0px; margin-left: 2px; white-space: nowrap; '>Filename</span></th>
		                <th style='margin-top: 0px; margin-right: 0px; margin-bottom: 0px; margin-left: 0px; padding-top: 3px; padding-right: 8px; padding-bottom: 3px; padding-left: 8px; font-style: normal; font-weight: normal; text-align: left; border-left-color: rgb(201, 201, 201); border-left-width: 1px; border-left-style: solid; border-bottom-color: rgb(201, 201, 201); border-bottom-width: 1px; border-bottom-style: solid;  background-attachment: initial; background-origin: initial; background-clip: initial; background-color: rgb(249, 249, 249); height: 20px; white-space: nowrap; background-position: 0% 0%; background-repeat: repeat no-repeat;'>
                		
		                <span style='font-family: Arial, Tahoma, Helvetica, sans-serif; font-size: 11px; font-weight: bold; color: rgb(34, 34, 34); margin-top: 0px; margin-right: 0px; margin-bottom: 0px; margin-left: 2px; white-space: nowrap; '>
		                Query</span></th>
		                <th style='margin-top: 0px; margin-right: 0px; margin-bottom: 0px; margin-left: 0px; padding-top: 3px; padding-right: 8px; padding-bottom: 3px; padding-left: 8px; font-style: normal; font-weight: normal; text-align: left; border-left-color: rgb(201, 201, 201); border-left-width: 1px; border-left-style: solid; border-bottom-color: rgb(201, 201, 201); border-bottom-width: 1px; border-bottom-style: solid;  background-attachment: initial; background-origin: initial; background-clip: initial; background-color: rgb(249, 249, 249); height: 20px; white-space: nowrap; background-position: 0% 0%; background-repeat: repeat no-repeat;'>
		                <span style='font-family: Arial, Tahoma, Helvetica, sans-serif; font-size: 11px; font-weight: bold; color: rgb(34, 34, 34); margin-top: 0px; margin-right: 0px; margin-bottom: 0px; margin-left: 2px; white-space: nowrap; '>Datamap</span></th>
	                </tr>";
            const string CellFormat = @"<td style='margin-top: 0px; margin-right: 0px; margin-bottom: 0px; margin-left: 0px; padding-top: 3px; padding-right: 8px; padding-bottom: 3px; padding-left: 8px; font-style: normal; font-weight: normal; text-align: left; border-left-color: rgb(201, 201, 201); border-left-width: 1px; border-left-style: solid; border-bottom-color: rgb(201, 201, 201); border-bottom-width: 1px; border-bottom-style: solid;  background-attachment: initial; background-origin: initial; background-clip: initial; background-color: rgb(249, 249, 249); height: 20px; white-space: nowrap; background-position: 0% 0%; background-repeat: repeat no-repeat; font-family: Arial, Tahoma, Helvetica, sans-serif; font-size: 11px; color: rgb(34, 34, 34); margin-top: 0px; margin-right: 0px; margin-bottom: 0px; margin-left: 2px; white-space: nowrap;'>{0}</td>";

            StringBuilder resultString = new StringBuilder();
            resultString.AppendFormat(HeaderFormat, entity.Name, entity.Unload ? "checked" : "", entity.Purge ? "checked" : "", true, false);

            resultString.Append("</p>");
            //Build table
            resultString.Append(TableHeader);
            foreach (UnloadItem item in entity.Items)
            {
                resultString.Append("<tr>");
                resultString.AppendFormat(CellFormat, item.ElementName);
                resultString.AppendFormat(CellFormat, item.Query);
                resultString.AppendFormat(CellFormat, string.Empty);
                resultString.Append("</tr>");
            }
            resultString.Append("</table>");
            return resultString.ToString();
        }

        private void GetDetailInfoFromReleaseManager(string releaseString)
        {
            string release = "";
            string qcr = "";
            string cr = "";

            if (releaseString.StartsWith("MS"))
            {
                releaseString = releaseString.Substring(2).Trim();
            }
            if (releaseString.StartsWith("."))
            {
                releaseString = releaseString.Substring(1).Trim();
            }

            Regex regex = new Regex(@"\s+");
            string[] items = regex.Split(releaseString);
            if (items[0].StartsWith("R"))
            {
                release = items[0].Substring(1);
            }

            if (items.Length > 1)
            {
                if (items[1].StartsWith("QCR"))
                {
                    isQCR = true;
                    isPD = false;
                    qcr = items[1].Substring(3);
                }
                else if (items[1].StartsWith("PD"))
                {
                    isQCR = false;
                    isPD = true;
                    qcr = items[1].Substring(2);
                }
                else if (items[1].StartsWith("CR"))
                {
                    isQCR = false;
                    isPD = false;
                    cr = items[1].Substring(2);
                }
                else
                {
                    //Nothing to do
                }
            }

            if (items.Length > 2)
            {
                if (items[2].StartsWith("CR"))
                {
                    cr = items[2].Substring(2);
                }
            }
            textBoxRelease.Text = release;
            textBoxQCR.Text = qcr;
            textBoxCR.Text = cr;

        }

        private void BuildDocName()
        {
            const string DocFileFormat = "Audit Log File for MS.R{0} - {1}{2} - {3}";
            const string MoveInstructionFileFormat = "Move Instruction File for MS.R{0} - {1}{2} - {3}";
            const string UnloadNameFormat = "MS.R{0} {1}{2} - {3}";

            string release = textBoxRelease.Text.Trim();
            string qcr = textBoxQCR.Text.Trim();
            string cr = textBoxCR.Text.Trim();
            string description = textBoxDescription.Text.Trim();
            if (isQCR)
            {
                qcr = "QCR" + qcr;
            }
            else
            {
                qcr = "PD" + qcr;
            }

            if (!string.IsNullOrEmpty(cr))
            {
                cr = " CR" + cr;
            }
            //Build Audit log file name
            StringBuilder docString = new StringBuilder();
            docString.AppendFormat(DocFileFormat, release, qcr, cr, description);
            auditFileTitle = docString.ToString();
            auditFileName = auditFileTitle + ".docx";

            textBoxAuditName.Text = auditFileName;

            //Build Move Instruction file name
            docString = new StringBuilder();
            docString.AppendFormat(MoveInstructionFileFormat, release, qcr, cr, description);
            moveInstructionTitle = docString.ToString();
            moveInstructionName = moveInstructionTitle + ".docx";

            textBoxMoveInstructionLocation.Text = moveInstructionName;

            textBoxUnloadName.Text = string.Format(UnloadNameFormat, release, qcr, cr, description);
            textBoxUnloadFile.Text = textBoxUnloadName.Text + ".Unload.Script.unl";
        }

        private void InsertUnloadFileToDocument(string bookmark, string documentLocation, string unloadLocation)
        {
            Microsoft.Office.Interop.Word.Application WordApp = new Microsoft.Office.Interop.Word.ApplicationClass();

            Microsoft.Office.Interop.Word.Document doc = new Microsoft.Office.Interop.Word.Document();

            //Full file name
            object objFileName = documentLocation;
            object visible = false;
            object readOnly = false;
            object IsSave = true;

            object bookMarkName = bookmark;
            object oClassType = "MSGraph.Chart";
            object oFileName = unloadLocation;
            object oMissing = System.Reflection.Missing.Value;
            object missingValue = System.Reflection.Missing.Value;
            try
            {
                //打开文件
                doc = WordApp.Documents.Open(ref objFileName, ref missingValue, ref readOnly, ref missingValue,
                    ref missingValue, ref missingValue, ref missingValue, ref missingValue,
                    ref missingValue, ref missingValue, ref missingValue, ref missingValue,
                    ref missingValue, ref missingValue, ref missingValue,
                    ref missingValue);
                doc.Activate();
                Microsoft.Office.Interop.Word.Bookmark titleBookmark = doc.Bookmarks.get_Item(ref bookMarkName);
                titleBookmark.Range.InlineShapes.AddOLEObject(ref oClassType, ref oFileName, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing);
                //doc. Close(ref IsSave, ref missingValue, ref missingValue);
            }
            finally
            {
                doc.Close(ref IsSave, ref missingValue, ref missingValue);
            }
        }

        private Image CreateUnloadImage(UnloadEntity entity)
        {
            int width = 1000;
            int height = 80 + 20 * (entity.Items.Count + 1);
            System.Drawing.Font textFont = new System.Drawing.Font("Helvetica", 10, FontStyle.Regular, GraphicsUnit.Pixel);
            System.Drawing.Font imgFont = new System.Drawing.Font("Helvetica", 16, FontStyle.Bold, GraphicsUnit.Pixel);

            Image image = new Bitmap(width, height, PixelFormat.Format32bppRgb);
            Graphics graphics = Graphics.FromImage(image);
            graphics.CompositingQuality = CompositingQuality.AssumeLinear;
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
            graphics.FillRectangle(new SolidBrush(Color.White), 0, 0, width, height);

            Pen linePen = new Pen(Color.Black, 1);
            graphics.DrawRectangle(linePen, 0, 0, width - 2, height - 2);

            int nameLabelX = 40;
            int nameLabelY = 20;
            SolidBrush textBrush = new SolidBrush(Color.Black);
            SolidBrush backgroundBrush = new SolidBrush(Color.SandyBrown);
            graphics.DrawString("Unload Script:", textFont, textBrush, nameLabelX, nameLabelY, new StringFormat());
            graphics.DrawString("Unload Script:", textFont, textBrush, nameLabelX, nameLabelY, new StringFormat());
            int nameTextX = 150;
            int nameTextY = nameLabelY - 4;
            int nameTextWidth = 400;
            int nameTextHeight = 20;
            graphics.DrawRectangle(linePen, nameTextX, nameTextY, nameTextWidth, nameTextHeight);

            graphics.DrawString(entity.Name, textFont, textBrush, nameTextX + 5, nameLabelY, new StringFormat());

            string chooseStr = "√";

            int unloadCheckboxX = 40;
            int unloadCheckboxY = 50;
            int unloadCheckboxWidth = 15;
            graphics.DrawRectangle(linePen, unloadCheckboxX, unloadCheckboxY, unloadCheckboxWidth, unloadCheckboxWidth);

            if (entity.Unload)
            {
                graphics.DrawString(chooseStr, imgFont, textBrush, unloadCheckboxX, unloadCheckboxY);
                graphics.DrawString(chooseStr, imgFont, textBrush, unloadCheckboxX, unloadCheckboxY);
            }

            int unloadTextX = 60;
            int unloadTextY = 50;
            graphics.DrawString("Unload?", textFont, textBrush, unloadTextX, unloadTextY);

            int purgeCheckboxX = 150;
            int purgeCheckboxY = 50;
            graphics.DrawRectangle(linePen, purgeCheckboxX, purgeCheckboxY, unloadCheckboxWidth, unloadCheckboxWidth);

            if (entity.Purge)
            {
                graphics.DrawString(chooseStr, imgFont, textBrush, purgeCheckboxX, purgeCheckboxY);
                graphics.DrawString(chooseStr, imgFont, textBrush, purgeCheckboxX, purgeCheckboxY);
            }
            int purgeTextX = 180;
            int purgeTextY = 50;
            graphics.DrawString("Purge?", textFont, textBrush, purgeTextX, purgeTextY);


            //Draw the Table;
            int firstColumnWidth = (int)((width - 2) * 0.25);
            int secondColumnWidth = (int)((width - 2) * 0.6);
            int thirdColumnWidth = (int)((width - 2) * 0.15);

            int rowHeight = 20;


            int firstCellX = 2;
            int firstCellY = 80;

            int secondCellX = firstCellX + firstColumnWidth;
            int secondCellY = firstCellY;

            int thirdCellX = secondCellX + secondColumnWidth;
            int thirdCellY = firstCellY;
            //Draw header
            graphics.DrawRectangle(linePen, firstCellX, firstCellY, firstColumnWidth, rowHeight);
            graphics.FillRectangle(backgroundBrush, firstCellX, firstCellY, firstColumnWidth, rowHeight);
            graphics.DrawString("FileName", textFont, textBrush, firstCellX + 4, firstCellY + 4);

            graphics.DrawRectangle(linePen, secondCellX, secondCellY, secondColumnWidth, rowHeight);
            graphics.FillRectangle(backgroundBrush, secondCellX, secondCellY, secondColumnWidth, rowHeight);
            graphics.DrawString("Query", textFont, textBrush, secondCellX + 4, secondCellY + 4);

            graphics.DrawRectangle(linePen, thirdCellX, thirdCellY, thirdColumnWidth, rowHeight);
            graphics.FillRectangle(backgroundBrush, thirdCellX, thirdCellY, thirdColumnWidth, rowHeight);
            graphics.DrawString("Datamap", textFont, textBrush, thirdCellX + 4, thirdCellY + 4);

            for (int i = 0; i < entity.Items.Count; i++)
            {
                UnloadItem currentItem = entity.Items[i];

                firstCellX = 2;
                firstCellY = 80 + rowHeight * (i + 1);

                secondCellX = firstCellX + firstColumnWidth;
                secondCellY = firstCellY;

                thirdCellX = secondCellX + secondColumnWidth;
                thirdCellY = firstCellY;

                graphics.DrawRectangle(linePen, firstCellX, firstCellY, firstColumnWidth, rowHeight);
                graphics.DrawString(currentItem.ElementName, textFont, textBrush, firstCellX + 4, firstCellY + 4);

                graphics.DrawRectangle(linePen, secondCellX, secondCellY, secondColumnWidth, rowHeight);
                graphics.DrawString(currentItem.Query, textFont, textBrush, secondCellX + 4, secondCellY + 4);

                graphics.DrawRectangle(linePen, thirdCellX, thirdCellY, thirdColumnWidth, rowHeight);

            }

            graphics.Flush();
            graphics.Dispose();
            return image;
        }

        private bool ValidateForm()
        {
            if (comboBoxServer.SelectedItem == null)
            {
                MessageBox.Show("Please select a server!");
                return false;
            }

            if (string.IsNullOrEmpty(textBoxReleaseManager.Text))
            {
                MessageBox.Show("Please input release manager!");
                return false;
            }

            if (string.IsNullOrEmpty(textBoxUnloadName.Text))
            {
                MessageBox.Show("Please input release manager!");
                return false;
            }

            if (string.IsNullOrEmpty(textBoxFolder.Text))
            {
                MessageBox.Show("Please input folder!");
                return false;
            }

            if (textBoxUnloadName.Text.Length > 70)
            {
                MessageBox.Show("Unload name too long, extend 70!");
                return false;
            }

            return true;
        }

        private void DisableControls()
        {
            buttonBrowse.Enabled = false;
            buttonGenerate.Enabled = false;
            buttonGetDocInfo.Enabled = false;
            buttonGetPackageInfo.Enabled = false;
        }

        private void EnableControls()
        {
            buttonBrowse.Enabled = true;
            buttonGenerate.Enabled = true;
            buttonGetDocInfo.Enabled = true;
            buttonGetPackageInfo.Enabled = true;

        }

        #endregion

        #region Events

        protected void HandleMergeField(object sender, MergeFieldEventArgs e)
        {
            // All merge fields that expect HTML data should be marked with some prefix, e.g. 'html'.
            if (e.DocumentFieldName == "Description")
            {
                // Insert the text for this merge field as HTML data, using DocumentBuilder.
                DocumentBuilder builder = new DocumentBuilder(e.Document);
                builder.MoveToMergeField(e.DocumentFieldName);
                builder.InsertHtml((string)e.FieldValue);
                // The HTML text itself should not be inserted.
                // We have already inserted it as an HTML.
                e.Text = "";
            }
        }

        private void PackageForm_Load(object sender, EventArgs e)
        {
        }

        private void OnButtonBrowseClick(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.SelectedPath = textBoxFolder.Text;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                textBoxFolder.Text = dialog.SelectedPath;
            }
        }

        private void DoButtonGenerateClick(object sender, EventArgs e)
        {
            releaseManagerName = textBoxReleaseManager.Text.Trim();
            unloadName = textBoxUnloadName.Text.Trim();
            fileDir = textBoxFolder.Text.Trim() + "\\" + textBoxUnloadFile.Text.Trim();
            moveInstructionFileLocation = textBoxMoveInstructionLocation.Text.Trim();

            needCreateAuditlog = checkBoxAuditlog.Checked;
            needCreateUnloadRecord = checkBoxCreateNewUnload.Checked;
            needCreateMoveInstruction = checkBoxMoveInstruction.Checked;
            currentConnectionInfo = comboBoxServer.SelectedItem as ConnectionInfo;
            if (!ValidateForm())
            {
                return;
            }
            DisableControls();
            Thread thread = new Thread(new ThreadStart(CreatePackage));
            thread.Start();
        }

        private void CreatePackage()
        {
            try
            {
                if (currentConnectionInfo != null)
                {
                    SessionCache sessionCache = new SessionCache();
                    ConnectionSession connectionSession = sessionCache[currentConnectionInfo];
                    DataBus dataBus = connectionSession.GetDataBus();
                    if (needCreateAuditlog)
                    {
                        //Generate audit log file.
                        AuditLogInfo auditInfo = GetAuditInfo(dataBus, releaseManagerName);
                        auditInfo.Author = currentConnectionInfo.UserName;
                        auditInfo.Title = auditFileTitle;
                        GenerateAuditLog(auditInfo, auditFileName);
                    }

                    //Create unload record
                    if (needCreateUnloadRecord)
                    {
                        CreateUnloadFile(dataBus, releaseManagerName, unloadName);
                    }
                    //Generate move instruction doc.
                    if (needCreateMoveInstruction)
                    {
                        dataBus = connectionSession.GetDataBus();
                        //Generate unload script file
                        UnloadScriptFile unloadScriptFile = new UnloadScriptFile();
                        unloadScriptFile.FileLocation = fileDir;
                        unloadScriptFile.UnloadScriptName = unloadName;
                        unloadScriptFile.GenerateUnloadScript(dataBus);

                        //Create move instruction.
                        MoveInstructionInfo moveInstructionInfo = new MoveInstructionInfo();
                        moveInstructionInfo.UnloadFileLocation = unloadScriptFile.FileLocation;
                        moveInstructionInfo.Title = textBoxMoveInstructionLocation.Text.Trim();
                        moveInstructionInfo.Author = currentConnectionInfo.UserName;
                        moveInstructionInfo.Entity = GetUnloadEntity(connectionSession.GetDataBus(), unloadScriptFile.UnloadScriptName);
                        GenerateMoveInstructions(moveInstructionInfo, moveInstructionFileLocation);
                    }
                }

                Invoke(new UpdateUI(delegate()
                {
                    MessageBox.Show("Package Created");
                    EnableControls();
                }));
            }
            catch
            {

                Invoke(new UpdateUI(delegate()
                 {
                     MessageBox.Show("Create Package Failed, please try again");
                     EnableControls();
                 }));
            }
        }

        private void DoTextBoxReleasemanagerMouseLeave(object sender, EventArgs e)
        {

        }

        private void DoTextBoxReleasemanagerFocusLeave(object sender, EventArgs e)
        {
            //GetDetailInfoFromReleaseManager(textBoxReleaseManager. Text. Trim());
            //BuildDocName();
        }

        private void DoTextBoxDescriptionFocusLeave(object sender, EventArgs e)
        {
            //GetDetailInfoFromReleaseManager(textBoxReleaseManager. Text. Trim());
            //BuildDocName();
        }

        private void DoButtonGetPackageInfoClick(object sender, EventArgs e)
        {
            const string UnloadNameFormat = "MS.R{0} {1}{2} - {3}";

            string releaseManagerString = textBoxReleaseManager.Text.Trim();

            GetDetailInfoFromReleaseManager(releaseManagerString);

            string release = textBoxRelease.Text.Trim();
            string qcr = textBoxQCR.Text.Trim();
            string cr = textBoxCR.Text.Trim();
            string description = textBoxDescription.Text.Trim();

            if (isQCR)
            {
                qcr = "QCR" + qcr;
            }
            if (isPD)
            {
                qcr = "PD" + qcr;
            }

            if (!string.IsNullOrEmpty(cr))
            {
                cr = " CR" + cr;
            }

            textBoxUnloadName.Text = string.Format(UnloadNameFormat, release, qcr, cr, description);
        }

        private void DoButtonGetDocInfoClick(object sender, EventArgs e)
        {
            const string DocFileFormat = "Audit Log File for MS.R{0} - {1}{2} - {3}";
            const string MoveInstructionFileFormat = "Move Instruction File for MS.R{0} - {1}{2} - {3}";
            string release = textBoxRelease.Text.Trim();
            string qcr = textBoxQCR.Text.Trim();
            string cr = textBoxCR.Text.Trim();
            string description = textBoxDescription.Text.Trim();
            if (isQCR)
            {
                qcr = "QCR" + qcr;
            }
            if (isPD)
            {
                qcr = "PD" + qcr;
            }

            if (!string.IsNullOrEmpty(cr))
            {
                cr = " CR" + cr;
            }
            //Build Audit log file name
            StringBuilder docString = new StringBuilder();
            docString.AppendFormat(DocFileFormat, release, qcr, cr, description);
            auditFileTitle = docString.ToString();
            auditFileName = auditFileTitle + ".docx";
            textBoxAuditName.Text = auditFileName;

            //Build Move Instruction file name
            docString = new StringBuilder();
            docString.AppendFormat(MoveInstructionFileFormat, release, qcr, cr, description);
            moveInstructionTitle = docString.ToString();
            moveInstructionName = moveInstructionTitle + ".docx";
            textBoxMoveInstructionLocation.Text = moveInstructionName;

            //Set the unload script file name
            textBoxUnloadFile.Text = textBoxUnloadName.Text + ".Unload.Script.unl";
        }

        #endregion

        private void DoButtonOpenClick(object sender, EventArgs e)
        {
            SHOpenFolder openFolder = new SHOpenFolder();
            openFolder.OpenAndSelect(textBoxFolder.Text + "\\12");
        }
    }
}
