using System.Runtime.Serialization;

namespace StudentWebApi.Controllers.CustomModel
{
    [DataContract]
    public class Bank
    {
        [DataMember(Name = "BankId")]
        public int BankId { get; set; }
        [DataMember(Name = "Name")]
        public string Name { get; set; }
        [DataMember(Name = "IFSC")]
        public string IFSC { get; set; }
    }
}