using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace SD_Burger.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DropdownController : ControllerBase
    {
        [HttpGet("user-roles")]
        public IActionResult GetUserRoles()
        {
            var roles = new List<object>
            {
                new { Value = "Admin", Text = "Yönetici" },
                new { Value = "Waiter", Text = "Garson" },
                new { Value = "KitchenStaff", Text = "Mutfak Personeli" },
                new { Value = "ReservationManager", Text = "Rezervasyon Sorumlusu" },
                new { Value = "Accountant", Text = "Muhasebeci" },
                new { Value = "Customer", Text = "Müşteri" }
            };
            return Ok(roles);
        }

        [HttpGet("table-statuses")]
        public IActionResult GetTableStatuses()
        {
            var statuses = new List<object>
            {
                new { Value = "Available", Text = "Müsait" },
                new { Value = "Occupied", Text = "Dolu" },
                new { Value = "Reserved", Text = "Rezerve" },
                new { Value = "OutOfService", Text = "Servis Dışı" }
            };
            return Ok(statuses);
        }

        [HttpGet("ingredient-units")]
        public IActionResult GetIngredientUnits()
        {
            var units = new List<object>
            {
                new { Value = "Kilogram", Text = "Kilogram" },
                new { Value = "Gram", Text = "Gram" },
                new { Value = "Liter", Text = "Litre" },
                new { Value = "Milliliter", Text = "Mililitre" },
                new { Value = "Piece", Text = "Adet" },
                new { Value = "Package", Text = "Paket" },
                new { Value = "Box", Text = "Kutu" },
                new { Value = "Bottle", Text = "Şişe" }
            };
            return Ok(units);
        }

        [HttpGet("reservation-statuses")]
        public IActionResult GetReservationStatuses()
        {
            var statuses = new List<object>
            {
                new { Value = "Pending", Text = "Beklemede" },
                new { Value = "Confirmed", Text = "Onaylandı" },
                new { Value = "Cancelled", Text = "İptal Edildi" },
                new { Value = "Completed", Text = "Tamamlandı" }
            };
            return Ok(statuses);
        }

        [HttpGet("payment-methods")]
        public IActionResult GetPaymentMethods()
        {
            var methods = new List<object>
            {
                new { Value = "Cash", Text = "Nakit" },
                new { Value = "CreditCard", Text = "Kredi Kartı" },
                new { Value = "DebitCard", Text = "Banka Kartı" },
                new { Value = "OnlinePayment", Text = "Online Ödeme" }
            };
            return Ok(methods);
        }

        [HttpGet("payment-statuses")]
        public IActionResult GetPaymentStatuses()
        {
            var statuses = new List<object>
            {
                new { Value = "Pending", Text = "Beklemede" },
                new { Value = "Completed", Text = "Tamamlandı" },
                new { Value = "Failed", Text = "Başarısız" },
                new { Value = "Refunded", Text = "İade Edildi" }
            };
            return Ok(statuses);
        }

        [HttpGet("order-statuses")]
        public IActionResult GetOrderStatuses()
        {
            var statuses = new List<object>
            {
                new { Value = "Received", Text = "Alındı" },
                new { Value = "Preparing", Text = "Hazırlanıyor" },
                new { Value = "Ready", Text = "Hazır" },
                new { Value = "Served", Text = "Servis Edildi" },
                new { Value = "Cancelled", Text = "İptal Edildi" }
            };
            return Ok(statuses);
        }

        [HttpGet("order-priorities")]
        public IActionResult GetOrderPriorities()
        {
            var priorities = new List<object>
            {
                new { Value = "Normal", Text = "Normal" },
                new { Value = "Urgent", Text = "Acil" }
            };
            return Ok(priorities);
        }
    }
} 