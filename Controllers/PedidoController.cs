using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using tech_test_payment_api.ContextoBanco;
using tech_test_payment_api.Models;

namespace tech_test_payment_api.Controllers
{
    [ApiController]
    [Route("api-docs/[controller]")]
    public class PedidoController : ControllerBase
    {
        private readonly Context _context;

        public PedidoController(Context context)
        {
            _context = context;
        }

        [HttpPost("RegistrarPedido")]
        public IActionResult RegistrarVenda(Pedido pedido)
        {
            if(pedido.Itens == null || pedido.Itens.Count == 0)
            return BadRequest(new{Erro = "Não é possível registrar um pedido sem item."});

            else
            {
                
                pedido.Data = DateTime.Now.Date;
                pedido.Status = EnumStatusPedido.AguardandoPagamento;
                _context.Add(pedido);
                _context.SaveChanges();

                return Ok(pedido);

            }
        }

        [HttpGet("{id}")]
        public IActionResult BuscarVendaId(int id)
        {
            var pedido = _context.Pedidos.Find(id);


            if(pedido == null)
                return NotFound();

            return Ok(pedido);    
        }

      

        [HttpPut("AtualizarStatus")]
        public IActionResult AtualizarStatusVenda(int id, EnumStatusPedido status)
        {
            var pedido = _context.Pedidos.Find(id);
            

            if(pedido == null)
                return NotFound();

            
            if(pedido.Status == EnumStatusPedido.AguardandoPagamento)
            {
                if(status != EnumStatusPedido.PagamentoAprovado || status != EnumStatusPedido.Cancelada)    
                    return BadRequest(new {Erro = "Pedido aguardando a aprovação de pagamento ou ser cancelado."});
                
            }

            if (pedido.Status == EnumStatusPedido.PagamentoAprovado)
            {
                if(status != EnumStatusPedido.EnviadoTransportadora || status != EnumStatusPedido.Cancelada)
                    return BadRequest(new{Error = "Pedido aguardando envio para transportadora ou ser cancelado."});
            
            }
            if (pedido.Status != EnumStatusPedido.Entregue)
                    return BadRequest(new{Error = "Pedido aguardando confirmação de entrega."});
            
            else 
            {
                pedido.Status = status;
                _context.Update(pedido.Status);
                _context.SaveChanges();
                
                return Ok(pedido);
            }
            
            
        }

        
    }
}