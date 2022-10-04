using System.Linq;
using Prova.Models;
using Microsoft.AspNetCore.Mvc;
namespace Prova.Controllers
{
    [ApiController]
    [Route("api/folha")]
    public class FolhaController : ControllerBase
    {
        private readonly DataContext _context;
        public FolhaController(DataContext context) =>
            _context = context;

        // GET: /api/folha/listar
        [HttpGet]
        [Route("listar")]
        public IActionResult Listar() => Ok(_context.Folhas.ToList());

        // POST: /api/folha/cadastrar
        [HttpPost]
        [Route("cadastrar")]
        public IActionResult Cadastrar([FromBody] FolhaPagamento folha)
        {
            double sb = Calcular.SB(folha.QuantidadeHoras, folha.ValorHora);
            double ir = Calcular.IR(sb);
            double inss = Calcular.INSS(sb);
            folha.SalarioBruto = sb;
            folha.ImpostoRenda = ir;
            folha.ImpostoInss = inss;
            folha.ImpostoFgts = Calcular.FGTS(sb);
            folha.SalarioLiquido = Calcular.SL(sb, ir, inss);
            _context.Folhas.Add(folha);
            _context.SaveChanges();
            return Created("", folha);
        }

        // GET: /api/folha/buscar/{cpf}/{mes}/{ano}
        [HttpGet]
        [Route("buscar/{cpf}/{mes}/{ano}")]
        public IActionResult Buscar([FromRoute] string cpf, string mes, int ano)
        {
            FolhaPagamento folha = _context.Folhas.
                FirstOrDefault(f => f.Funcionario.Cpf.Equals(cpf));
                if(folha.Mes == mes){
                    if(folha.Ano == ano){
                        return Ok(folha);
                    }
                }
            return NotFound();
        }

        // GET: /api/folha/filtrar/{mes}/{ano}
        [HttpGet]
        [Route("filtrar/{mes}/{ano}")]
        public IActionResult Filtrar([FromRoute] string mes, int ano)
        {
            FolhaPagamento folha = _context.Folhas.
                FirstOrDefault(f => f.Ano.Equals(ano));
                if(folha.Mes == mes) {
                    return Ok(folha);
                }
            return NotFound();
        }
    }
}