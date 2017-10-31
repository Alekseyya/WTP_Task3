using System.Runtime.Serialization;


namespace Model.DataContract
{
    [DataContract]
    public class ClientContract
    {
        [DataMember(IsRequired = true)]
        public string FirstName { get; set; }

        [DataMember(IsRequired = true)]
        public string LastName { get; set; }

        [DataMember(IsRequired = true)]
        public int Age { get; set; }
        
    }
}
