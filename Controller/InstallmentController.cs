using Microsoft.AspNetCore.Mvc;
using WebApplication1.Infrastructure;

namespace WebApplication1.Controller;

[ApiController]
[Route("[controller]")]
public class InstallmentController(InstallmentService installmentService) : ControllerBase
{
    [HttpGet("installment")]
    public ActionResult<string> Installment(string product, int price, string phoneNumber, int installment)
    {
        try
        {
            IResult result = installmentService.Installments(product, price, phoneNumber, installment); 
            return Ok(result);
        }
        catch (ArgumentException e)
        {
            return BadRequest(e.Message);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
}