using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDB.DataAccess.Model;
using MongoDbAspNetCore.DataAccess.Repo;
using MongoDbAspNetCore.Models;

namespace MongoDbAspNetCore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        ICustomerRepository _customerRepo;

        public HomeController(ILogger<HomeController> logger, ICustomerRepository customerRepo)
        {
            _logger = logger;
            _customerRepo = customerRepo;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = await _customerRepo.GetAllCustomers();
            return View(model);
        }
        [HttpGet]
        [ActionName("Get")]
        public async Task<IActionResult> GetCustomer(int customerID)
        {
            var customer = await _customerRepo.GetCustomer(customerID);
            return customer == null ? (IActionResult)new NotFoundResult() : View("GetCustomer", customer);
        }
        [HttpGet]
        public IActionResult Insert()
        {
            return View("Insert", new Customer());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[ActionName("Post")]
        public async Task<IActionResult> Insert([Bind(include: "CustomerID, Name, Age, Salary")]Customer customer)
        {
            if(ModelState.IsValid)
            {
                await _customerRepo.Create(customer);
                TempData["Message"] = "Customer Created Successfully";
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            return View("Insert", await _customerRepo.GetCustomer(id));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update([Bind(include: "CustomerID, Name, Age, Salary")]Customer customer)
        {
            if(ModelState.IsValid)
            {
                var customerInDB = await _customerRepo.GetCustomer(customer.CustomerID);
                if (customerInDB == null)
                    return new NotFoundResult();
                customer.Id = customerInDB.Id;
                await _customerRepo.Update(customer);
                TempData["Message"] = "Customer Updated Successfully";
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            return View("ConfirmDelete", await _customerRepo.GetCustomer(id));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var customerInDB = await _customerRepo.GetCustomer(id);
            if (customerInDB == null || !await _customerRepo.Delete(customerInDB.CustomerID))
                return new NotFoundResult();
            TempData["Meassage"] = "Customer Deleted Successfully";
            return RedirectToAction("Index");
        }
    }
}
