﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudyRouteClient.Models;
using StudyRouteLibrary.Entities;

namespace StudyRouteClient.Controllers
{
    public class CollegesController : Controller
    {
        private readonly StudyRouteHttpClient httpClient;

        public CollegesController(StudyRouteHttpClient client)
        {
            httpClient = client;
        }

        // GET: Colleges
        public async Task<IActionResult> Index()
        {
            return View(await httpClient.GetColleges());
        }

        // GET: Colleges/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var college = await httpClient.GetCollege(id.Value);
            if (college == null)
            {
                return NotFound();
            }

            return View(college);
        }

        // GET: Colleges/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Colleges/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Ratings,NumberOfPrograms,Address,City,Country")] College college)
        {
            if (ModelState.IsValid)
            {
                var isSuccess = await httpClient.PostCollege(college);
                return RedirectToAction(nameof(Index));
            }
            return View(college);
        }

        // GET: Colleges/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var college = await httpClient.GetCollege(id.Value);
            if (college == null)
            {
                return NotFound();
            }
            return View(college);
        }

        // POST: Colleges/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Ratings,NumberOfPrograms,Address,City,Country")] College college)
        {
            if (id != college.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var isSuccess = await httpClient.PutCollege(college);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CollegeExists(college.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(college);
        }

        // GET: Colleges/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var college = await httpClient.GetCollege(id.Value);
            if (college == null)
            {
                return NotFound();
            }

            return View(college);
        }

        // POST: Colleges/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await httpClient.DeleteCollege(id);
            return RedirectToAction(nameof(Index));
        }

        private bool CollegeExists(int id)
        {
            return httpClient.GetCollege(id) != null;
        }
    }
}
