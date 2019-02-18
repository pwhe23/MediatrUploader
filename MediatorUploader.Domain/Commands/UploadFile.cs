using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace MediatorUploader.Domain
{
    public class UploadFile : IRequest<string>
    {
        public StreamInfo File { get; set; }
        public List<StreamInfo> Files { get; set; }

        internal class Handler : IRequestHandler<UploadFile, string>
        {
            private readonly IAppContext _appContext;

            public Handler(IAppContext appContext)
            {
                _appContext = appContext;
            }

            public Task<string> Handle(UploadFile request, CancellationToken cancellationToken)
            {
                var files = request.Files.ToList();
                if (request.File != null)
                {
                    files.Insert(0, request.File);
                }

                var fileNames = new HashSet<string>();

                foreach (var file in files)
                {
                    var filePath = _appContext.MapDataPath(Path.GetFileName(file.Filename));
                    var bytes = file.Stream.ToArray();

                    System.IO.File.WriteAllBytes(filePath, bytes);

                    fileNames.Add($" > {filePath} [{bytes.Length}]");
                }

                return Task.FromResult($"{fileNames.Count} File(s) uploaded"
                                       + Environment.NewLine
                                       + string.Join(Environment.NewLine, fileNames));
            }
        };
    };
}
