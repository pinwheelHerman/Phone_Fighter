namespace Assets.Scripts
{
    public struct Player
    {
        public enum Side
        {
            Left,
            Right
        }

        public enum Feet
        {
            Ground,
            Air
        }

        public enum Attack
        {
            None,
            Block,
            LungeForward,
            LungeUp,
            KickDown
        }

        private bool canAttack;
        private int position;
        private Side side;
        private Feet feet;
        private Attack attack;

        public bool GetCanAttack()
        {
            return canAttack;
        }

        public void SetCanAttack(bool value)
        {
            canAttack = value;
        }

        public int GetPosition()
        {
            return position;
        }

        public void SetPosition(int value)
        {
            position = value;
        }

        public Side GetSide()
        {
            return side;
        }

        public void SetSide(Side value)
        {
            side = value;
        }

        public Feet GetFeet()
        {
            return feet;
        }

        public void SetFeet(Feet value)
        {
            feet = value;
        }

        public Attack GetAttack()
        {
            return attack;
        }

        public void SetAttack(Attack value)
        {
            attack = value;
        }
    }
}
