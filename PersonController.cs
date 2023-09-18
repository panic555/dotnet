using System.ComponentModel.DataAnnotations;
using System.Net.Http.Headers;
using System.Text;
using System.Xml;
using AutoMapper;
using DotNetTest.Controllers.Responses;
using DotNetTest.Entities;
using DotNetTest.Mappers;
using DotNetTest.Services;
using DotNetTest.Services.Commands;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DotNetTest.Controllers;

[ApiController]
[Route("persons")]
public class PersonController : ControllerBase
{
    private readonly ILogger<PersonController> _logger;
    private readonly IPersonService _personService;
    private readonly PersonResponseMapper _personResponseMapper;
    private readonly CreatePersonCommandMapper _createPersonCommandMapper;

    public PersonController(ILogger<PersonController> logger, IPersonService personService, PersonResponseMapper personResponseMapper, CreatePersonCommandMapper createPersonCommandMapper)
    {
        _logger = logger;
        _personService = personService;
        _personResponseMapper = personResponseMapper;
        _createPersonCommandMapper = createPersonCommandMapper;
    }

    [HttpGet("{id:int}")]
    //[Authorize]
    public IActionResult GetById(int id)
    {
        var person = _personService.GetById(id);
        var personResponse = _personResponseMapper.Map(person);

        if (person == null)
        {
            return NotFound();
        }

        return Ok(personResponse);
    }
    
    [HttpGet]
    public IActionResult GetAll(int page = 1, int pageSize = 10)
    {
        var persons = _personService.GetAll(page, pageSize);
        var personResponses = persons.Persons.Select(person => _personResponseMapper.Map(person));
        var personPagedResponse = new PersonPagedResponse()
        {
            PageSize = pageSize,
            PageNumber = page,
            Persons = personResponses,
            Count = persons.TotalCount
        };

        return Ok(personPagedResponse);
    }
    
    [HttpPost("create")]
    public IActionResult Create(CreatePersonCommand command)
    {
        if (_personService.GetAll().Any(person => person.Username == command.Username))
        {
            ModelState.AddModelError("Username", "Username must be unique.");
        }
        
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var validationContext = new ValidationContext(command);
        var validationResults = new List<ValidationResult>();
        if (!Validator.TryValidateObject(command, validationContext, validationResults, true))
        {
            foreach (var validationResult in validationResults)
            {
                ModelState.AddModelError(validationResult.MemberNames.FirstOrDefault(), validationResult.ErrorMessage);
            }

            return BadRequest(ModelState);
        }

        var person = _createPersonCommandMapper.Map(command);

        _personService.Create(person);

        return CreatedAtAction(nameof(GetById), new { id = person.Id }, person);
    }
    
    [HttpPut("update/{id:int}")]
    public IActionResult Update(int id, UpdatePersonCommand command)
    {
        try
        {
            var validationContext = new ValidationContext(command);
            var validationResults = new List<ValidationResult>();
            if (!Validator.TryValidateObject(command, validationContext, validationResults, true))
            {
                foreach (var validationResult in validationResults)
                {
                    ModelState.AddModelError(validationResult.MemberNames.FirstOrDefault(), validationResult.ErrorMessage);
                }

                return BadRequest(ModelState);
            }
        
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UpdatePersonCommand, Person>();
            });
        
            var mapper = configuration.CreateMapper();
            var person = mapper.Map<Person>(command);

            if (person == null)
            {
                return NotFound();
            }

            _personService.Update(person);
        
            var url = Url.Action("Update", "Person", new { id }, Request.Scheme, Request.Host.ToString());
        
            return Ok(url);
        }
        catch (Exception ex)
        {
            _logger.LogInformation("Couldn't update with an exception: ", ex);
        
            return StatusCode(500, "An error occurred while updating the person.");
        }
    }

    [HttpDelete("delete/{id:int}")]
    public IActionResult Delete(int id)
    {
        var person = _personService.GetById(id);
        
        if (person == null)
        {
            return NotFound();
        }

        _personService.Delete(person);

        return NoContent();
    }
    
    [HttpGet("{id}/drinkedamount")]
    public async Task<IActionResult> GetDrinkedAmount(int id)
    {
        var person = _personService.GetById(id);
        var personResponse = _personResponseMapper.Map(person);
        if (personResponse == null)
        {
            return NotFound();
        }
        
        var amountInCents = (int)(personResponse.AmountConsumed * 100);

        // SOAP request XML
        var soapRequestXml = $@"
            <soap:Envelope xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:tns=""http://www.dataaccess.com/webservicesserver/"">
                <soap:Body>
                    <tns:NumberToWords>
                        <tns:ubiNum>{amountInCents}</tns:ubiNum>
                    </tns:NumberToWords>
                </soap:Body>
            </soap:Envelope>";
        
        using (var client = new HttpClient())
        {
            // header
            client.DefaultRequestHeaders.Add("SOAPAction", "");

            // content-type
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/xml"));

            // SOAP request
            var response = await client.PostAsync("https://www.dataaccess.com/webservicesserver/numberconversion.wso",
                new StringContent(soapRequestXml, Encoding.UTF8, "text/xml"));

            if (response.IsSuccessStatusCode)
            {
                // Read the SOAP response XML
                var responseXml = await response.Content.ReadAsStringAsync();

                // Parse the XML
                var xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(responseXml);
                var namespaceManager = new XmlNamespaceManager(xmlDoc.NameTable);
                namespaceManager.AddNamespace("soap", "http://schemas.xmlsoap.org/soap/envelope/");
                namespaceManager.AddNamespace("tns", "http://www.dataaccess.com/webservicesserver/");

                var drinkedAmountNode = xmlDoc.SelectSingleNode("//tns:NumberToWordsResult", namespaceManager);
                if (drinkedAmountNode != null)
                {
                    var drinkedAmountInWords = drinkedAmountNode.InnerText;
                    return Ok(drinkedAmountInWords);
                }
            }
            
            return StatusCode((int)response.StatusCode, "Failed to retrieve drinked amount in words.");
        }
    }
    
    [HttpGet("persons/akira")]
    public async Task<IActionResult> GetAllPersons()
    {
        try
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri("https://api.example.com"); //akira link
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "YOUR_TOKEN"); //authorisation

                // Send the HTTP GET request to the "getAll" endpoint
                var response = await httpClient.GetAsync("/enpoint");
                
                if (response.IsSuccessStatusCode)
                {
                    // Read the response content as a string
                    var responseContent = await response.Content.ReadAsStringAsync();

                    //Instead of person, make an entity that akira has
                    var persons = JsonConvert.DeserializeObject<List<Person>>(responseContent);

                    // Map the akira objects to the desired response model (PersonResponse/Person)
                    var personResponses = persons.Select(person => _personResponseMapper.Map(person));

                    // Return the list of person responses
                    return Ok(personResponses);
                }

                return StatusCode((int)response.StatusCode, "An error occurred while retrieving the persons.");
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, "An error occurred while processing the request.");
        }
    }


}
