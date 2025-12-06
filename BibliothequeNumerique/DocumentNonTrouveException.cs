using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliothequeNumerique
{
        public class DocumentNonTrouveException : Exception
        {
            public DocumentNonTrouveException(string message) : base(message)
            {
            }

            public DocumentNonTrouveException(string message, Exception innerException)
                : base(message, innerException)
            {
            }
        }
    
}
