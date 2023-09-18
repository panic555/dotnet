using System.Runtime.Serialization;

namespace DotNetTest.Entities;

[DataContract]
public class SoapPerson
{
    [DataMember]
    public string Name { get; set; }
    
    [DataMember]
    public string Surname { get; set; }
    
    [DataMember]
    public string Username { get; set; }
}