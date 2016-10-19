using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using System.Windows.Forms;
using System.Reflection;
using Crownwood. Magic. Menus;

namespace SM.Smorgasbord.Frame
{
    public class MenuHandler
    {
        public MenuEntity LoadMenu()
        {
            FileStream connectionStream = new FileStream("MenuConfig.xml", FileMode.Open);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(MenuEntity));
            MenuEntity menu = null;
            try
            {
                menu = (MenuEntity)xmlSerializer.Deserialize(connectionStream);
                
            }
            catch (Exception ex)
            {
                menu = new MenuEntity();
            }
            finally
            {
                connectionStream.Close();
            }

            return menu;
        }

        public void SaveMenu(MenuEntity menu)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(MenuEntity));
            StreamWriter menuWriter = new StreamWriter("MenuConfig.xml");
            xmlSerializer.Serialize(menuWriter, menu);
            menuWriter.Close();
        }

        public void DisplayMenu(Crownwood. Magic. Controls. TabControl tabControl,MenuCommand topItem, MenuEntity menu)
        {
            MenuCommand childItem = new MenuCommand(menu. Text);

            if (menu.Children.Count == 0)
            {
                childItem.Tag = menu;
                childItem.Click += delegate(object sender, EventArgs e)
                {
                    MenuCommand menuItem = sender as MenuCommand;
                    MenuEntity menuEntity = menuItem.Tag as MenuEntity;

                    Assembly assembly;
                    Type type;
                    object obj;
                    string dir = System. AppDomain. CurrentDomain. BaseDirectory;
                    //Create assembly
                    assembly = Assembly.LoadFile(dir + menuEntity.DllName);
                    //Get type
                    type = assembly.GetType(menuEntity.ClassName);
                    //Create instance
                    obj = assembly.CreateInstance(menuEntity.ClassName);
                    //Get method info
                    MethodInfo method = type.GetMethod(menuEntity.Method);
                    //Invoke method.
                    try
                    {
                        if (menu. TabContained)
                        {
                            Form subForm = method. Invoke(obj, new Form[] { new Form() }) as Form;
                            if (subForm != null)
                            {
                                if (tabControl. TabPages[menu.TabText] == null)
                                {                                 
                                    tabControl. TabPages. Add(new Crownwood. Magic. Controls. TabPage(menu.TabText, subForm));
                                }
                                tabControl. TabPages[menu. TabText]. Selected = true;
                            }
                        }
                        else
                        {
                            method. Invoke(obj, new Form[] { new Form() });
                        }
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                };
            }
            topItem.MenuCommands.Add(childItem);

            foreach (MenuEntity nextMenu in menu.Children)
            {
                DisplayMenu(tabControl,childItem, nextMenu);
            }
        }
    }
}
