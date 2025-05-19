using System;

namespace GTETools.Models
{
    public class change_log
    {
        private string _ClassName;
        public string ClassName
        {
            get { return _ClassName; }
            set { _ClassName = value; }
        }

        private string _PropertyName;
        public string PropertyName
        {
            get { return _PropertyName; }
            set { _PropertyName = value; }
        }

        private string _PrimaryKey;
        public string PrimaryKey
        {
            get { return _PrimaryKey; }
            set { _PrimaryKey = value; }
        }

        private string _OldValue;
        public string OldValue
        {
            get { return _OldValue; }
            set { _OldValue = value; }
        }

        private string _NewValue;
        public string NewValue
        {
            get { return _NewValue; }
            set { _NewValue = value; }
        }

        private DateTime _DateChanged;
        public DateTime DateChanged
        {
            get { return _DateChanged; }
            set { _DateChanged = value; }
        }
    }
}
