using Deli.Controllers;
using Deli.Services;
using Microsoft.AspNetCore.Mvc;

namespace TicketSystem.Controllers;

public class FileController: BaseController{
    private readonly IFileService _fileService;

    public FileController(IFileService fileService) {
        _fileService = fileService;
    }

    
    [HttpPost("multi")]
    public async Task<IActionResult> Upload([FromForm] IFormFile[] files) => Ok(await _fileService.Upload(files));



}