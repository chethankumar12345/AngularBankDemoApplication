using System;

namespace StudentWebApi.Controllers.CustomModel
{
    public class Transaction
    {
        public int TransationId { get; set; }
        public Nullable<int> CustomerAccountId { get; set; }
        public Nullable<double> Amount { get; set; }
        public Nullable<System.DateTime> TransactionDate { get; set; }
        public Nullable<int> TransactionTypeId { get; set; }
    }
}