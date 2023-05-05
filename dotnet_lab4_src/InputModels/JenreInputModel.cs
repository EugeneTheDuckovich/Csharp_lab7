using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioLibrary.InputModels;

public class JenreInputModel
{
    public string Name { get; set; }
    public JenreInputModel() 
    {
        Name= string.Empty;
    }
}
