using BulkyWeb.Models;
using BulkyWeb.Service;
using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Options;
using Razorpay.Api;
using QuestPDF.Previewer;
using QuestPDF.Fluent;
namespace BulkyWeb.Controllers
{
    public class PaymentController : Controller
    {
        private readonly RazorpaySettings _razorpay;
        private readonly IConverter _converter;
        public PaymentController(IOptions<RazorpaySettings> razorpay, IConverter converter)
        {
            _razorpay = razorpay.Value;
            _converter = converter;
        }
        public IActionResult Index()
        {
            return View();
        }
        public ActionResult CreateOrder(int amount)
        {
            RazorpayClient client = new RazorpayClient(_razorpay.Key, _razorpay.Secret);

            Dictionary<string, object> options = new Dictionary<string, object>
            {
                { "amount", amount * 100 }, // Convert ₹ to paise
                { "currency", "INR" },
                { "receipt", Guid.NewGuid().ToString()},
                { "payment_capture", 1 }
            };

            Order order = client.Order.Create(options);

            ViewBag.OrderId = order["id"];
            ViewBag.RazorpayKey = _razorpay.Key;
            ViewBag.Amount = amount;

            return View("Payment");
        }

        public IActionResult VerifyPayment(string paymentId, string orderId)
        {
            RazorpayClient client = new RazorpayClient(_razorpay.Key, _razorpay.Secret);
            Payment payment = client.Payment.Fetch(paymentId);

            ViewBag.Message = payment["status"].ToString() == "captured" ? "Payment Successful!" : "Payment Failed!";
            ViewBag.PaymentId = payment["id"];
            ViewBag.OrderId = payment["order_id"];
            ViewBag.Amount = (decimal)payment["amount"] / 100; // in rupees
            ViewBag.Email = payment["email"];
            ViewBag.Contact = payment["contact"];
            ViewBag.Status = payment["status"];
            ViewBag.Method = payment["method"];
            ViewBag.CreatedAt = payment["created_at"];

            return View("Result");
        }

        [HttpPost("Payment/DownloadPdf")]
        public IActionResult DownloadPdf(string paymentId, string orderId, decimal amount, string status, long createdAtUnix)
        {
            DateTime createdAt = DateTimeOffset.FromUnixTimeSeconds(createdAtUnix).LocalDateTime;

            var document = new PaymentReceiptDocument(paymentId, orderId, amount, status, createdAt);
            byte[] pdfBytes = document.GeneratePdf();

            return File(pdfBytes, "application/pdf", $"PaymentReceipt_{paymentId}.pdf");
        }

    }
}