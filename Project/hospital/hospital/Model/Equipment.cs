namespace Model
{
    public class Equipment
    {
        public string type { get; set; }
        public int quantity { get; set; }

        public Equipment() { }
        public Equipment(string type, int quantity)
        {
            this.type = type;
            this.quantity = quantity;
        }

        public override string ToString()
        {
            return type + " " + quantity;
        }
    }
}