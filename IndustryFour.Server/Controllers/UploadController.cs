﻿using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace IndustryFour.Server.Controllers
{
	[Route("api/upload")]
	[ApiController]
	public class UploadController : ControllerBase
	{
		[HttpPost]
		public IActionResult Upload()
		{
			var file = Request.Form.Files[0];
            if (file.Length == 0)
            {
                return BadRequest();
            }

            var targetFolder = Path.Combine("StaticFiles", "Documents");
			var targetDirectoryPath = Path.Combine(Directory.GetCurrentDirectory(), targetFolder);

			var targetFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
			var targetFilePath = Path.Combine(targetDirectoryPath, targetFileName);
			var urlPath = Path.Combine(targetFolder, targetFileName).Replace("\\", "/");

			using (var stream = new FileStream(targetFilePath, FileMode.Create))
			{
				file.CopyTo(stream);
			}

			return Ok(urlPath);
		}
	}
}
