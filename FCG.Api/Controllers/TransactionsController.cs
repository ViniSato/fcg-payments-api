using FCG.Api.Models.Requests;
using FCG.Api.Models.Responses;
using FCG.Api.Services.Mappers;
using FCG.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace FCG.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionService _transactionService;
        private readonly ILogService _logService;

        public TransactionsController(ITransactionService transactionService, ILogService logService)
        {
            _transactionService = transactionService;
            _logService = logService;
        }

        [HttpPost("process")]
        public async Task<ActionResult<TransactionResponse>> ProcessPayment([FromBody] TransactionRequest request)
        {
            try
            {
                await LogAsync("Info", $"Processing payment request - Amount: {request.Amount} {request.Currency}, PaymentMethod: {request.PaymentMethod}");
                
                var transactionDTO = TransactionMapper.ToDTO(request);
                var result = await _transactionService.ProcessPaymentAsync(transactionDTO);
                var response = TransactionMapper.ToResponse(result);

                await LogAsync("Info", $"Payment processed successfully - TransactionId: {response.TransactionId}");
                return CreatedAtAction(nameof(GetTransaction), new { id = response.Id }, response);
            }
            catch (ArgumentException ex)
            {
                await LogAsync("Warning", $"Invalid payment request: {ex.Message}");
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                await LogAsync("Error", $"Payment processing failed: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    new { message = "An error occurred while processing the payment", detail = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TransactionResponse>> GetTransaction(int id)
        {
            try
            {
                await LogAsync("Info", $"Retrieving transaction with ID: {id}");
                
                var transactionDTO = await _transactionService.GetTransactionAsync(id);
                var response = TransactionMapper.ToResponse(transactionDTO);
                return Ok(response);
            }
            catch (KeyNotFoundException ex)
            {
                await LogAsync("Warning", $"Transaction not found: {ex.Message}");
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpGet("by-id/{transactionId}")]
        public async Task<ActionResult<TransactionResponse>> GetTransactionById(string transactionId)
        {
            try
            {
                await LogAsync("Info", $"Retrieving transaction with TransactionId: {transactionId}");
                
                var transactionDTO = await _transactionService.GetTransactionByIdAsync(transactionId);
                if (transactionDTO == null)
                {
                    await LogAsync("Warning", $"Transaction not found with TransactionId: {transactionId}");
                    return NotFound(new { message = "Transaction not found" });
                }

                var response = TransactionMapper.ToResponse(transactionDTO);
                return Ok(response);
            }
            catch (Exception ex)
            {
                await LogAsync("Error", $"Error retrieving transaction by ID: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    new { message = ex.Message });
            }
        }

        [HttpGet("status/{status}")]
        public async Task<ActionResult<List<TransactionResponse>>> GetTransactionsByStatus(string status)
        {
            try
            {
                await LogAsync("Info", $"Retrieving transactions with status: {status}");
                
                var transactionDTOs = await _transactionService.GetTransactionsByStatusAsync(status);
                var responses = transactionDTOs.Select(TransactionMapper.ToResponse).ToList();
                
                await LogAsync("Info", $"Retrieved {responses.Count} transactions with status: {status}");
                return Ok(responses);
            }
            catch (Exception ex)
            {
                await LogAsync("Error", $"Error retrieving transactions by status: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    new { message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<TransactionResponse>>> GetAllTransactions()
        {
            try
            {
                await LogAsync("Info", "Retrieving all transactions");
                
                var transactionDTOs = await _transactionService.GetAllTransactionsAsync();
                var responses = transactionDTOs.Select(TransactionMapper.ToResponse).ToList();
                
                await LogAsync("Info", $"Retrieved {responses.Count} transactions");
                return Ok(responses);
            }
            catch (Exception ex)
            {
                await LogAsync("Error", $"Error retrieving all transactions: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    new { message = ex.Message });
            }
        }

        [HttpGet("customer/{customerId}")]
        public async Task<ActionResult<List<TransactionResponse>>> GetTransactionsByCustomerId(string customerId)
        {
            try
            {
                await LogAsync("Info", $"Retrieving transactions for customer: {customerId}");
                
                var transactionDTOs = await _transactionService.GetTransactionsByCustomerIdAsync(customerId);
                var responses = transactionDTOs.Select(TransactionMapper.ToResponse).ToList();
                
                await LogAsync("Info", $"Retrieved {responses.Count} transactions for customer: {customerId}");
                return Ok(responses);
            }
            catch (Exception ex)
            {
                await LogAsync("Error", $"Error retrieving transactions by customer: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    new { message = ex.Message });
            }
        }

        [HttpGet("date-range")]
        public async Task<ActionResult<List<TransactionResponse>>> GetTransactionsByDateRange(
            [FromQuery] DateTime startDate, 
            [FromQuery] DateTime endDate)
        {
            try
            {
                await LogAsync("Info", $"Retrieving transactions from {startDate:yyyy-MM-dd} to {endDate:yyyy-MM-dd}");
                
                var transactionDTOs = await _transactionService.GetTransactionsByDateRangeAsync(startDate, endDate);
                var responses = transactionDTOs.Select(TransactionMapper.ToResponse).ToList();
                
                await LogAsync("Info", $"Retrieved {responses.Count} transactions in date range");
                return Ok(responses);
            }
            catch (Exception ex)
            {
                await LogAsync("Error", $"Error retrieving transactions by date range: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    new { message = ex.Message });
            }
        }

        [HttpPut("{id}/status")]
        public async Task<ActionResult<TransactionResponse>> UpdateTransactionStatus(int id, [FromQuery] string status)
        {
            try
            {
                await LogAsync("Info", $"Updating transaction status - ID: {id}, NewStatus: {status}");
                
                var transactionDTO = await _transactionService.UpdateTransactionStatusAsync(id, status);
                var response = TransactionMapper.ToResponse(transactionDTO);
                
                await LogAsync("Info", $"Transaction status updated successfully - ID: {id}, Status: {status}");
                return Ok(response);
            }
            catch (KeyNotFoundException ex)
            {
                await LogAsync("Warning", $"Transaction not found for status update: {ex.Message}");
                return NotFound(new { message = ex.Message });
            }
            catch (ArgumentException ex)
            {
                await LogAsync("Warning", $"Invalid status for update: {ex.Message}");
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                await LogAsync("Error", $"Error updating transaction status: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    new { message = ex.Message });
            }
        }

        [HttpDelete("{id}/cancel")]
        public async Task<ActionResult> CancelTransaction(int id)
        {
            try
            {
                await LogAsync("Info", $"Cancelling transaction - ID: {id}");
                
                var result = await _transactionService.CancelTransactionAsync(id);
                if (!result)
                {
                    await LogAsync("Warning", $"Transaction not found for cancellation: {id}");
                    return NotFound(new { message = "Transaction not found" });
                }

                await LogAsync("Info", $"Transaction cancelled successfully - ID: {id}");
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                await LogAsync("Warning", $"Cannot cancel transaction: {ex.Message}");
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                await LogAsync("Error", $"Error cancelling transaction: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    new { message = ex.Message });
            }
        }

        private async Task LogAsync(string level, string message)
        {
            try
            {
                var logEntry = new FCG.Domain.Models.LogEntry
                {
                    Timestamp = DateTime.UtcNow,
                    Message = message,
                    Level = level,
                    Source = "TransactionsController"
                };

                await _logService.SaveAsync(logEntry);
            }
            catch
            {
            }
        }
    }
}
