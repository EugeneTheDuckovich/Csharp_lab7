using System.Collections.Generic;

namespace AudioLibrary.Entities
{
    public class JenreEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<SongEntity> Songs { get; set; }
    }
}
