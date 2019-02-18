using System.Threading.Tasks;
using System.Web.Mvc;
using MediatorUploader.Domain;
using MediatR;

namespace MediatorUploader.Web
{
    public class HomeController : Controller
    {
        private readonly IMediator _mediator;

        public HomeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<ActionResult> Index(QueryFiles queryFiles)
        {
            var files = await _mediator.Send(queryFiles);
            return View(files);
        }

        public async Task<ActionResult> Upload(UploadFile request)
        {
            var response = await _mediator.Send(request);
            return Content(response, "text/plain");
        }
    };
}