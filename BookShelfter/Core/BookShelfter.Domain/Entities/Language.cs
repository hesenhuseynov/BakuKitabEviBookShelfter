using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookShelfter.Domain.Entities;
using BookShelfter.Domain.Entities.Common;

namespace BookShelfter.Domain.Entities
{
    public class Language:BaseEntity
    {
        public string LanguageCode { get; set; }
        public string LanguageName  { get; set; }

        public ICollection<Book> Books { get; set; }


    }
}
