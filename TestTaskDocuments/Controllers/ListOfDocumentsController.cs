using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TestTaskDocuments.Data;
using TestTaskDocuments.Models.Task;

namespace TestTaskDocuments.Controllers
{
    public class ListOfDocumentsController : Controller
    {
        public ApplicationDbContext _context;
        private readonly IHostingEnvironment _appEnvironment;
        public ListOfDocumentsController(ApplicationDbContext context, IHostingEnvironment appEnvironment)
        {
            _context = context;
            _appEnvironment = appEnvironment;
        }

        [Authorize(Roles = "Admin")]
        [Authorize(Roles = "Editor")]
        [Authorize(Roles = "Subscriber")]
        [HttpGet]
        public IActionResult GetAllDocuments()
        {
            return View(_context.PublishedDocuments.ToList());
        }

        
        [HttpGet]
        public IActionResult CreateDocument()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateDocument(IFormFile file, 
            string LanguageFromDropDownList, string Header, string Description, string Comment)
        {
            string Path_Root = _appEnvironment.WebRootPath;
            string FilePath = Path_Root + "\\UserFiles\\" + file.FileName;

            string Publisher = HttpContext.User.Identity.Name;

            PublishedDocument publishDocument = new PublishedDocument(Header, Description, LanguageFromDropDownList,
            Publisher, Comment, FilePath);

            await _context.PublishedDocuments.AddAsync(publishDocument);
            await _context.SaveChangesAsync();
            using (var stream = new FileStream(FilePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return Redirect("GetAllDocuments");
        }

        public async Task<IActionResult> GetInfoAboutDocument(string documentId)
        {
            return View(await _context.PublishedDocuments.FindAsync(documentId));
        }

        public FileResult DownloadFile(string documentId)
        {
            string file_path = _context.PublishedDocuments.Find(documentId).PathDocument;
            string file_type = "application/jpg";

            return PhysicalFile(file_path, file_type);
        }

        public IActionResult DeletDocument(string documentId)
        {
            var document =  _context.PublishedDocuments.Find(documentId);
            _context.PublishedDocuments.Remove(document);
            _context.SaveChanges();

            return Redirect("GetAllDocuments");
        }
    }
}