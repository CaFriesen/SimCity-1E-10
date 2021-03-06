﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly
{
    [Serializable]
    public class Player
    {
        private int position;
        private List<RealEstate> realEstates;
        public int Position
        {
            get { return position; }

            set
            {
                int newPosition = Position + value;
                if (newPosition >= 40 && newPosition <= 80)
                {
                    position = newPosition - 40;
                    Cash += 200;
                    LastRoll = value;
                }
                else
                {
                    if(newPosition >= 80)
                    {
                        throw new RollIsToBigException("Roll is to big"); //whoopsie daisy
                    }
                    else
                    {
                        LastRoll = value;
                        position = newPosition;
                    }
                }
            }
        }

        public int Cash { get; set; }
        public bool Jailed { get; set; }
        public int LastRoll { get; set; }
        public bool HasGetOutOfJailCard { get; set; }
        public IReadOnlyList<RealEstate> RealEstates
        {
            get { return realEstates.AsReadOnly(); }
        }

        public Player()
        {
            Cash = 1500;
            position = 0;
            LastRoll = 0;
            realEstates = new List<RealEstate>();
        }

        public void AddRealEstate(RealEstate estate)
        {
            realEstates.Add(estate);
        }
    }
}
