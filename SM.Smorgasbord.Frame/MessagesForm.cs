using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SM.Smorgasbord.Communication.Common.Http;
using SM.Smorgasbord.Communication.SoapEntities.Response;

namespace SM.Smorgasbord.Frame
{
    public partial class MessagesForm : Form
    {
        private delegate void AddMessageHandler();
        public MessagesForm()
        {
            InitializeComponent();

            ImageList imageList = new ImageList();
            imageList.Images.Add("Info", Image.FromFile("Images\\INFO.ICO"));
            imageList.Images.Add("Alert", Image.FromFile("Images\\warning.ico"));
            imageList.Images.Add("Error", Image.FromFile("Images\\error.ico"));

            listViewMessage.SmallImageList = imageList;

            listViewMessage. Dock = DockStyle. Fill;
            listViewMessage.View = View.Details;
            listViewMessage.Columns.Add("System Messages", listViewMessage.Width - 10);
            listViewMessage.HeaderStyle = ColumnHeaderStyle.None;
           // listViewMessage.Columns.Add("Severity");
            ShowMessages();
        }

        private void ShowMessages()
        {
            Messages messages = Messages.Create();
            foreach (MessageItem message in messages.Items)
            {
                ListViewItem listViewItem = new ListViewItem();
                listViewItem.Text = message.Text;
                if (message.Severity == 1)
                {
                    listViewItem.ImageKey = "Info";
                }
                else if (message.Severity == 0)
                {
                    listViewItem.ImageKey = "Alert";
                }
                else
                {
                    listViewItem.ImageKey = "Error";
                }
                //listViewItem.SubItems.Add(message.Severity.ToString());
                listViewMessage.Items.Add(listViewItem);
            }
        }

        private void AddMessage(MessageItem messageItem)
        {
            ListViewItem listViewItem = new ListViewItem();
            listViewItem.Text = messageItem.Text;
            if (messageItem.Severity == 1)
            {
                listViewItem.ImageKey = "Info";
            }
            else if (messageItem.Severity == 0)
            {
                listViewItem.ImageKey = "Alert";
            }
            else
            {
                listViewItem.ImageKey = "Error";
            }

            listViewMessage.Items.Insert(0, listViewItem);
                //.Insert(0, messageItem.Text);
        }

        public void DoMessageAdded(object sender, MessageItem messageItem)
        {
            Invoke(new AddMessageHandler(delegate()
            {
                AddMessage(messageItem);

            }));
            
        }

        private void listViewMessage_SizeChanged(object sender, EventArgs e)
        {
            if (listViewMessage.Columns.Count > 0)
            {
                listViewMessage.Columns[0].Width = listViewMessage.Width - 10;
            }
        }
    }
}
