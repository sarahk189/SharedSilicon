
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Infrastructure.Dtos;

public class ContactDto
{
    public string FullName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? Service { get; set; }
    public string Message { get; set; } = null!;

}
