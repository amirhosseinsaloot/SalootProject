﻿using Core.Entities.Files;

namespace Core.Entities.Logging;

public class EmailsLogFileModel : IEntity
{
    public int EmailsLogId { get; set; }

    public int FileModelId { get; set; }

    // Navigation properties
    public EmailsLog EmailsLog { get; set; } = null!;

    public FileModel FileModel { get; set; } = null!;
}