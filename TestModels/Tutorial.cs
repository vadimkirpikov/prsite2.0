using System;
using System.Collections.Generic;

namespace test1.TestModels;

public partial class Tutorial
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Title { get; set; }

    public string Url { get; set; }

    public string? Text { get; set; }

    public int? ChapterId { get; set; }
}
