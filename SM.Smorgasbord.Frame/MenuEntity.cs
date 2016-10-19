using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace SM.Smorgasbord.Frame
{
    [Serializable]
    public class MenuEntity
    {
        private string text;
        private string dllName;
        private string className;
        private string method;
        private string tabText;

        private bool tabContained = true;

        private Collection<MenuEntity> children = new Collection<MenuEntity>();

        public string Text
        {
            get
            {
                return text;
            }
            set
            {
                text = value;
            }
        }

        public string DllName
        {
            get
            {
                return dllName;
            }
            set
            {
                dllName = value;
            }
        }

        public string ClassName
        {
            get
            {
                return className;
            }
            set
            {
                className = value;
            }
        }

        public string Method
        {
            get
            {
                return method;
            }
            set
            {
                method = value;
            }
        }

        public string TabText
        {
            get
            {
                return tabText;
            }
            set
            {
                tabText = value;
            }
        }

        public bool TabContained
        {
            get
            {
                return tabContained;
            }
            set
            {
                tabContained = value; 
            }
        }

        public Collection<MenuEntity> Children
        {
            get
            {
                return children;
            }
        }
    }
}
