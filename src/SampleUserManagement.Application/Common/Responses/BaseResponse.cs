using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleUserManagement.Application.Common.Responses
{
	public record BaseResponse<T>(T Data);
}
