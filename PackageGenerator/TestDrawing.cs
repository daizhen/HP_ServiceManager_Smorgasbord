using System;
using System. Collections. Generic;
using System. ComponentModel;
using System. Data;
using System. Drawing;
using System. Linq;
using System. Text;
using System. Windows. Forms;
using System. Drawing. Drawing2D;
using System. Drawing. Imaging;

namespace SM. Smorgasbord. PackageGenerator
{
    public partial class TestDrawing : Form
    {
        public TestDrawing()
        {
            InitializeComponent();
        }

        private void TestDrawing_Paint(object sender, PaintEventArgs e)
        {
            UnloadEntity entity = new UnloadEntity();
            entity. Unload = true;
            entity. Name = "MS.R3.5 QCR19248 - Electronic Signature Control";
            for (int i = 0; i < 3; i++)
            {
                UnloadItem item = new UnloadItem();
                item. ElementName = i. ToString() ;
                item. Query = "activity=\"Item Verification Failed\" and notif.for.company=\"DEFAULT\" and language=\"en\" and module.identifier=\"SM\"";
                entity. Items. Add(item);
            }
            Image image =  CreateUnloadImage(entity);
            image. Save("test.jpg");
            e. Graphics. DrawImage(image, 0, 0);
        }

        private Image CreateUnloadImage(UnloadEntity entity)
        {
            int width = 1000;
            int height = 80 + 20 * (entity. Items. Count + 1);
            System. Drawing. Font textFont = new System. Drawing. Font("Helvetica", 10, FontStyle. Regular, GraphicsUnit. Pixel);
            System. Drawing. Font imgFont = new System. Drawing. Font("Helvetica", 16, FontStyle. Bold, GraphicsUnit. Pixel);

            Image image = new Bitmap(width, height, PixelFormat. Format32bppRgb);
            Graphics graphics = Graphics. FromImage(image);
            graphics. CompositingQuality = CompositingQuality. AssumeLinear;
            graphics. SmoothingMode = SmoothingMode. HighQuality;
            graphics. TextRenderingHint = System. Drawing. Text. TextRenderingHint. AntiAlias;
            graphics. InterpolationMode = InterpolationMode. HighQualityBicubic;
            graphics. PixelOffsetMode = PixelOffsetMode. HighQuality;
            graphics. FillRectangle(new SolidBrush(Color. White), 0, 0, width, height);

            Pen linePen = new Pen(Color. Black, 1);
            graphics. DrawRectangle(linePen, 0, 0, width - 2, height - 2);

            int nameLabelX = 40;
            int nameLabelY = 20;
            SolidBrush textBrush = new SolidBrush(Color. Black);
            SolidBrush backgroundBrush = new SolidBrush(Color. SandyBrown);
            graphics. DrawString("Unload Script:", textFont, textBrush, nameLabelX, nameLabelY, new StringFormat());
            graphics. DrawString("Unload Script:", textFont, textBrush, nameLabelX, nameLabelY, new StringFormat());
            int nameTextX = 150;
            int nameTextY = nameLabelY - 4;
            int nameTextWidth = 400;
            int nameTextHeight = 20;
            graphics. DrawRectangle(linePen, nameTextX, nameTextY, nameTextWidth, nameTextHeight);

            graphics. DrawString(entity. Name, textFont, textBrush, nameTextX + 5, nameLabelY, new StringFormat());

            string chooseStr = "√";

            int unloadCheckboxX = 40;
            int unloadCheckboxY = 50;
            int unloadCheckboxWidth = 15;
            graphics. DrawRectangle(linePen, unloadCheckboxX, unloadCheckboxY, unloadCheckboxWidth, unloadCheckboxWidth);

            if (entity. Unload)
            {
                graphics. DrawString(chooseStr, imgFont, textBrush, unloadCheckboxX, unloadCheckboxY);
                graphics. DrawString(chooseStr, imgFont, textBrush, unloadCheckboxX, unloadCheckboxY);
            }

            int unloadTextX = 60;
            int unloadTextY = 50;
            graphics. DrawString("Unload?", textFont, textBrush, unloadTextX, unloadTextY);

            int purgeCheckboxX = 150;
            int purgeCheckboxY = 50;
            graphics. DrawRectangle(linePen, purgeCheckboxX, purgeCheckboxY, unloadCheckboxWidth, unloadCheckboxWidth);

            if (entity. Purge)
            {
                graphics. DrawString(chooseStr, imgFont, textBrush, purgeCheckboxX, purgeCheckboxY);
                graphics. DrawString(chooseStr, imgFont, textBrush, purgeCheckboxX, purgeCheckboxY);
            }
            int purgeTextX = 180;
            int purgeTextY = 50;
            graphics. DrawString("Purge?", textFont, textBrush, purgeTextX, purgeTextY);


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
            graphics. DrawRectangle(linePen, firstCellX, firstCellY, firstColumnWidth, rowHeight);
            graphics. FillRectangle(backgroundBrush, firstCellX, firstCellY, firstColumnWidth, rowHeight);
            graphics. DrawString("FileName", textFont, textBrush, firstCellX + 4, firstCellY + 4);

            graphics. DrawRectangle(linePen, secondCellX, secondCellY, secondColumnWidth, rowHeight);
            graphics. FillRectangle(backgroundBrush, secondCellX, secondCellY, secondColumnWidth, rowHeight);
            graphics. DrawString("Query", textFont, textBrush, secondCellX + 4, secondCellY + 4);

            graphics. DrawRectangle(linePen, thirdCellX, thirdCellY, thirdColumnWidth, rowHeight);
            graphics. FillRectangle(backgroundBrush, thirdCellX, thirdCellY, thirdColumnWidth, rowHeight);
            graphics. DrawString("Datamap", textFont, textBrush, thirdCellX + 4, thirdCellY + 4);

            for (int i = 0; i < entity. Items. Count; i++)
            {
                UnloadItem currentItem = entity. Items[i];

                firstCellX = 2;
                firstCellY = 80 + rowHeight * (i + 1);

                secondCellX = firstCellX + firstColumnWidth;
                secondCellY = firstCellY;

                thirdCellX = secondCellX + secondColumnWidth;
                thirdCellY = firstCellY;

                graphics. DrawRectangle(linePen, firstCellX, firstCellY, firstColumnWidth, rowHeight);
                graphics. DrawString(currentItem. ElementName, textFont, textBrush, firstCellX + 4, firstCellY + 4);

                graphics. DrawRectangle(linePen, secondCellX, secondCellY, secondColumnWidth, rowHeight);
                graphics. DrawString(currentItem. Query, textFont, textBrush, secondCellX + 4, secondCellY + 4);

                graphics. DrawRectangle(linePen, thirdCellX, thirdCellY, thirdColumnWidth, rowHeight);

            }

            graphics. Flush();
            graphics. Dispose();
            return image;
        }

    }
}
