using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuizBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace QuizBackend.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    //[ApiController]
    public class QuestionsController : ControllerBase
    {
        readonly QuizContext context;
        public QuestionsController (QuizContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IEnumerable<Question> Get()
        {
            //return new Question[] {
            //    new Question () { Text="Hello"},
            //    new Question () { Text="Hi"}

            //};
            return context.Questions;
        }

        [HttpGet("{quizID}")]
        public IEnumerable<Question> Get([FromRoute] int quizID )
        {           
            return context.Questions.Where(q=>q.QuizId == quizID);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Question question)
        {
            var quiz = context.Quiz.SingleOrDefault(q => q.ID == question.QuizId);
            if (quiz == null)
                return NotFound();

            context.Questions.Add(question);
             await context.SaveChangesAsync();
            return Ok(question);
        }


        [HttpPut("{id}")]
        public async  Task<IActionResult> Put(int id, [FromBody]Question question)
        {
            if (id != question.ID)
                return BadRequest();
            context.Entry(question).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return Ok(question);
        }

    }
}