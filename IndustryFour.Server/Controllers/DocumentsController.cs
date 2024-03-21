using AutoMapper;
using IndustryFour.Shared.Dtos.Document;
using IndustryFour.Server.Models;
using IndustryFour.Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace IndustryFour.Server.Controllers;

[Route("api/documents")]
[ApiController]
public class DocumentsController : Controller
{
    private readonly ILoggerManager _logger;
    private readonly IDocumentService _documentService;
    private readonly IMapper _mapper;

    public DocumentsController(IMapper mapper, IDocumentService documentService, ILoggerManager logger)
    {
        _mapper = mapper;
        _documentService = documentService;
        _logger = logger;

        _logger.LogInfo("Instantiated DocumentsController");
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var documents = await _documentService.GetAll();

        return Ok(_mapper.Map<IEnumerable<DocumentResultDto>>(documents));
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var document = await _documentService.GetById(id);
        if (document == null)
        {
            return NotFound();
        }

        return Ok(_mapper.Map<DocumentResultDto>(document));
    }

    [HttpGet]
    [Route("get-documents-by-category/{categoryId:int}")]
    public async Task<IActionResult> GetDocumentsByCategory(int categoryId)
    {
        var documents = await _documentService.GetDocumentsByCategory(categoryId);
        if (!documents.Any())
        {
            return NotFound();
        }

        return Ok(_mapper.Map<IEnumerable<DocumentResultDto>>(documents));
    }

    [HttpPost]
    public async Task<IActionResult> Add(DocumentAddDto documentDto)
    {
        var document = _mapper.Map<Document>(documentDto);
        var documentResult = await _documentService.Add(document);
        if (documentResult == null)
        {
            _logger.LogError("Error creating document");
            return BadRequest();
        }

        return CreatedAtAction(nameof(GetById), new { id = documentResult.Id }, documentResult);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, DocumentEditDto documentDto)
    {
        if (id != documentDto.Id)
        {
            return BadRequest();
        }

        await _documentService.Update(_mapper.Map<Document>(documentDto));

        return Ok(documentDto);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Remove(int id)
    {
        var document = await _documentService.GetById(id);
        if (document == null)
        {
            return NotFound(); 
        }

        await _documentService.Remove(document);

        return Ok();
    }

    [HttpGet]
    [Route("search/{documentName}")]
    public async Task<ActionResult<List<Document>>> Search(string documentName)
    {
        var documents = _mapper.Map<List<Document>>(await _documentService.Search(documentName));
        if (documents == null || documents.Count == 0)
        {
            return NotFound("No document was found");
        }

        return Ok(documents);
    }

    [HttpGet]
    [Route("search-document-with-category/{searchedValue}")]
    public async Task<ActionResult<List<Document>>> SearchDocumentWithCategory(string searchedValue)
    {
        var documents = _mapper.Map<List<Document>>(await _documentService.SearchDocumentsWithCategory(searchedValue));
        if (!documents.Any())
        {
            return NotFound("No document was found");
        }

        return Ok(_mapper.Map<IEnumerable<DocumentResultDto>>(documents));
    }
}
