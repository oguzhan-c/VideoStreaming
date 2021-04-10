using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstruct;
using Core.Entities.Concrute;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperationClaimsController : ControllerBase
    {
        private IOperationClaimService _operationClaimService;

        public OperationClaimsController(IOperationClaimService operationClaimService)
        {
            _operationClaimService = operationClaimService;
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _operationClaimService.GetAll();

            if (result.Succcess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        public IActionResult GetById(int id)
        {
            var result = _operationClaimService.GetById(id);

            if (result.Succcess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpDelete("delete")]
        public IActionResult Delete(int id)
        {
            var deleteToRepository = _operationClaimService.GetById(id);

            var result = _operationClaimService.Delete(deleteToRepository.Data.Name);

            if (result.Succcess)
            {
                return Ok(result);
            }

            return BadRequest(result);

        }

        [HttpPut("add")]
        public IActionResult Add(OperationClaim claim)
        {
            var result = _operationClaimService.Add(claim);

            if (result.Succcess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }


        [HttpPut("Update")]
        public IActionResult Update(OperationClaim claim)
        {
            var result = _operationClaimService.Update(claim);

            if (result.Succcess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}
