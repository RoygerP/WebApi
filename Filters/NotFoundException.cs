using System;

namespace WebApi.Filters;

public class NotFoundException : Exception
{
    public NotFoundException(string Message)
        : base(Message) { }
}
