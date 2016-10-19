﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace SM.Smorgasbord.RADParser
{
    public class Statements : BaseStatement
    {
        private Collection<BaseStatement> content = new Collection<BaseStatement>();

        public Collection<BaseStatement> Content
        {
            get
            {
                return content;
            }
        }

        public override string ToString()
        {

            StringBuilder str = new StringBuilder();
            int itemIndex = 0;
            int itemCount = content.Count;
            foreach (BaseStatement statement in content)
            {
                if (statement != null)
                {
                    str.Append(statement.ToString());
                }
                if (itemIndex < itemCount - 1)
                {
                    str.Append(";");
                }
                itemIndex++;
            }
            return str.ToString();
        }

        public override string ToString(int nestLevel)
        {
            StringBuilder str = new StringBuilder();
            int itemIndex = 0;
            int itemCount = content.Count;
            foreach (BaseStatement statement in content)
            {
                if (statement != null)
                {
                    str.Append(statement.ToString(nestLevel));
                }
                else
                {
                    str.Append("\r\n");
                }
                if (itemIndex < itemCount - 1)
                {
                    if (statement.Type != StatementType.Comments)
                    {
                        if (statement.Type == StatementType.Assignment)
                        {
                            AssignmentStatement assignmentStatement = statement as AssignmentStatement;
                            string leftString = assignmentStatement.LeftExpression.ToString();
                            if (leftString.CompareTo("$L.comments.AutoGenerated.byEDITOR") != 0)
                            {
                                str.Append(";");
                            }
                        }
                        else
                        {
                            str.Append(";");
                        }
                    }
                    str.Append("\r\n");
                }
                itemIndex++;
            }
            return str.ToString();
        }
    }
}
