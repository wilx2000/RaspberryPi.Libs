﻿using System;
using System.Collections.Generic;
using System.Text;

namespace RaspberryPi.PiGPIO
{
    public struct gpioPulse_t
    {
        public uint gpioOn;
        public uint gpioOff;
        public uint usDelay;

        public gpioPulse_t(uint gpioOn, uint gpioOff, uint usDelay) : this()
        {
            this.gpioOn = gpioOn;
            this.gpioOff = gpioOff;
            this.usDelay = usDelay;
        }
    }
}
