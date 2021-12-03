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
    public class TreinamentosController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetTreinamentosAsync([FromServices] TreinoContext context)
        {
            var treinamento = await context
                .Treinamentos
                .AsNoTracking()
                .ToListAsync();

            return Ok(treinamento);

        }

        [HttpGet("{codigo}")]
        public async Task<IActionResult> GetTreinamentoByIdAsync([FromServices] TreinoContext context, int codigo)
        {
            var treinamento = await context
                .Treinamentos
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Codigo == codigo);

            return treinamento == null
                ? NotFound()
                : Ok(treinamento);

        }

        [HttpPost("treinamento")]
        public async Task<IActionResult> PostAsync(
            [FromServices] TreinoContext context,
            [FromBody] CreateTreinamento model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var treinamento = new Treinamento
            {
                Codigo = model.Codigo,
                Descricao = model.Descricao,
                CargaHoraria = model.CargaHoraria,
                Vagas = model.Vagas,
                Inicio = model.Inicio,
                Fim = model.Fim
                
            };

            try
            {
                await context.Treinamentos.AddAsync(treinamento);
                await context.SaveChangesAsync();
                return Created($"v1/treinamentos/{treinamento.Codigo}", treinamento);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPut("todos/{codigo}")]
        public async Task<IActionResult> PutAsync(
            [FromServices] TreinoContext context,
            [FromBody] CreateTreinamento model,
            [FromRoute] int codigo)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var treinamento = await context.Treinamentos.FirstOrDefaultAsync(x => x.Codigo == codigo);

            if (treinamento == null)
                return NotFound();

            try
            {
                treinamento.Descricao = model.Descricao;
                treinamento.Inicio = model.Inicio;
                treinamento.Fim = model.Fim;
                treinamento.CargaHoraria = model.CargaHoraria;
                treinamento.Vagas = model.Vagas;

                context.Treinamentos.Update(treinamento);
                await context.SaveChangesAsync();
                return Ok(treinamento);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpDelete("todos/{codigo}")]
        public async Task<IActionResult> DeleteAsync(
            [FromServices] TreinoContext context,
            [FromRoute] int codigo)
        {
            var treinamento = await context.Treinamentos.FirstOrDefaultAsync(x => x.Codigo == codigo);

            try
            {
                context.Treinamentos.Remove(treinamento);
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
