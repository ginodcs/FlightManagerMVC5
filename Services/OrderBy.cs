using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace ARQ.Maqueta.Services
{
    public class OrderBy
    {
        private string _property;

        public string Property
        {
            get { return _property; }
            set { _property = value; }
        }

        private bool _ascending;

        public bool Ascending
        {
            get { return _ascending; }
            set { _ascending = value; }
        }

        public OrderBy(string propertyName, bool ascending = true)
        {
            _property = propertyName;
            _ascending = ascending;
        }
    }
}

