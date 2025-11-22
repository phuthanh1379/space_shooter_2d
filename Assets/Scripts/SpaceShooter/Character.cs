using System;

namespace SpaceShooter
{
    [Serializable]
    public class Character
    {
        public int Health
        {
            get; set;
        }

        public string Name
        {
            get; set; 
        }

        public Character(int health, string name)
        {
            this.Health = health;
            this.Name = name;
        }
    }
}
