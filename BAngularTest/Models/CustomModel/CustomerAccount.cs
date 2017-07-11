using System;

namespace StudentWebApi.Controllers.CustomModel
{
    public class CustomerAccount
    {
        public int CustomerAccountId { get; set; }
        public Nullable<int> CustomerId { get; set; }
        public Nullable<int> AccountTypeId { get; set; }
        public Nullable<System.DateTime> JoinDate { get; set; }
    }
}