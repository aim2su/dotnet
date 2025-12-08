using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Notify.Contracts;
using Notify.DataAccess;
using Notify.Models;
using System.Linq.Expressions;

namespace Notify.Controllers;
[ApiController]
[Route("[controller]")]
public class NotesController : ControllerBase
{

    private readonly NotesDbContext _dbContext;

    public NotesController(NotesDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateNoteRequest request, CancellationToken ct)
    {
        var note = new Note(request.Title, request.Description);
        await _dbContext.AddAsync(note, ct);
        await _dbContext.SaveChangesAsync(ct);
        return Ok();
    }
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] GetNotesRequest request, CancellationToken ct)
    {
        var notesQuery = _dbContext.Notes.AsQueryable();

       
        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            var searchLower = request.Search.ToLower();
            notesQuery = notesQuery.Where(n => n.Title.ToLower().Contains(searchLower));
        }

        var sortItem = (request.SortItem ?? "id").ToLower(); 
        var sortOrder = (request.SortOrder ?? "desc").ToLower();

        switch (sortItem)
        {
            case "date":
                notesQuery = sortOrder == "desc"
                    ? notesQuery.OrderByDescending(n => n.CreatedAt)
                    : notesQuery.OrderBy(n => n.CreatedAt);
                break;

            case "title":
                notesQuery = sortOrder == "desc"
                    ? notesQuery.OrderByDescending(n => n.Title)
                    : notesQuery.OrderBy(n => n.Title);
                break;

            default:
                notesQuery = sortOrder == "desc"
                    ? notesQuery.OrderByDescending(n => n.Id)
                    : notesQuery.OrderBy(n => n.Id);
                break;
        }

        var noteDtos = await notesQuery
            .Select(n => new NoteDto(n.Id, n.Title, n.Description, n.CreatedAt))
            .ToListAsync(ct);

        return Ok(new GetNotesResponse(noteDtos));
    }

    //[HttpGet]
    //public async Task<IActionResult> Get([FromQuery]GetNotesRequest request, CancellationToken ct)
    //{
    //    var notesQuery = _dbContext.Notes
    //        .Where(n => !string.IsNullOrWhiteSpace(request.Search) &&
    //        n.Title.ToLower().Contains(request.Search.ToLower()));

    //    Expression<Func<Note, object>> selectorKey = request.SortItem?.ToLower() switch
    //    {
    //        "date" => note => note.CreatedAt,
    //        "title" => Note => Note.Title,
    //        _ => note => note.Id
    //    };

    //    notesQuery = request.SortOrder == "Desc" 
    //        ? notesQuery.OrderByDescending(selectorKey) 
    //        : (IQueryable<Note>)notesQuery.OrderBy(selectorKey);

    //    var noteDtos = await notesQuery
    //        .Select(n => new NoteDto(n.Id, n.Title, n.Description, n.CreatedAt))
    //        .ToListAsync(cancellationToken: ct);

    //    return Ok(new GetNotesResponse(noteDtos));
    //}



    //private Expression<Func<Note, object>> GetSelectorKey(string sortItem)
    //{
    //    return sortItem switch
    //    {
    //        "createdAt" => note => note.CreatedAt,
    //        "title" => Note => Note.Title,
    //        _ => note => note.Id
    //    };
    //}

}
