using Business_Card.Models;
using Business_Card.Models.Dtos;
using Business_Card.Services;
using Business_Card.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace Business_Card.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BusinessCardController : ControllerBase
    {
       private readonly IBusinessCard _businessCardSevices;

        public BusinessCardController(IBusinessCard businessCardSevices)
        {
            _businessCardSevices = businessCardSevices;
        }

        // GET: api/BusinessCard
        [HttpGet("GetAllBusinessCard")]
        public async Task<ActionResult> ViewAllBusinessCard()
        {
            var data = await _businessCardSevices.GetAllBusinessCard();
            return Ok(data);
        }   

        // POST: api/BusinessCard

        [HttpPost]
        [Route("AddBusinessCard")]
        public async Task<ActionResult> CreateBusinessCard( BusinessCardDto businessCardDto)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Validate Photo if provided
            if (!string.IsNullOrEmpty(businessCardDto.Photo))
            {
                if (!Base64Helper.IsValidBase64(businessCardDto.Photo))
                {
                    return BadRequest("Photo is not a valid Base64 string.");
                }

                var photoSize = Base64Helper.GetBase64DecodedSize(businessCardDto.Photo);
                if (photoSize > 1048576) // 1MB in bytes
                {
                    return BadRequest("Photo size exceeds 1MB.");
                }
            }


            var data = new BusinessCard
            {
                BusinessCard_Name = businessCardDto.BusinessCard_Name,
                BusinessCard_Email = businessCardDto.BusinessCard_Email,
                BusinessCard_PhoneNumber = businessCardDto.BusinessCard_PhoneNumber,
                BusinessCard_Address = businessCardDto.BusinessCard_Address,
                BusinessCard_Date_Of_Birth = businessCardDto.BusinessCard_Date_Of_Birth,
                BusinessCard_Gender = businessCardDto.BusinessCard_Gender,
                Photo = businessCardDto.Photo
            };

            await _businessCardSevices.CreateBusinessCard(data);
           
            return Ok(data);
        }

        // GET: api/BusinessCard/DELETE/5

        [HttpDelete]
        [Route("DeleteBusinessCardByID")]
        public async Task<ActionResult> DeleteBusinessCard(int id)
        {
            var data = await _businessCardSevices.GetById(id);
            if (data == null)
            {
                return NotFound();
            }

           _businessCardSevices.DeleteBusinessCard(data);
            return Ok(data);
        }




        // GET: api/BusinessCard/Export

        [HttpGet]
        [Route("ExportBusinessCard")]
        public async Task<IActionResult> ExportBusinessCards( string format)
        {
            if (string.IsNullOrEmpty(format))
            {
                return BadRequest("Format query parameter is required (csv or xml).");
            }

            var exportedData = await _businessCardSevices.ExportBusinessCards(format);

            if (exportedData == null)
            {
                return BadRequest("Invalid format. Supported formats are csv and xml.");
            }

            string mimeType;
            string fileName;

            if (format.ToLower() == "csv")
            {
                mimeType = "text/csv";
                fileName = "BusinessCards.csv";
            }
            else if (format.ToLower() == "xml")
            {
                mimeType = "application/xml";
                fileName = "BusinessCards.xml";
            }
            else
            {
                return BadRequest("Invalid format. Supported formats are csv and xml.");
            }

            return File(exportedData, mimeType, fileName);
        }



        [HttpPost]
        [Route("ImportBusinessCardFromFile")]
        public async Task<ActionResult> ImportBusinessCardFromFile([FromForm] IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file provided.");
            }

            string fileExtension = Path.GetExtension(file.FileName).ToLower();

            try
            {
                using (var stream = file.OpenReadStream())
                {
                    if (fileExtension == ".csv")
                    {
                        await _businessCardSevices.ImportBusinessCardsFromCsv(stream);
                    }
                    else if (fileExtension == ".xml")
                    {
                        await _businessCardSevices.ImportBusinessCardsFromXml(stream);
                    }
                    else
                    {
                        return BadRequest("Invalid file format. Supported formats are CSV and XML.");
                    }
                }

                return Ok(new { message = "Business cards imported successfully." });
            }
            catch (Exception ex)
            {
                // Log the exception (ex) as needed
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }







    }
}
