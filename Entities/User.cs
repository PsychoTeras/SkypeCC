namespace SkypeCC.Entities
{
    class User : DbObject
    {
        public string Nick { get; }
        public string Name { get; private set; }

        public User(int id, string nick, string name)
        {
            Id = id;
            Nick = nick;
            Name = name;
        }

        public void UpdateName(string newName)
        {
            Name = newName;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
