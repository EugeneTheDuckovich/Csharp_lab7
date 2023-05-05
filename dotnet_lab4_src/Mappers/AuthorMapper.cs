using AudioLibrary.Entities;
using AudioLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioLibrary.Mappers;

public static class AuthorMapper
{
    public static Author ToModel(this AuthorEntity entity)
    {
        return new Author(entity.Id, entity.Name,entity.Year);
    }

    public static AuthorEntity ToEntity(this Author model)
    {
        return new AuthorEntity
        {
            Id = model.Id,
            Name = model.Name,
            Year = model.Year,
        };
    }
}
