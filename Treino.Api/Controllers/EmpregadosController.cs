using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Treino.Domain.Models;
using Treino.Repository.Context;
using Treino.Repository.ViewModels;

namespace Treino.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpregadosController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetEmpregadosAsync([FromServices]TreinoContext context)
        {
            var empregados = await context
                .Empregados
                .AsNoTracking()
                .ToListAsync();

            return Ok(empregados);

        }

        [HttpGet("{matricula}")]
        public async Task<IActionResult> GetEmpregadosByIdAsync([FromServices] TreinoContext context, int matricula)
        {
            var empregados = await context
                .Empregados
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Matricula == matricula);

            return empregados == null 
                ? NotFound() 
                : Ok(empregados);

        }

        [HttpPost("empregado")]
        public async Task<IActionResult> PostAsync(
            [FromServices] TreinoContext context,
            [FromBody] CreateEmpregado model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var empregado = new Empregado
            {
                Matricula = model.Matricula,
                Nome = model.Nome,
                Sexo = model.Sexo
            };

            try
            {
                await context.Empregados.AddAsync(empregado);
                await context.SaveChangesAsync();
                return Created($"v1/empregados/{empregado.Matricula}", empregado);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPut("todos/{matricula}")]
        public async Task<IActionResult> PutAsync(
            [FromServices] TreinoContext context,
            [FromBody] CreateEmpregado model,
            [FromRoute] int matricula)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var empregado = await context.Empregados.FirstOrDefaultAsync(x => x.Matricula == matricula);

            if (empregado == null)
                return NotFound();

            try
            {
                empregado.Nome = model.Nome;

                context.Empregados.Update(empregado);
                await context.SaveChangesAsync();
                return Ok(empregado);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpDelete("todos/{matricula}")]
        public async Task<IActionResult> DeleteAsync(
            [FromServices] TreinoContext context,
            [FromRoute] int matricula)
        {
            var empregado = await context.Empregados.FirstOrDefaultAsync(x => x.Matricula == matricula);

            try
            {
                context.Empregados.Remove(empregado);
                await context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }



    }
}
