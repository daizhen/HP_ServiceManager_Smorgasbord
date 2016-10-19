using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System. Windows. Forms;
using Crownwood. Magic. Docking;
using Crownwood. Magic. Common;

namespace SM.Smorgasbord.Frame
{
    public partial class MainForm : Form
    {
        private int childFormNumber = 0;

        protected DockingManager dockingManager = null;
        private WindowContent mainWindowContent = null;
        private Crownwood. Magic. Controls. TabControl tabControl;

        private Control firstControl = new Label();
        public MainForm()
        {
            firstControl. Visible = false;
            firstControl. Dock = DockStyle. Fill;
            Controls. Add(firstControl);
            InitializeComponent();
            SetMenu();
            //InitDockingWindows();
        }

        #region Private methods

        private void InitDockingWindows()
        {

            dockingManager = new DockingManager(this, VisualStyle. IDE);

            dockingManager. InnerControl = firstControl;

            //Add message windows
            MessagesForm messageForm = new MessagesForm();
            Content messageContent = dockingManager. Contents. Add(messageForm, "System Messages");
            WindowContent messageWindowContent = dockingManager. AddContentWithState(messageContent, State. DockBottom) as WindowContent;


            //Set main windows

            Panel welcomePanel = new Panel();
            tabControl = new Crownwood. Magic. Controls. TabControl();
            tabControl. TabPages. Add(new Crownwood. Magic. Controls. TabPage("First", welcomePanel));
            tabControl. TabPages. Add(new Crownwood. Magic. Controls. TabPage("Third", new MessagesForm()));
            tabControl. Appearance = Crownwood. Magic. Controls. TabControl. VisualAppearance. MultiDocument;
            tabControl. Dock = DockStyle. Fill;
            tabControl. IDEPixelBorder = true;

            //tabControl. SuspendLayout();
            Controls. Add(tabControl);

            //Set the region for docking.
            //RichTextBox filler = new RichTextBox();
            //filler. Dock = DockStyle. Fill;

            StatusBar status = new StatusBar();
            status. Dock = DockStyle. Bottom;

            dockingManager. OuterControl = status;
        }

        #endregion
        private void ShowNewForm(object sender, EventArgs e)
        {
            Form childForm = new Form();
            childForm.MdiParent = this;
            childForm.Text = "Window " + childFormNumber++;
            childForm.Show();
        }

        private void OpenFile(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = openFileDialog.FileName;
            }
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = saveFileDialog.FileName;
            }
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void ToolBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
                toolStrip.Visible = toolBarToolStripMenuItem.Checked;
        }

        private void StatusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            statusStrip.Visible = statusBarToolStripMenuItem.Checked;
        }

        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }

        private void SetMenu()
        {
            MenuHandler menuHandler = new MenuHandler();
            MenuEntity menuEntity = menuHandler.LoadMenu();

            //foreach (MenuEntity childEntity in menuEntity.Children)
            //{
            //    menuHandler.DisplayMenu(this,menuStrip.Items["toolsMenu"] as ToolStripMenuItem, childEntity);
            //}
        }

        private void messagesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessagesForm messagesForm = new MessagesForm();
            messagesForm.MdiParent = this;
            messagesForm.Show();
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConnectionForm connectionForm = new ConnectionForm();
            //connectionForm. MdiParent = parentForm;
            connectionForm. Show();
        }
    }
}
