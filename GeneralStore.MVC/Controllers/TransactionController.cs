using GeneralStore.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace GeneralStore.MVC.Controllers
{
    public class TransactionController : Controller
    {
        private ApplicationDbContext _db = new ApplicationDbContext(); 

        // GET: Transaction
        public ActionResult Index()
        {
            List<Transaction> transactionList = _db.Transactions.ToList();
            List<Transaction> orderedList = transactionList.OrderBy(t => t.TimeStamp).ToList();
            return View(orderedList);
        }

        // GET: Create
        public ActionResult Create()
        {
            ViewBag.CustomerId = new SelectList(_db.Customers, "CustomerId", "FullName");
            ViewBag.ProductId = new SelectList(_db.Products, "ProductId", "Name");
            return View();
        }

        // POST: Create
        [HttpPost]
        public ActionResult Create(Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                //if (transaction.Product == null) return View(transaction);
                //if (transaction.Customer == null) return View(transaction);
                //if (transaction.PurchaseQuantity > transaction.Product.InventoryCount) return View(transaction);
                transaction.Product.InventoryCount -= transaction.PurchaseQuantity;
                transaction.TimeStamp = DateTime.Now;
                _db.Transactions.Add(transaction);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerId = new SelectList(_db.Customers, "CustomerId", "FullName", transaction.CustomerId);
            ViewBag.ProductId = new SelectList(_db.Products, "ProductId", "Name", transaction.ProductId);
            return View(transaction);
        }
    }
}