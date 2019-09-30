using System;

namespace ProMassSpammer.Core.Exceptions
{
    public class AttachmentNotFoundException : Exception
    {
        public AttachmentNotFoundException()
        {
        }

        public AttachmentNotFoundException(string message)
            : base(message)
        {
        }

        public AttachmentNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}