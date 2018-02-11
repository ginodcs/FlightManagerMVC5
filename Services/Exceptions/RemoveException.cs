using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ARQ.Maqueta.Services
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2237:MarkISerializableTypesWithSerializable")]
    public class RemoveException : Exception
    {
        private string message;

        public override string Message
        {
            get
            {
                return message;
            }
        }

        public RemoveException(string message)
        {
            this.message = message;
        }
    }
}
