﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRR.UIP.HW5.OOP.Factory
{
    class Worker
    {
        public string Name { get; private set; }
        public string Qualification { get; set; }
        public int Salary { get; private set; }
        public int Productivity
        {
            get
            {
                switch (this.Qualification)
                {
                    case "newbie":
                        return 1;
                    case "profi":
                        return 3;
                    case "master":
                        return 5;
                    default:
                        return 0;
                }
                
            }
        }
        public int WorkCounter { get; private set; }

        public Worker(string name, string qualification, int salary)
        {
            this.Name = name;
            this.Salary = salary;
            InitializeQualification(qualification);
            this.WorkCounter = 0;
        }
        public void DoWork(Car takenCar)
        {
            takenCar.BuildAggregate();
            takenCar.IncreaseProductionCosts(this.Salary);
            GetTired();
        }

        public void DoWork(Tank takenTank)
        {
            takenTank.BuildAggregate();
            takenTank.IncreaseProductionCosts(this.Salary);
            GetTired();
        }
        private void InitializeQualification(string qualification)
        {
            string[] qualificationUsed = new string[]
            {
                "newbie",
                "profi",
                "master"
            };

            if (qualification == null)
            {
                this.Qualification = "newbie";
                return;
            }
            foreach (var qualificationItem in qualificationUsed)
            {
                if (qualification.Equals(qualificationItem))
                {
                    this.Qualification = qualification;
                    return;
                }
            }
            this.Qualification = "newbie";
        }

        private void GetTired()
        {
            this.WorkCounter = (this.WorkCounter + 1) % this.Productivity;
        }


    }
}
