﻿namespace PetHome.Application.FileProvider
{
    public record FileData(
        Stream Stream,
        string BucketName,
        string ObjectName);
}
