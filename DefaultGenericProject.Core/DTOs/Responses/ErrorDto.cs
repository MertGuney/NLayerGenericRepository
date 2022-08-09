﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DefaultGenericProject.Core.Dtos.Responses
{
    public class ErrorDto
    {
        public List<string> Errors { get; private set; } = new List<string>();

        public bool IsShow { get; private set; }

        public ErrorDto(string error, bool isShow)
        {
            Errors.Add(error);
            IsShow = isShow;
        }

        public ErrorDto(List<string> errors, bool isShow)
        {
            Errors = errors;
            IsShow = isShow;
        }
    }
}