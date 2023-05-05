using AudioLibrary.Entities;
using AudioLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace AudioLibrary.Mappers;

public static class JenreMapper
{
    public static Jenre ToModel(this JenreEntity entity)
    {
        return new Jenre(entity.Id,entity.Name);
    }

    public static JenreEntity ToEntity(this Jenre model) {
        return new JenreEntity
        {
            Id = model.Id,
            Name = model.Name,
        };
    }
}
