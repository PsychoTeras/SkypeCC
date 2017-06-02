using System;

namespace SkypeCC.Entities
{
    class Message : DbObject
    {
        public bool FromMe { get; }
        public DateTime DateTime { get; }
        public string Text { get; private set; }

        public Message(int id, bool fromMe, DateTime dateTime, string text)
        {
            Id = id;
            FromMe = fromMe;
            DateTime = dateTime;
            Text = text;
        }

        public void SetText(string newText)
        {
            Text = newText;
        }

        public override string ToString()
        {
            return string.Format("[{0}] {1} {2}", Id, FromMe ? "<<<" : ">>>", Text);
        }
    }
}