using Microsoft.AspNetCore.Mvc;
using Sst.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sst.Shared.BaseControllers
{
    public class CustomBaseController : ControllerBase
    {
        public IActionResult CreateActionResultInstance<T>(ResponseDto<T> responseDto)
        {
            return new ObjectResult(responseDto)
            {
                
                StatusCode = responseDto.StatusCode

            };

        }
    }

}