using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManage.Domain.Entities
{
    public class PaymentHistory
    {
        public Guid Id { get; private set; }
        public decimal Amount { get; private set; }
        public DateTime PaymentDate { get; private set; }
        private PaymentHistory() { }

        public PaymentHistory(decimal amount, DateTime paymentDate)
        {
            Id = Guid.NewGuid();
            Amount = amount;
            PaymentDate = paymentDate;
        }
    }
}