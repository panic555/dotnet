using System.ServiceModel;
using System.Xml.Linq;
using DotNetTest.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DotNetTest.Services;

[ServiceContract]
public interface IPersonSoapService
{
    [OperationContract]
    string Test(string s);

    [OperationContract]
    void XmlMethod(System.Xml.Linq.XElement xml);

    [OperationContract]
    SoapPerson TestPerson(SoapPerson person);
    
    [OperationContract]
    Person Save(SoapPerson person);
}