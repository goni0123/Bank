using System;

namespace TransactionSystem.ViewModels
{
    public class Bill
    {
        public int BillId { get; set; }
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public bool Status { get; set; }

        public Bill(int billId, string name, decimal amount, bool status)
        {
            BillId = billId;
            Name = name;
            Amount = amount;
            Status = status;
        }
    }
}
