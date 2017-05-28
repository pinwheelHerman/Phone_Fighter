using UnityEngine;

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

        public enum LastSwipe
        {
            Left,
            Right,
            Up,
            Down
        }

        private bool canAttack;
        private int position;
        private int timesMoved;
        private Side side;
        private Feet feet;
        private Attack attack;
        private LastSwipe lastSwipe;

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

        public LastSwipe GetLastSwipe()
        {
            return lastSwipe;
        }

        public void SetLastSwipe(LastSwipe value)
        {
            lastSwipe = value;
        }

        public int GetTimesMoved()
        {
            return timesMoved;
        }

        public void SetTimesMoved(int value)
        {
            timesMoved = value;
        }
    }
}
