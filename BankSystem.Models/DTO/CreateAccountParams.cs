using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.Models.DTO
{
    public class CreateAccountParams
    {
        public Guid UserId { get; set; }
        public double InitialBalance { get; set; }

        public CreateAccountParams()
        {
            InitialBalance = 100;
        }
    }
}
