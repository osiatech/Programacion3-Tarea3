
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using PaymentDetails.Infrastructure.Context;
using PaymentDetails.Infrastructure.Models;

namespace PaymentDetailsRegister.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentDetailController : ControllerBase
    {
        private readonly PaymentDetailContext context;

        public PaymentDetailController(PaymentDetailContext context)
        {
            this.context = context;
        }

        private bool PaymentDetailExists(int id)
        {
            return (context.PaymentDetails?.Any(paymentDetail => paymentDetail.PaymentDetailId == id)).GetValueOrDefault();
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<PaymentDetail>> GetPaymentDetail(int id)
        {
            if (context.PaymentDetails == null)
            {
                return NotFound();
            }

            var paymentDetail = await context.PaymentDetails.FindAsync(id);

            if (paymentDetail == null)
            {
                return NotFound();
            }

            return paymentDetail;
        }


        [HttpGet("GetAllPaymentDetail")]
        public async Task<ActionResult<IEnumerable<PaymentDetail>>> GetAllPaymentDetails()
        {
            if (context.PaymentDetails == null)
            {
                return NotFound();
            }
            return await context.PaymentDetails.ToListAsync();
        }


        [HttpPost("PostPaymentDetail")]
        public async Task<ActionResult<PaymentDetail>> PostPaymentDetail(PaymentDetail paymentDetail)
        {
            if (context.PaymentDetails == null)
            {
                return Problem("Entity set 'PaymentDetailsContext.PaymentDetails' is null.");
            }
            context.PaymentDetails.Add(paymentDetail);
            await context.SaveChangesAsync();
            return Ok(await context.PaymentDetails.ToListAsync());
        }


        [HttpPut("UpdatePaymentDetail")]
        public async Task<IActionResult> UpdatePaymentDetail(int id, PaymentDetail paymentDetail)
        {
            if (id != paymentDetail.PaymentDetailId)
            {
                return BadRequest();
            }

            context.Entry(paymentDetail).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PaymentDetailExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Ok(await context.PaymentDetails.ToListAsync());
        }


        [HttpDelete("DeletePaymentDetail")]
        public async Task<IActionResult> DeletePaymentDetail(int id)
        {
            if (context.PaymentDetails == null)
            {
                return NotFound();
            }

            var paymentDetail = await context.PaymentDetails.FindAsync(id);

            if (paymentDetail == null)
            {
                return NotFound();
            }

            context.PaymentDetails.Remove(paymentDetail);

            await context.SaveChangesAsync();

            return Ok(await context.PaymentDetails.ToListAsync());
        }
    }
}