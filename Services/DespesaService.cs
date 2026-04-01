using ApiFinanceira.DataContexts;
using ApiFinanceira.Dtos;
using ApiFinanceira.Exceptions;
using ApiFinanceira.Model;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiFinanceira.Services
{

    public class DespesaService
    {
        private readonly AppDbContex _context;

        private readonly IMapper _mapper;

        public DespesaService(AppDbContex context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ICollection<Despesa>> FindAll()
        {
            try
            {
                return await _context.Despesas.ToListAsync();

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Despesa> Create(DespesaDto novaDespesa)
        {
            try
            {
                var despesa = _mapper.Map<Despesa>(novaDespesa);

                await _context.Despesas.AddAsync(despesa);
                await _context.SaveChangesAsync();
                return despesa;
            }
            catch (Exception)
            {
                throw;
            }

        }

        public async Task<Despesa> FindById(int id)
        {
            try
            {
                var despesa = await _context.Despesas.FirstOrDefaultAsync(x => x.Id == id);

                if (despesa is null)
                {
                    throw new ErrorServiceException($"Despesa #{id} não encontrada.", 
                    c => c.NotFound (new{ mensagem = $"Despesa #{id} não encontrada."}));
                }
                return despesa;
            }
            catch (Exception)
            {
                throw;
            }

        }

        public async Task<Despesa> Update(int id, DespesasUpdateDto despesaDto)
        {
            try
            {
                var despesa = await FindById(id);

                _mapper.Map<DespesasUpdateDto, Despesa>(despesaDto, despesa);

                _context.Despesas.Update(despesa);
                await _context.SaveChangesAsync();
                return despesa;
            }
            catch (Exception)
            {
                throw;
            }

        }
               

        public async Task<ActionResult> Remove(int id)
        {
            try
            {
                var despesa = await FindById(id);

                _context.Despesas.Remove(despesa);
                await _context.SaveChangesAsync();
                return new NoContentResult();

            }
            catch (Exception)
            {
                throw;
            }
        }





    }//

}//
