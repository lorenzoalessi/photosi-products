﻿using System.Diagnostics.CodeAnalysis;

namespace PhotosiProducts.Dto;

[ExcludeFromCodeCoverage]
public class ProductDto
{
    public int Id { get; set; }

    public int CategoryId { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }
}