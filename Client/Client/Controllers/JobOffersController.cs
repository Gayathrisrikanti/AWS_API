using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Client.Models;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using System.Net.Http.Headers;

namespace Client.Controllers
{
    public class JobOffersController : Controller
    {
        HttpClient client;
        string baseUrl;
        string apiKey;
      
        public JobOffersController()
        {
            client = new HttpClient();
            baseUrl = "https://localhost:44386/api/JobOffers/";
            //apiKey = "CmUTHfc7KaDihUAdyKlX7AdvjM3nGXEh";
        }

        // GET: JobOffers : Get All
        public async Task<IActionResult> Index()
        {
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //client.DefaultRequestHeaders.Add("apikey", apiKey);
            IEnumerable<JobOffer> jobOffers = new List<JobOffer>();

            HttpResponseMessage response = await client.GetAsync(baseUrl);
            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                // convert json to joboffers object
                jobOffers = JsonConvert.DeserializeObject<IEnumerable<JobOffer>>(json);
            }

            return View(jobOffers);
        }

       // GET: JobOffers/Details/5 : 
        public async Task<IActionResult> Details(string? id)
        {
           
            if (id == null)
            {
                return NotFound();
            }

            var jobOffer = await JobOfferExists(id.ToString());
         
            if (jobOffer == null)
            {
                return NotFound();
            }

            return View(jobOffer);
        }

        // GET: JobOffers/Create : POST
        public IActionResult Create()
        {
            return View();
        }

        // POST: JobOffers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("JobId,JobName,JobTitle,JobExperience,Skill,JobAddress,JobSalary")] JobOffer jobOffer)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
           // client.DefaultRequestHeaders.Add("apikey", apiKey);

            if (ModelState.IsValid)
            {
                string json = JsonConvert.SerializeObject(jobOffer);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(baseUrl, content);
                if (response.IsSuccessStatusCode)
                   return RedirectToAction(nameof(Index)); 
                
            }
            return View(jobOffer);
        }

        // GET: JobOffers/Edit/5 
        public async Task<IActionResult> Edit(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobOffer = await JobOfferExists(id.ToString());
            if (jobOffer == null)
            {
                return NotFound();
            }
            return View(jobOffer);
        }

        // POST: JobOffers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("JobId,JobName,JobTitle,JobExperience,Skill,JobAddress,JobSalary")] JobOffer jobOffer)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //client.DefaultRequestHeaders.Add("apikey", apiKey);

            if (id != jobOffer.JobId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                string url = baseUrl + id.ToString();
                string json = JsonConvert.SerializeObject(jobOffer);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PutAsync(url, content);
                if (response.IsSuccessStatusCode)
                    return RedirectToAction(nameof(Index));
            }
            return View(jobOffer);
        }

        // GET: JobOffers/Delete/5
        public async Task<IActionResult> Delete(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobOffer = await JobOfferExists(id.ToString());

            if (jobOffer == null)
            {
                return NotFound();
            }

            return View(jobOffer);
        }

        // POST: JobOffers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
           // client.DefaultRequestHeaders.Add("apikey", apiKey);

            string url = baseUrl + id.ToString();
            //HttpResponseMessage response = 
                await client.DeleteAsync(url);

            return RedirectToAction(nameof(Index));
        }

        //get by id
        private async Task<JobOffer> JobOfferExists(string id)
        {
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
           // client.DefaultRequestHeaders.Add("apikey", apiKey);
            JobOffer jobOffer = new JobOffer();

            HttpResponseMessage response = await client.GetAsync(baseUrl+id.ToString());
            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                // convert json to joboffers object
                jobOffer = JsonConvert.DeserializeObject<JobOffer>(json);
            }

            return jobOffer;
        }
    }
}
