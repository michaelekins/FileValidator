﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileValidator
{
    interface IValidator
    {
        Boolean ValidateField(string fieldText, out string errorText);
    }
}
