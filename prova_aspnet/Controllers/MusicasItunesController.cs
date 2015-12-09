using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Controllers
{
    public class MusicasItunesController : Controller
    {
        WSController ws = new WSController();
        List<ListaModel> lista = new List<ListaModel>();

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Filtro(string artista, string musica = null)
        {
            var musicas = ws.Itunes(artista);
            var cotacao = ws.Cotacao("dolar");

            foreach (var item in musicas)
            {
                var totalReais = item.collectionPrice * cotacao;

                lista.Add(new ListaModel() { artworkUrl60 = item.artworkUrl60, trackName = item.trackName, collectionName = item.collectionName, trackTimeMillis = item.trackTimeMillis, releaseDate = item.releaseDate, artistName = item.artistName, collectionPrice = "$ " + Math.Round(item.collectionPrice, 2), valorReal = "R$ " + Math.Round(totalReais, 2) });
            }

            return View(lista);
        }

        public ActionResult PlayerMusica(string artista, string musica)
        {
            var musicas = ws.Itunes(artista);
            var arqMusica = "";

            foreach (var item in musicas)
            {
                if (item.trackName == musica)
                    arqMusica = item.previewUrl;
            }

            return PartialView(model: arqMusica);
        }
    }
}
