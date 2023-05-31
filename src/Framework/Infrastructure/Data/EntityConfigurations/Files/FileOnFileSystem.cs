﻿using Core.Entities.Files;

namespace Infrastructure.Data.EntityConfigurations.Files;

public class FileOnFileSystemConfiguration : IEntityTypeConfiguration<FileOnFileSystem>
{
    public void Configure(EntityTypeBuilder<FileOnFileSystem> builder)
    {
        builder.Property(p => p.FilePath).HasMaxLength(500);
    }
}