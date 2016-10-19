using System;
using System. Collections. Generic;
using System. Linq;
using System. Text;

namespace SM. Smorgasbord. PackageGenerator
{
    public class MoveInstructionInfo
    {
        private string title;
        private string author;
        private string unloadFileLocation;

        private UnloadEntity entity;

        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                title = value;
            }
        }

        public string Author
        {
            get
            {
                return author;
            }
            set
            {
                author = value;
            }
        }

        public string UnloadFileLocation
        {
            get
            {
                return unloadFileLocation;
            }
            set
            {
                unloadFileLocation = value;
            }
        }

        public UnloadEntity Entity
        {
            get
            {
                return entity;
            }
            set
            {
                entity = value;
            }
        }
    }
}
