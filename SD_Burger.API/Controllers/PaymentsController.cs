using Microsoft.AspNetCore.Mvc;
using SD_Burger.Application.DTOs;
using SD_Burger.Application.Services;

namespace SD_Burger.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentsController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PaymentDto>>> GetAll()
        {
            try
            {
                var payments = await _paymentService.GetAllAsync();
                return Ok(payments);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving payments.", error = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PaymentDto>> GetById(int id)
        {
            try
            {
                var payment = await _paymentService.GetByIdAsync(id);
                return Ok(payment);
            }
            catch (ArgumentException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving the payment.", error = ex.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult<PaymentDto>> Create([FromBody] PaymentDto paymentDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var createdPayment = await _paymentService.CreateAsync(paymentDto);
                return CreatedAtAction(nameof(GetById), new { id = createdPayment.Id }, createdPayment);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while creating the payment.", error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<PaymentDto>> Update(int id, [FromBody] PaymentDto paymentDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var updatedPayment = await _paymentService.UpdateAsync(id, paymentDto);
                return Ok(updatedPayment);
            }
            catch (ArgumentException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while updating the payment.", error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _paymentService.DeleteAsync(id);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while deleting the payment.", error = ex.Message });
            }
        }
    }
} 