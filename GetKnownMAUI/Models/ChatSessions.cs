using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace GetKnownMAUI.Models
{
    public class ChatSessions : MyContacts
    {
        [PrimaryKey, AutoIncrement]
        public int session_id { get; set; }
        public int Partner { get; set; }
        public int Unread { get; set; }
    }
}
