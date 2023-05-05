using AudioLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioLibrary.InputModels;

public class SongInputModel
{
    public string Name { get; set; }
    public Author Author { get; set; }
    public Jenre Jenre{ get; set; }
    public string AlbumName { get; set; }
}
