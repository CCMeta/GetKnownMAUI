using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GetKnownAPI.Models;

namespace GetKnownAPI.Controllers
{
    [Route("api/Exams/{exams_id}/Questions")]
    [ApiController]
    public class ExamQuestionsController : DefaultController
    {
        private readonly ExamQuestionsRepository _ExamQuestionsRepository;

        public ExamQuestionsController(ExamQuestionsRepository ExamQuestionsRepository, IHttpContextAccessor context) : base(context)
        {
            _ExamQuestionsRepository = ExamQuestionsRepository;
        }

        // GET: api/<CoursesController>
        [HttpGet]
        public async Task<IEnumerable<ExamQuestions>> GetAsync(int exams_id)
        {
            return await _ExamQuestionsRepository.GetList(exams_id);
        }

        // GET api/<CoursesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<CoursesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<CoursesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CoursesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
