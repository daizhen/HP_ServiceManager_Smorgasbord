using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ScintillaNET;
using System.Text.RegularExpressions;
using SM.Smorgasbord.RADParser;

namespace SM.Smorgasbord.Debuger
{
    public partial class FormatControl : Panel
    {
        private RTViewer rtViewer = new RTViewer();
        private int unitPixel = 15;
        private int widthUnitPixel = 5;

        private const int SCE_C_COMMENT = 1;
        private const int SCE_C_COMMENTLINE = 2;
        private const int SCE_C_WORD = 5;
        private const int SCE_C_STRING = 6;
        private const int SCE_C_CHARACTER = 7;

        public DebugTracePanel DebugTrace
        {
            get;
            set;
        }

        public FormatControl(Format formatObject)
        {
            InitializeComponent();
            this.Width = 0;
            this.Height = 0;
            ShowFormat(formatObject, this);
        }

        private void SetCodeViewerStyle(Scintilla viewer)
        {
            string keywordList = "if then else for to do while in and or not true false isin";

            //STYLE_DEFAULT
            viewer.NativeInterface.StyleSetSize(32, 12);
            viewer.NativeInterface.StyleSetFont(32, "Courier New");
            viewer.NativeInterface.StyleClearAll();
            //SCLEX_CPP
            viewer.NativeInterface.SetLexer(3);


            viewer.NativeInterface.SetKeywords(0,keywordList);
            viewer.NativeInterface.StyleSetFore(SCE_C_WORD, 0x00FF0000); //Key

            viewer.NativeInterface.StyleSetFore(SCE_C_STRING, 0x001515A3); //string

            viewer.NativeInterface.StyleSetFore(SCE_C_CHARACTER, 0x00008000); //char

            viewer.NativeInterface.StyleSetFore(SCE_C_COMMENT, 0x00008000); //Comments
            viewer.NativeInterface.StyleSetFore(SCE_C_COMMENTLINE, 0x00008000); //Comments
            viewer.NativeInterface.SetCaretLineVisible(true);
            viewer.NativeInterface.SetMouseDwellTime(500);
            viewer.DwellStart += new EventHandler<ScintillaNET.ScintillaMouseEventArgs>(viewer_DwellStart);
            viewer.Click += new EventHandler(viewer_Click);
            MenuItem item = new MenuItem("Format");
            item.Click += new EventHandler(viewer_FormatCodeClick);
            viewer.ContextMenu = new ContextMenu();
            viewer.ContextMenu.MenuItems.Add(item);
        }

        private void ShowFormat(Format formatObject, Control parent)
        {
            int fieldIndex = 0;

            while (fieldIndex < formatObject.Fields.Count)
            {
                FormatField field = formatObject.Fields[fieldIndex];
                if (!string.IsNullOrEmpty(field.Property))
                {
                    if (field.PropertyObject.FieldType == "Label")
                    {
                        ShowLabel(field, parent);
                    }
                    else if (field.PropertyObject.FieldType == "Text")
                    {
                        ShowTextControl(field, parent);
                    }
                    else
                    {

                    }
                }
                else
                {
                    //Label
                    if (field.Attribute == 2)
                    {
                        ShowLabel(field, parent);
                    }
                    else if (field.Attribute == 1) //Text Box
                    {
                        ShowTextControl(field, parent);
                    }
                    if (!string.IsNullOrEmpty(field.Output))
                    {
                        ShowLabel(field, parent);
                    }
                }
                fieldIndex++;
            }
        }

        private void ShowLabel(FormatField field, Control parent)
        {
            Label label = new Label();

            //if (!string.IsNullOrEmpty(field.Property))
            //{
            //    label.Location = new Point(field.PropertyObject.X * widthUnitPixel, field.PropertyObject.Y * unitPixel);
            //    label.Width = field.PropertyObject.Width * widthUnitPixel;
            //    label.Height = field.PropertyObject.Height * unitPixel;
            //    label.Text = field.PropertyObject.Items["Caption"];
            //}
            //else
            //{

            //    label.Location = new Point((field.Column - 1) * 2 * widthUnitPixel, (field.Line - 1) * 2 * unitPixel);
            //    label.Width = field.Length * 2 * widthUnitPixel;
            //    //label.Height = field.PropertyObject.Height * unitPixel;
            //    label.Text = field.Output;

            //}
            label.Location = GetFieldLocation(field);
            label.Width = GetFieldWidth(field);
            label.Height = GetFieldHeight(field);
            label.Text = GetFieldCaption(field);
            parent.Controls.Add(label);
            UpdateSize(parent, label);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="field"></param>
        /// <param name="parent"></param>
        private void ShowTextControl(FormatField field, Control parent)
        {
            if (field.Window == 0)
            {
                ShowSingleTextControl(field, parent);
            }
            else
            {
                if (field.PropertyObject.Items.ContainsKey("Elastic"))
                {
                    ShowMultiTextControl(field, parent);
                }
                else
                {
                    ShowListBox(field, parent);
                }
            }
        }

        private void ShowSingleTextControl(FormatField field, Control parent)
        {
            TextBox textBox = new TextBox();

            textBox.Location = GetFieldLocation(field);
            textBox.Width = GetFieldWidth(field);
            textBox.Height = GetFieldHeight(field);
            string inputName = GetFieldInput(field);
            if (!string.IsNullOrEmpty(inputName))
            {
                textBox.Name = inputName.Replace('.', '_');
            }
            parent.Controls.Add(textBox);

            UpdateSize(parent, textBox);
        }

        private void ShowMultiTextControl(FormatField field, Control parent)
        {
            Scintilla textBox = new Scintilla();
            SetCodeViewerStyle(textBox);

            textBox.Location = GetFieldLocation(field);
            textBox.Width = GetFieldWidth(field);
            textBox.Height = GetFieldHeight(field) * field.Window;
            string inputName = GetFieldInput(field);
            if (!string.IsNullOrEmpty(inputName))
            {
                textBox.Name = inputName.Replace('.', '_');
            }

            parent.Controls.Add(textBox);

            UpdateSize(parent, textBox);
        }

        private void ShowListBox(FormatField field, Control parent)
        {

            ListBox listBox = new ListBox();

            //listBox.Location = new Point(field.PropertyObject.X * widthUnitPixel, field.PropertyObject.Y * unitPixel);
            //listBox.Width = field.PropertyObject.Width * widthUnitPixel;
            //listBox.Height = field.PropertyObject.Height * field.Window * unitPixel;

            listBox.Location = GetFieldLocation(field);
            listBox.Width = GetFieldWidth(field);
            listBox.Height = GetFieldHeight(field) * field.Window;
            string inputName = GetFieldInput(field);
            if (!string.IsNullOrEmpty(inputName))
            {
                listBox.Name = inputName.Replace('.', '_');
            }
            parent.Controls.Add(listBox);

            UpdateSize(parent, listBox);
        }

        private void UpdateSize(Control parent, Control childControl)
        {
            if (parent.Width < childControl.Location.X + childControl.Width)
            {
                parent.Width = childControl.Location.X + childControl.Width;
            }
            if (parent.Height < childControl.Location.Y + childControl.Height)
            {
                parent.Height = childControl.Location.Y + childControl.Height;
            }
        }

        private Point GetFieldLocation(FormatField field)
        {
            Point point = new Point(0, 0);

            int pointX = 0;
            int pointY = 0;

            //Look into the  property first.
            if (!string.IsNullOrEmpty(field.Property))
            {
                if (field.PropertyObject.Items.ContainsKey("X"))
                {
                    pointX = field.PropertyObject.X * widthUnitPixel;
                }

                if (field.PropertyObject.Items.ContainsKey("Y"))
                {
                    pointY = field.PropertyObject.Y * unitPixel;
                }
            }

            //If no such field, then look into column and line.
            if (pointX == 0)
            {
                pointX = (field.Column - 1) * 2 * widthUnitPixel;
            }
            if (pointY == 0)
            {
                pointY = (field.Line - 1) * 2 * unitPixel;
            }
            point = new Point(pointX, pointY);
            return point;
        }

        private int GetFieldWidth(FormatField field)
        {
            if (!string.IsNullOrEmpty(field.Property))
            {
                return field.PropertyObject.Width * widthUnitPixel;
            }
            else
            {
                return field.Length * 2 * widthUnitPixel;
            }
        }

        private int GetFieldHeight(FormatField field)
        {
            if (!string.IsNullOrEmpty(field.Property))
            {
                return field.PropertyObject.Height * unitPixel;
            }
            else
            {
                return unitPixel;
            }
        }

        private string GetFieldInput(FormatField field)
        {
            string inputName = "";
            if (!string.IsNullOrEmpty(field.Property))
            {
                if (field.PropertyObject.Items.ContainsKey("Input"))
                {
                    inputName = field.PropertyObject.Items["Input"];
                }
            }
            else
            {
                inputName = field.Input;
            }

            return inputName;
        }

        private string GetFieldCaption(FormatField field)
        {
            string caption = "";
            if (!string.IsNullOrEmpty(field.Property))
            {
                if (field.PropertyObject.Items.ContainsKey("Caption"))
                {
                    caption = field.PropertyObject.Items["Caption"];
                }
            }
            else
            {
                caption = field.Output;
            }

            return caption;
        }

        protected void viewer_Click(Object sender, EventArgs args)
        {
            if (rtViewer.Visible)
            {
                rtViewer.Hide();
            }
        }

        protected void viewer_FormatCodeClick(Object sender, EventArgs args)
        {
            Scintilla scintilla = (sender as MenuItem).Parent.GetContextMenu().SourceControl as Scintilla;

            RADLexer lexer = new RADParser.RADLexer(scintilla.Text);
            lexer.Build();
            RADParser.RadParser parser = new RadParser(lexer.Tokens);
            string formatedStr = parser.ParseStatements().ToString(0);
            scintilla.Text = formatedStr;
        }

        private void viewer_DwellStart(Object sender, ScintillaNET.ScintillaMouseEventArgs args)
        {
            //MessageBox.Show(args.Position.ToString());
            string varStr = GetVariableFromPosition((sender as Scintilla).Text, args.Position);
            if (!string.IsNullOrEmpty(varStr))
            {
                //MessageBox.Show(varStr);
                rtViewer.Width = 500;
                rtViewer.Height = 200;
                int x = args.X + (sender as Scintilla).Location.X ;
                int y = args.Y + (sender as Scintilla).Location.Y + 5;
                if (x + rtViewer.Width > this.Width)
                {
                    x = this.Width - rtViewer.Width;
                }

                rtViewer.Location = new Point(x,y);

                rtViewer.VarName = varStr;
                rtViewer.Context = DebugTrace.PostContext;
                rtViewer.Display();
                if (rtViewer.Parent == null)
                {
                    this.Controls.Add(rtViewer);
                }
                rtViewer.Visible = true;
                rtViewer.BringToFront();
            }
        }

        private string GetVariableFromPosition(string content, int position)
        {
            string varName = string.Empty;
            Regex varReg = new Regex("(\\$[.a-zA-Z0-9]+$)");
            Regex varSecondReg = new Regex("([$.a-zA-Z0-9]+)");
            StringBuilder varStr = new StringBuilder();
            Match match = varReg.Match(content, 0, position + 1);
            if (match.Success)
            {
                varName = match.Captures[0].Value;

                Match secondMatch = varSecondReg.Match(content, position + 1);
                if (secondMatch.Success && secondMatch.Captures[0].Index == position + 1)
                {
                    varName += secondMatch.Captures[0].Value;
                }
            }
            return varName;
        }
        
        //private void ShowText
    }
}
