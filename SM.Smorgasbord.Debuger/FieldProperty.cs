using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SM.Smorgasbord.Debuger
{
    public class FieldProperty
    {
        public FieldProperty()
        {
 
        }

        public FieldProperty(string rawString)
        {
            if (!string.IsNullOrEmpty(rawString) && rawString.Trim().Length > 1)
            {
                int semicolonIndex = rawString.IndexOf(";");
                FieldType = rawString.Substring(0, semicolonIndex);
                string followingString = rawString.Substring(semicolonIndex + 1);

                string[] itemsStrings = followingString.Trim().Split('\t');
                foreach (string itemString in itemsStrings)
                {
                    if (!string.IsNullOrEmpty(itemString))
                    {
                        string itemName = itemString.Split('=')[0];
                        string itemValue = itemString.Split('=')[1];
                        if (itemName == "X")
                        {
                            X = Convert.ToInt32(itemValue);
                        }
                        else if (itemName == "Y")
                        {
                            Y = Convert.ToInt32(itemValue);
                        }
                        else if (itemName == "Width")
                        {
                            Width = Convert.ToInt32(itemValue);
                        }
                        else if (itemName == "Height")
                        {
                            Height = Convert.ToInt32(itemValue);
                        }
                        else
                        {
                            //Nothing to do.
                        }

                        if (items.Keys.Contains(itemName))
                        {
                            items[itemName] = itemValue;
                        }
                        else
                        {
                            items.Add(itemName, itemValue);
                        }
                    }
                }
            }
        }

        private Dictionary<string, string> items = new Dictionary<string, string>();
        public string FieldType
        {
            get;
            set;
        }

        public int X
        {
            get;
            set;
        }
        public int Y
        {
            get;
            set;
        }
        public int Width
        {
            get;
            set;
        }
        public int Height
        {
            get;
            set;
        }

        public Dictionary<string, string> Items
        {
            get
            {
                return items;
            }
        }
    }
}
