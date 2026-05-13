using ApiFinanceira.DataContexts;
using ApiFinanceira.Dtos;
using ApiFinanceira.Dtos.Responses;
using ApiFinanceira.Exceptions;
using ApiFinanceira.Model;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ApiFinanceira.Services
{

    public class DespesaService
    {
        private readonly AppDbContext _context;

        private readonly IMapper _mapper;

        public DespesaService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ICollection<DespesaResponseDto>> FindAll()
        {
            try
            {
                //var list = await _context.Despesas
                //    .Include(d => d.Categoria)
                //    .ToListAsync();

                //return _mapper.Map<ICollection<DespesaResponseDto>>(list);

                return await _context.Despesas
                    .ProjectTo<DespesaResponseDto>(_mapper.ConfigurationProvider)
                    .ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Despesa> Create(DespesaDto data)
        {
            try
            {
                var categoriaExiste = await _context.Categorias.AnyAsync(
                    x => x.Id == data.CategoriaId);
                if (!categoriaExiste)
                {
                    throw new ErrorServiceException($"Categoria não encontrada.",
                    c => c.NotFound(new { mensagem = $"Categoria #{data.CategoriaId} não encontrada." }));
                }
                var despesa = _mapper.Map<Despesa>(data);

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
                var despesa = await _context.Despesas
                    .Include(x => x.Tags)  
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (despesa is null)
                {
                    throw new ErrorServiceException($"Despesa #{id} não encontrada.", 
                    c => c.NotFound(new{ mensagem = $"Despesa #{id} não encontrada."}));
                }
                return despesa;
            }
            catch (Exception)
            {
                throw;
            }

        }

        public async Task<Despesa> Update(int id, DespesasUpdateDto data)
        {
            try
            {
                var despesa = await FindById(id);
                var categoriaExiste = await _context.Categorias.AnyAsync(
                    x => x.Id == data.CategoriaId);
                if (!categoriaExiste)
                {
                    throw new ErrorServiceException($"Categoria não encontrada.",
                    c => c.NotFound(new { mensagem = $"Categoria #{data.CategoriaId} não encontrada." }));
                }

                _mapper.Map<DespesasUpdateDto, Despesa>(data, despesa);

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

        public async Task<Despesa> AddTags(int id, DespesaTagDto tag)
        {

            try
            {
                var despesa = await FindById(id);
                var tags = await _context.Tags.Where(x => tag.Tags.Contains(x.Id)).ToListAsync();

                if (tags.Count == 0)
                {
                    throw new ErrorServiceException($"Tags não encontrada.",
                    c => c.NotFound(new { mensagem = $"Tags não encontrada." }));
                }

                foreach (Tag _tag in tags)
                {
                    if(despesa.Tags.Any(t => t.Id != _tag.Id))
                    {
                        despesa.Tags.Add(_tag);
                    }
                    
                }
                    
                await _context.SaveChangesAsync();

                return despesa;
            }
            catch (Exception)
            {

                throw;
            }

            

            throw new NotImplementedException();
        }
    }//

}//
