using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pilkarzeMVVM.Model
{
    class Player
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public double Weight { get; set; }
        public int Age { get; set; }

        public Player() { }

        public Player(string name, string surname, double weight, int age)
        {
            Name = name;
            Surname = surname;
            Weight = weight;
            Age = age;
        }

        public override string ToString()
        {
            return String.Format($"Imię: {Name} Nazwisko: {Surname} Wiek: {Age} Waga: {Weight}");
        }

    }
}
