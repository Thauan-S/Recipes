//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Http.HttpResults;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using WebApplication1.Context;
//using WebApplication1.Dto;
//using WebApplication1.Models;
//using WebApplication1.Services.Client;
//using WebApplication1.Services.Reserve;

//namespace WebApplication1.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class ReserveController : ControllerBase
//    {
//        private readonly IReserveInterface _reserveInterface;

//        public ReserveController(IReserveInterface reserveInterface)
//        {
//            _reserveInterface = reserveInterface;
//        }

//        // GET: api/Reserve
//        [HttpGet]
//        public async Task<ActionResult<ReserveModel>> FindAllReserves()
//        {
//            var reserves= await _reserveInterface.FindAllReserves();
//            return Ok(reserves);
//        }

//        // GET: api/Reserve/5
//        [HttpGet("{id}")]
//        public async Task<ActionResult<ReserveModel>> FindById(int id)
//        {
//            var reserveModel = await _reserveInterface.FindById(id);

//            if (reserveModel == null)
//            {
//                return NotFound();
//            }
//            return Ok(reserveModel);
            
//        }

//        // PUT: api/Reserve/5
//        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//        [HttpPut]
//        public async Task<ActionResult<ResponseModel<ReserveDto>>> Update( ReserveDto reserveDto)
//        {
            
//           var reserve =await _reserveInterface.Update(reserveDto);
//            return Ok(reserve);
//        }

//        // POST: api/Reserve
//        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//        [HttpPost]
//        public async Task<ActionResult<ReserveDto>> Create(ReserveDto reserveDto)
//        {
//           var reservedto= await _reserveInterface.Create(reserveDto);
//            return CreatedAtAction("CreateReserve",new {id=reserveDto.Id},reserveDto);
//        }

//        // DELETE: api/Reserve/5
//        [HttpDelete("{id}")]
//        public async Task<IActionResult> DeleteReserveModel(int id)
//        {
//            var reserveModel = await _reserveInterface.DeleteById(id);
//            if(reserveModel == null) { 
//                return NotFound(); 
//            }
//            return NoContent();
//        }
//    }
//}
