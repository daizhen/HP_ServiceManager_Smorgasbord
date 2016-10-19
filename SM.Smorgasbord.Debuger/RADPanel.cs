using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Collections.ObjectModel;

namespace SM.Smorgasbord.Debuger
{
    [DataContract]
    public class RADPanel
    {
        private string radName;
        private string panelName;
        private string rawContent;
        [DataMember(Name = "application")]
        public string RADName
        {
            get
            {
                return radName;
            }
            set
            {
                radName = value;
            }
        }
        [DataMember(Name = "label")]
        public string PanelName
        {
            get
            {
                return panelName;
            }
            set
            {
                panelName = value;
            }
        }
        [DataMember(Name = "rawContent")]
        public string RawContent
        {
            get
            {
                return rawContent;
            }
            set
            {
                rawContent = value;
            }
        }

        [DataMember(Name = "comments")]
        public string Comments
        {
            get;
            set;
        }
        [DataMember(Name = "normal")]
        public string Normal
        {
            get;
            set;
        }
        [DataMember(Name = "error")]
        public string Error
        {
            get;
            set;
        }
        [DataMember(Name = "format")]
        public string Format
        {
            get;
            set;
        }
        [DataMember(Name = "file")]
        public string File
        {
            get;
            set;
        }
        [DataMember(Name = "all_null")]
        public string AllNull
        {
            get;
            set;
        }
        [DataMember(Name = "key_null")]
        public string KeyNull
        {
            get;
            set;
        }
        [DataMember(Name = "key_dupl")]
        public string KeyDupl
        {
            get;
            set;
        }
        [DataMember(Name = "second_file")]
        public string SecondFile
        {
            get;
            set;
        }
        [DataMember(Name = "target_file")]
        public string TargetFile
        {
            get;
            set;
        }
        [DataMember(Name = "record")]
        public string Record
        {
            get;
            set;
        }
        [DataMember(Name = "query")]
        public string Query
        {
            get;
            set;
        }
        [DataMember(Name = "name")]
        public string Name
        {
            get;
            set;
        }
        [DataMember(Name = "names")]
        public Collection<string> Names
        {
            get;
            set;
        }
        [DataMember(Name = "values")]
        public Collection<string> Values
        {
            get;
            set;
        }
        [DataMember(Name = "prompt")]
        public string Prompt
        {
            get;
            set;
        }
        [DataMember(Name = "condition")]
        public Collection<string> Conditions
        {
            get;
            set;
        }
        [DataMember(Name = "option")]
        public Collection<string> Option
        {
            get;
            set;
        }
        [DataMember(Name = "description")]
        public Collection<string> Description
        {
            get;
            set;
        }
        [DataMember(Name = "exit")]
        public Collection<string> Exit
        {
            get;
            set;
        }
        [DataMember(Name = "index")]
        public string Index
        {
            get;
            set;
        }
        [DataMember(Name = "empty")]
        public string Empty
        {
            get;
            set;
        }
        [DataMember(Name = "one_rec")]
        public string OneRec
        {
            get;
            set;
        }
        [DataMember(Name = "text")]
        public string Text
        {
            get;
            set;
        }
        [DataMember(Name = "statements")]
        public Collection<string> Statements
        {
            get;
            set;
        }
        [DataMember(Name = "cond_input")]
        public string CondInput
        {
            get;
            set;
        }
        [DataMember(Name = "sort")]
        public Collection<string> Sort
        {
            get;
            set;
        }
        [DataMember(Name = "types")]
        public Collection<string> Types
        {
            get;
            set;
        }
        [DataMember(Name = "levels")]
        public Collection<string> Levels
        {
            get;
            set;
        }
        [DataMember(Name = "numbers")]
        public Collection<string> Numbers
        {
            get;
            set;
        }
        [DataMember(Name = "number1")]
        public string Number1
        {
            get;
            set;
        }
        [DataMember(Name = "string1")]
        public string String1
        {
            get;
            set;
        }
        [DataMember(Name = "time1")]
        public string Time1
        {
            get;
            set;
        }
        [DataMember(Name = "boolean1")]
        public string Boolean1
        {
            get;
            set;
        }
        [DataMember(Name = "times")]
        public Collection<string> Times
        {
            get;
            set;
        }
        [DataMember(Name = "expressions")]
        public Collection<string> Expressions
        {
            get;
            set;
        }
        [DataMember(Name = "comments_more")]
        public Collection<string> CommentsMore
        {
            get;
            set;
        }
        [DataMember(Name = "file_variables")]
        public Collection<string> FileVariables
        {
            get;
            set;
        }
        [DataMember(Name = "second_record")]
        public string SecondRecord
        {
            get;
            set;
        }
        [DataMember(Name = "booleans")]
        public Collection<string> Booleans
        {
            get;
            set;
        }
        [DataMember(Name = "table")]
        public string Table
        {
            get;
            set;
        }
        [DataMember(Name = "tables")]
        public Collection<string> Tables
        {
            get;
            set;
        }

    }
}
