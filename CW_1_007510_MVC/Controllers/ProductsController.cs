using CW_1_007510_MVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CW_1_007510_MVC.Controllers
{
    public class ProductsController : Controller
    {
        // GET: ProductsController
        public  string baseUrl = "http://ec2-3-142-153-99.us-east-2.compute.amazonaws.com";
        private string  getList = "/api/Products";
        public async Task<ActionResult> Index()
        {
            var list = new List<Product>();
            string content = null;

            var client = new HttpClient();
            var response = await client.GetAsync(baseUrl + getList);
            if (response.IsSuccessStatusCode)
            {
                content = await response.Content.ReadAsStringAsync();
                list =  JsonConvert.DeserializeObject<List<Product>>(content);
            }

            return View(list);
        }

        // GET: ProductsController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            Product item = await GetOne(id);
            
            return View(item);
        }

        // GET: ProductsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Product collection)
        {
            try
            {
                var client = new HttpClient();
                var json = JsonConvert.SerializeObject(collection);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(baseUrl+getList, data);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductsController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            Product item = await GetOne(id);
            return View(item);
        }

        // POST: ProductsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Product collection)
        {
            try
            {
                var client = new HttpClient();
                var json = JsonConvert.SerializeObject(collection);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PutAsync(baseUrl + getList + "/"+ id, data);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductsController/Delete/5
        public async  Task<ActionResult> Delete(int id)
        {
            Product item = await GetOne(id);
            return View(item);
        }

        // POST: ProductsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            try { 
                var client = new HttpClient();
                var response = await client.DeleteAsync(baseUrl + getList +"/" + id);
            
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public async Task<Product> GetOne(int id)
        {
            Product item = null;
            var client = new HttpClient();
            var response = await client.GetAsync(baseUrl + getList + "/" + id);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                item = JsonConvert.DeserializeObject<Product>(content);
            }
            return item;
        }
    }
}
