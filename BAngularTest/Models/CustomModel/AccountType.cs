using System.Runtime.Serialization;

namespace StudentWebApi.Controllers.CustomModel
{
    [DataContract]
    public class AccountType
    {
        [DataMember(Name = "AccountTypeId")]
        public int AccountTypeId { get; set; }
        [DataMember(Name = "AccountType")]
        public string AccountTypeName { get; set; }
        [DataMember(Name = "BankId")]
        public int? BankId { get; set; }
        [DataMember(Name = "Name")]
        public string BankName { get; set; }
    }
}