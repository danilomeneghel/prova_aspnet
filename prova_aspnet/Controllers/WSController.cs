using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Web.Script.Serialization;
using System.Text;
using Models;
using System.Globalization;

namespace Controllers
{
    public class WSController
    {
        double preco;

        public List<Result> Itunes(string filtro)
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create("https://itunes.apple.com/search?media=music&term=" + filtro);

            MusicaModel musics = new MusicaModel();

            using (var response = (HttpWebResponse)request.GetResponse())
            {
                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    JavaScriptSerializer js = new JavaScriptSerializer();
                    var objText = reader.ReadToEnd();

                    musics = (MusicaModel)js.Deserialize(objText, typeof(MusicaModel));
                }
            }
            
            return musics.results;
        }

        public double Cotacao(string tipo)
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create("http://api.promasters.net.br/cotacao/v1/valores");
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/45.0.2454.85 Safari/537.36";

            CotacaoModel quote = new CotacaoModel();

            using (var response = (HttpWebResponse)request.GetResponse())
            {
                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    JavaScriptSerializer js = new JavaScriptSerializer();
                    var objText = reader.ReadToEnd();

                    quote = (CotacaoModel)js.Deserialize(objText, typeof(CotacaoModel));
                }
            }

            if (tipo == "dolar")
                preco = quote.valores.USD.valor;
            else if (tipo == "euro")
                preco = quote.valores.EUR.valor;
            
            return preco;
        }
    }
}