using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Crownwood.Magic.Docking;
using Crownwood.Magic.Common;
using Crownwood.Magic.Menus;
using SM.Smorgasbord.Communication.Common.Http;

namespace SM.Smorgasbord.Frame
{
    public partial class TopForm : Form
    {
        protected DockingManager dockingManager = null;

        protected Crownwood.Magic.Menus.MenuControl topMenu;
        protected Crownwood.Magic.Controls.TabControl tabControl;
        protected StatusBar statusBar;

        protected MessagesForm messageForm;

        public TopForm()
        {
            InitializeComponent();
            InitDockingWindows();

            //Register the message 
            Messages messages = Messages.Create();
            messages.OnItemAdded += new ItemAddHandler(messageForm.DoMessageAdded);
        }

        private void InitDockingWindows()
        {
            dockingManager = new DockingManager(this, VisualStyle.IDE);

            //Add the tab control first.
            tabControl = new Crownwood.Magic.Controls.TabControl();
            Panel welcomePanel = new Panel();
            Panel welcomePanel2 = new Panel();
            welcomePanel.Dock = DockStyle.Fill;

            Crownwood.Magic.Controls.TabPage welcomePage = new Crownwood.Magic.Controls.TabPage("Welcome", welcomePanel);
            tabControl.TabPages.Add(welcomePage);
            tabControl.Appearance = Crownwood.Magic.Controls.TabControl.VisualAppearance.MultiDocument;
            tabControl.Dock = DockStyle.Fill;
            tabControl.ClosePressed += new EventHandler(DoTabControlClosePressed);
            Controls.Add(tabControl);

            //Add Message form
            messageForm = new MessagesForm();
            Content messageContent = dockingManager.Contents.Add(messageForm, "System Messages");
            WindowContent messageWindowContent = dockingManager.AddContentWithState(messageContent, State.DockBottom) as WindowContent;

            //Add Menus
            topMenu = CreateMenus();
            Controls.Add(topMenu);

            //Add status bar
            statusBar = new StatusBar();
            statusBar.Dock = DockStyle.Bottom;
            statusBar.ShowPanels = true;

            StatusBarPanel statusBarPanel = new StatusBarPanel();
            statusBarPanel.AutoSize = StatusBarPanelAutoSize.Spring;
            statusBar.Panels.Add(statusBarPanel);
            Controls.Add(statusBar);

            dockingManager.InnerControl = tabControl;
            dockingManager.OuterControl = statusBar;
        }

        private void InitMainMenu()
        {
            //this.MainMenuStrip
        }

        private Crownwood.Magic.Menus.MenuControl CreateMenus()
        {
            Crownwood.Magic.Menus.MenuControl topMenu = new Crownwood.Magic.Menus.MenuControl();

            #region Tools menu

            MenuCommand toolItem = new MenuCommand("Tools");

            topMenu.MenuCommands.AddRange(new MenuCommand[] { toolItem });

            MenuHandler menuHandler = new MenuHandler();
            MenuEntity menuEntity = menuHandler.LoadMenu();

            foreach (MenuEntity childEntity in menuEntity.Children)
            {
                menuHandler.DisplayMenu(tabControl, toolItem, childEntity);
            }
            #endregion

            MenuCommand configItem = new MenuCommand("Configs");
            MenuCommand connectionsItem = new MenuCommand("Connections...");
            connectionsItem.Click += new EventHandler(DoConnectionsItemClick);
            configItem.MenuCommands.Add(connectionsItem);
            topMenu.MenuCommands.Add(configItem);

            return topMenu;
        }

        private void DoConnectionsItemClick(object sender, EventArgs args)
        {
            ConnectionForm connectionForm = new ConnectionForm();
            connectionForm.ShowDialog();
        }


        private void DoTabControlClosePressed(object sender, EventArgs args)
        {
            int selectedIndex = tabControl.SelectedIndex;
            if (selectedIndex >= 0)
            {
                // Crownwood. Magic. Controls. TabPage selectedPage = tabControl. TabPages[selectedIndex];
                (tabControl.TabPages[selectedIndex].Control as Form).Close();

                tabControl.TabPages.RemoveAt(selectedIndex);
            }
        }
        protected override void OnClosing(CancelEventArgs e)
        {
             foreach (Crownwood.Magic.Controls.TabPage page in tabControl.TabPages)
             {
                 if (page.Control is Form)
                 {
                     (page.Control as Form).Close();
                 }
             }
             base.OnClosing(e);
        }
    }
}
