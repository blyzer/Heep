using HeeP.Models.Exceptions;
using System;
using System.Collections.Generic;

namespace HeeP.Core.Rules
{
    public interface IRules<T> : IDisposable
        where T : class
    {
        ValidationException Validate(T model);


        T Get(int id);

        ICollection<T> GetAll();

        T Add(T model);

        T Modify(T model);
    }
}
