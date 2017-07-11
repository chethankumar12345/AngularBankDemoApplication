using System.Runtime.Serialization;

namespace StudentWebApi.Controllers.CustomModel
{
    [DataContract]
    public class Customer
    {
        [DataMember(Name = "CustomerId")]
        public int CustomerId { get; set; }
        [DataMember(Name = "Name")]
        public string Name { get; set; }
        [DataMember(Name = "MobileNumber")]
        public string MobileNumber { get; set; }
        [DataMember(Name = "EmailId")]
        public string EmailId { get; set; }

    }
}