using System;
using System.Collections.Generic;
using System.Text;

namespace DefaultGenericProject.Core.DTOs.Responses
{
    public class ErrorDTO
    {
        public List<string> Errors { get; private set; } = new List<string>();

        public bool IsShow { get; private set; }

        public ErrorDTO(string error, bool isShow)
        {
            Errors.Add(error);
            IsShow = isShow;
        }

        public ErrorDTO(List<string> errors, bool isShow)
        {
            Errors = errors;
            IsShow = isShow;
        }
    }
}