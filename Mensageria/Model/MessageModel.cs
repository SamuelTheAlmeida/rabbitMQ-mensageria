using System;
using System.IO;

namespace Mensageria.Model
{
    public class MessageModel
    {
        public MessageModel()
        {

        }

        public Guid Id => Guid.NewGuid();
        public string ApplicationName { get; set; }
        public string Timestamp => DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

        public string Message { get; set; }
    }
}