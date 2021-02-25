using System;
using System.Collections.Generic;
using System.Text;

namespace JwtDemo.Shared.Dtos
{
    public class ErrorDto
    {
        public ErrorDto()
        {
            Errors = new List<string>();
        }

        public ErrorDto(string error, bool isShow) : this()
        {
            Errors.Add(error);
            IsShow = isShow;
        }
        public ErrorDto(List<string> errors, bool isShow) : this()
        {
            Errors.AddRange(errors);
            IsShow = isShow;
        }
        public List<string> Errors { get; private set; }
        public bool IsShow { get; private set; }
    }
}
