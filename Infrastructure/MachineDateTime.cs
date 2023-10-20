using System;
using SceletonAPI.Application.Interfaces;

namespace SceletonAPI.Infrastructure
{
    public class MachineDateTime : IDateTime
    {
        public DateTime Now => DateTime.Now;
        public int CurrentYear = DateTime.Now.Year;
    }
}
