using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace BlobTrigger;

public class StudentImages
{
    private readonly ILogger<StudentImages> _logger;

    public StudentImages(ILogger<StudentImages> logger)
    {
        _logger = logger;
    }

    [Function(nameof(StudentImages))]
    public async Task Run([BlobTrigger("studentimages/{name}", Connection = "")] Stream stream, string name)
    {
        using var blobStreamReader = new StreamReader(stream);
        var content = await blobStreamReader.ReadToEndAsync();
        _logger.LogInformation("New file arrived: {name}", name);
    }
}