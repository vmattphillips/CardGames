namespace Models
{
    public class Card
    {
        // 11 Jack, 12 Queen, 13 King, 1 Ace
        private int _value;
        private Suit _suit;

        public Card(int v, Suit s)
        {
            this.SetSuit(s);
            this.SetValue(v);
        }

        // explicit getters and setters for ease of understanding
        public string GetValue()
        {
            if(this._value == 1)
            {
                return "Ace";
            }
            else if(this._value == 11)
            {
                return "Jack";
            }
            else if(this._value == 12)
            {
                return "Queen";
            }
            else if(this._value == 13)
            {
                return "King";
            }
            else
            {
                return this._value.ToString();
            }
        }

        public int GetValueAsInt()
        {
            return this._value;
        }

        public Suit GetSuit()
        {
            return this._suit;
        }

        public void SetValue(int v)
        {
            if(v > 0 && v <= 13)
            {
                this._value = v;
                return;
            }
        }

        public void SetSuit(Suit s)
        {
            this._suit = s;
        }

        override public string ToString()
        {
            return $"{this.GetValue()} of {this.GetSuit()}";
        }
    }

}