namespace Model
{
    public class Medicine
    {
        private string id;
        private string name;

        public Therapy therapy;

        public string Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
    }
}