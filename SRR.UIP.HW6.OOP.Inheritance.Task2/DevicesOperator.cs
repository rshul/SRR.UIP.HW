﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRR.UIP.HW6.OOP.Inheritance.Task2
{
    class DevicesOperator
    {
        public List<ElectricDevice> SupportedDevices { get; private set; }
        
        public bool IsLessThanTwoDevices
        {
            get
            {
                if (SupportedDevices == null || this.SupportedDevices.Count == 1)
                {
                    return true;
                }
                return false;
            }    
        }


        public DevicesOperator(List<ElectricDevice> devices)
        {
            this.SupportedDevices = devices;
        }

        public void ConnectDevices()
        {
            if (FindGenerator() != null)
            {
                ElectricDevice previousLink = (ElectricDevice)FindGenerator();
                if (!this.IsLessThanTwoDevices)
                {
                    foreach (var device in this.SupportedDevices)
                    {
                        if (device is ConsumptionDevice)
                        {
                            bool isNotPossibleToContinue = previousLink.PlugDevice((ConsumptionDevice)device);
                            if (isNotPossibleToContinue)
                            {
                                break;
                            }
                            
                        }
                    }
                }

            }
        }

        private Generator FindGenerator()
        {
            if (!this.IsLessThanTwoDevices)
            {
                foreach (var device in this.SupportedDevices)
                {
                    if (device is Generator)
                    {
                        return (Generator)device;
                    }
                }
                return null;

            }
            return null;
        }
    }
}
