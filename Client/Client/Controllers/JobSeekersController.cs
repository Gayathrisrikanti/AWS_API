using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Client.Data;
using Client.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Text;

namespace Client.Controllers
{
    public class JobSeekersController : Controller
    {
        HttpClient client;
        string baseUrl;
        string apiKey;

        public JobSeekersController()
        {
            client = new HttpClient();
            baseUrl = "https://localhost:44386/api/JobSeekers/";
            //apiKey = "CmUTHfc7KaDihUAdyKlX7AdvjM3nGXEh";
        }

        // GET: JobSeekers
        public async Task<IActionResult> Index()
        {
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //client.DefaultRequestHeaders.Add("apikey", apiKey);
            IEnumerable<JobSeeker> jobSeekers = new List<JobSeeker>();

            HttpResponseMessage response = await client.GetAsync(baseUrl);
            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                // convert json to joboffers object
                jobSeekers = JsonConvert.DeserializeObject<IEnumerable<JobSeeker>>(json);
            }

            return View(jobSeekers);
        }

        // GET: JobSeekers/Details/5
        public async Task<IActionResult> Details(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobSeekers = await JobSeekerExists(id.ToString());

            if (jobSeekers == null)
            {
                return NotFound();
            }

            return View(jobSeekers);
        }

        // GET: JobSeekers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: JobSeekers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SeekerId,SeekerName,SeekerEmail,SeekerMajor,Skill,SeekerCity,SeekerCountry")] JobSeeker jobSeeker)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //client.DefaultRequestHeaders.Add("apikey", apiKey);

            if (ModelState.IsValid)
            {
                string json = JsonConvert.SerializeObject(jobSeeker);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(baseUrl, content);
                if (response.IsSuccessStatusCode)
                    return RedirectToAction(nameof(Index));

            }
            return View(jobSeeker);
        }

        // GET: JobSeekers/Edit/5
        public async Task<IActionResult> Edit(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobSeeker = await JobSeekerExists(id.ToString());
            if (jobSeeker == null)
            {
                return NotFound();
            }
            return View(jobSeeker);
        }

        // POST: JobSeekers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("SeekerId,SeekerName,SeekerEmail,SeekerMajor,Skill,SeekerCity,SeekerCountry")] JobSeeker jobSeeker)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
           // client.DefaultRequestHeaders.Add("apikey", apiKey);

            if (id != jobSeeker.SeekerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                string url = baseUrl + id.ToString();
                string json = JsonConvert.SerializeObject(jobSeeker);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PutAsync(url, content);
                if (response.IsSuccessStatusCode)
                    return RedirectToAction(nameof(Index));
            }
            return View(jobSeeker);
        }

        // GET: JobSeekers/Delete/5
        public async Task<IActionResult> Delete(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobSeeker = await JobSeekerExists(id.ToString());

            if (jobSeeker == null)
            {
                return NotFound();
            }

            return View(jobSeeker);
        }

        // POST: JobSeekers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //client.DefaultRequestHeaders.Add("apikey", apiKey);

            string url = baseUrl + id.ToString();
            //HttpResponseMessage response = 
            await client.DeleteAsync(url);

            return RedirectToAction(nameof(Index));
        }

        private async Task <JobSeeker> JobSeekerExists(string id)
        {
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
           // client.DefaultRequestHeaders.Add("apikey", apiKey);
            JobSeeker jobSeeker = new JobSeeker();

            HttpResponseMessage response = await client.GetAsync(baseUrl + id.ToString());
            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                // convert json to jobSeeker object
                jobSeeker = JsonConvert.DeserializeObject<JobSeeker>(json);
            }

            return jobSeeker;
        }
    }
}
