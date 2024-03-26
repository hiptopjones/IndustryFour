using AutoMapper;
using IndustryFour.Server.Models;
using IndustryFour.Server.Services;
using IndustryFour.Shared.Dtos.Chunk;
using Microsoft.AspNetCore.Mvc;

namespace IndustryFour.Server.Controllers;

[Route("api/chunks")]
[ApiController]
public class ChunksController : Controller
{
    private readonly ILoggerManager _logger;
    private readonly IChunkService _chunkService;
    private readonly IMapper _mapper;

    public ChunksController(IMapper mapper, IChunkService chunkService, ILoggerManager logger)
    {
        _mapper = mapper;
        _chunkService = chunkService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var chunks = await _chunkService.GetAll();

        return Ok(_mapper.Map<IEnumerable<ChunkResultDto>>(chunks));
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var chunk = await _chunkService.GetById(id);
        if (chunk == null)
        {
            return NotFound();
        }

        return Ok(_mapper.Map<ChunkResultDto>(chunk));
    }

    [HttpGet]
    [Route("document/{documentId:int}")]
    public async Task<IActionResult> GetByDocumentId(int documentId)
    {
        var chunks = await _chunkService.GetByDocumentId(documentId);
        if (!chunks.Any())
        {
            return NotFound();
        }

        return Ok(_mapper.Map<IEnumerable<ChunkResultDto>>(chunks));
    }
}
