using Microsoft.AspNetCore.Mvc;
using CRUDUsingASPCoreWebAPI.Models;
using System.Text;
using Newtonsoft.Json;

namespace CRUDUsingASPCoreWebAPI.Controllers
{
    public class StudentController : Controller
    {
        private string url = "https://localhost:7221/api/StudentAPI/";
        private HttpClient client = new HttpClient();

        [HttpGet]
        public IActionResult Index()
        {
            List<Student> students = new List<Student>();
            HttpResponseMessage response = client.GetAsync(url).Result;
            if (response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<List<Student>>(result);
                if (data != null)
                {
                    students = data;
                }
            }
            return View(students);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Student std)
        {
            // Check if the model state is valid
            if (!ModelState.IsValid) // Added validation check
            {
                return View(std); // Return the model to the view if there are validation errors
            }

            // Serialize the model to JSON
            string data = JsonConvert.SerializeObject(std);
            // Create StringContent for the POST request
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            // Changed from PostAsJsonAsync to PostAsync to match content type
            HttpResponseMessage response = client.PostAsync(url, content).Result;

            if (response.IsSuccessStatusCode)
            {
                TempData["insert_message"] = "Student Added Successfully";
                return RedirectToAction("Index");
            }
            return View(std); // Return the model to the view if the request is not successful
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            Student std = new Student();
            HttpResponseMessage response = client.GetAsync(url + id).Result;
            if(response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<Student>(result);
                if (data != null)
                {
                    std = data;
                }
            }
            return View(std);
        }
        [HttpPost]
        public IActionResult Edit(Student std)
        {
            // Check if the model state is valid
            if (!ModelState.IsValid) // Added validation check
            {
                return View(std); // Return the model to the view if there are validation errors
            }

            // Serialize the model to JSON
            string data = JsonConvert.SerializeObject(std);
            // Create StringContent for the POST request
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            // Changed from PostAsJsonAsync to PostAsync to match content type
            HttpResponseMessage response = client.PutAsync(url + std.id, content).Result;

            if (response.IsSuccessStatusCode)
            {
                TempData["update_message"] = "Student Updated Successfully";
                return RedirectToAction("Index");
            }
            return View(std); // Return the model to the view if the request is not successful
        }
        [HttpGet]
        public IActionResult Details(int id)
        {
            Student std = new Student();
            HttpResponseMessage response = client.GetAsync(url + id).Result;
            if (response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<Student>(result);
                if (data != null)
                {
                    std = data;
                }
            }
            return View(std);
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            Student std = new Student();
            HttpResponseMessage response = client.GetAsync(url + id).Result;
            if (response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<Student>(result);
                if (data != null)
                {
                    std = data;
                }
            }
            return View(std);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirm(int id)
        {
            Student std = new Student();
            HttpResponseMessage response = client.DeleteAsync(url + id).Result;
            if (response.IsSuccessStatusCode)
            {
                TempData["delete_message"] = "Student Deleted Successfully";
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
