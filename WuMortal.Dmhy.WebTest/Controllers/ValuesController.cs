﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WuMortal.Dmhy.DataAnalysis.Interface;
using WuMortal.Dmhy.DataAnalysis.Models;

namespace WuMortal.Dmhy.WebTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        readonly IDmhyInfo _dmhyInfo;

        public ValuesController(IDmhyInfo dmhyInfo)
        {
            _dmhyInfo = dmhyInfo;
        }

        // GET api/values
        [HttpGet]
        public async Task<DHotPost[]> Get()
        {
            return await _dmhyInfo.GetHotPostAsync();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
