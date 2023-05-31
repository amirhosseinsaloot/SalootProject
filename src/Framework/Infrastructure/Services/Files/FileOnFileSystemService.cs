﻿using Core.Entities.Files;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Infrastructure.Services.Files;

public class FileOnFileSystemService : IFileService
{
    private readonly IDataProvider<FileOnFileSystem> _fileOnFileSystemDataProvider;

    public FileOnFileSystemService(IDataProvider<FileOnFileSystem> fileOnFileSystemDataProvider)
    {
        _fileOnFileSystemDataProvider = fileOnFileSystemDataProvider;
    }

    public async Task<int> StoreFileAsync(IFormFile formFile, string description, CancellationToken cancellationToken)
    {
        if (formFile is null)
        {
            throw new BadRequestException("File is empty object");
        }

        var basePath = Path.Combine("Resources");
        var basePathExists = Directory.Exists(basePath);
        if (basePathExists is false)
        {
            Directory.CreateDirectory(basePath);
        }
        var fileName = Path.GetFileNameWithoutExtension(formFile.FileName);
        var filePath = Path.Combine(basePath, formFile.FileName);
        var extension = Path.GetExtension(formFile.FileName);
        if (File.Exists(filePath))
        {
            fileName = $"{fileName}_{DateTime.UtcNow.Ticks}";
            filePath = Path.Combine(basePath, fileName);
        }

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await formFile.CopyToAsync(stream);
        }

        var fileModel = new FileOnFileSystem
        {
            Name = fileName,
            FileType = formFile.ContentType,
            Extension = extension,
            Description = description,
            FilePath = filePath
        };

        return await _fileOnFileSystemDataProvider.AddAsync(fileModel, cancellationToken);
    }

    public async Task<List<int>> StoreFilesAsync(List<IFormFile> formFiles, string description, CancellationToken cancellationToken)
    {
        if (formFiles is null)
        {
            throw new BadRequestException("Files is empty object");
        }

        var fileModels = new List<FileOnFileSystem>();

        foreach (var formFile in formFiles)
        {
            var basePath = Path.Combine("Resources");
            var basePathExists = Directory.Exists(basePath);
            if (basePathExists is false)
            {
                Directory.CreateDirectory(basePath);
            }

            var fileName = Path.GetFileNameWithoutExtension(formFile.FileName);
            var filePath = Path.Combine(basePath, formFile.FileName);
            var extension = Path.GetExtension(formFile.FileName);
            if (File.Exists(filePath))
            {
                fileName = $"{fileName}_{DateTime.UtcNow.Ticks}";
                filePath = Path.Combine(basePath, fileName);
            }

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await formFile.CopyToAsync(stream);
            }

            var fileModel = new FileOnFileSystem
            {
                Name = fileName,
                FileType = formFile.ContentType,
                Extension = extension,
                Description = description,
                FilePath = filePath
            };

            fileModels.Add(fileModel);
        }

        return await _fileOnFileSystemDataProvider.AddRangeAsync(fileModels, cancellationToken);
    }

    public async Task<FileStreamResult> GetFileByIdAsync(int id, CancellationToken cancellationToken)
    {
        var entity = await _fileOnFileSystemDataProvider.GetByIdAsync(id, cancellationToken);

        var memory = new MemoryStream();
        using (var stream = new FileStream(entity.FilePath!, FileMode.Open))
        {
            await stream.CopyToAsync(memory);
        }
        memory.Position = 0;

        // Create FileStreamResult
        var fileStreamResult = new FileStreamResult(memory, entity.FileType)
        {
            FileDownloadName = entity.Name + entity.Extension
        };

        return fileStreamResult;
    }

    public async Task DeleteFileAsync(int id, CancellationToken cancellationToken)
    {
        var entity = await _fileOnFileSystemDataProvider.GetByIdAsync(id, cancellationToken);

        if (File.Exists(entity.FilePath))
        {
            File.Delete(entity.FilePath);
        }
        await _fileOnFileSystemDataProvider.RemoveAsync(id, cancellationToken);
    }
}
