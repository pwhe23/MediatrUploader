using System.IO;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace MediatorUploader.Domain
{
    public class QueryFiles : IRequest<FileInfo[]>
    {
        internal class Handler : IRequestHandler<QueryFiles, FileInfo[]>
        {
            private readonly IAppContext _appContext;

            public Handler(IAppContext appContext)
            {
                _appContext = appContext;
            }

            public Task<FileInfo[]> Handle(QueryFiles request, CancellationToken cancellationToken)
            {
                var path = _appContext.MapDataPath("");
                var folder = new DirectoryInfo(path);

                var files = folder.GetFiles();

                return Task.FromResult(files);
            }
        };
    };
}
