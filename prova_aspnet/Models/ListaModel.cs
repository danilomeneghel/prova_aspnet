using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class ListaModel
    {
        public string artworkUrl60 { get; set; }

        public string trackName { get; set; }

        public string collectionName { get; set; }

        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = @"{0:mm\:ss}")]
        public TimeSpan trackTimeMillis { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime releaseDate { get; set; }

        public string artistName { get; set; }

        public string collectionPrice { get; set; }

        public string valorReal { get; set; }
    }
}
