using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using ModelLibrary;
using REST_WCF_TEMPLATE;
using Newtonsoft.Json;

namespace REST_CLIENT_TEMPLATE
{
    internal class RestClient
    {
        private const String uri = "http://localhost:5683/Service1.svc";

        public RestClient()
        {
        }

        public void Start()
        {
            var FilmList = GetMoviesAsync().Result;
            Console.WriteLine("Alle movies\n" +
                String.Join("\n", FilmList));


            var oneFilm = GetOneMovieAsync(1).Result;
            Console.WriteLine("Film nr=" + 1 + "\n" +
                              oneFilm);


            var deleteFilm = DeleteMovieAsync(1).Result;
            Console.WriteLine("Film nr=" + 1 + " er slettet \n" +
                              deleteFilm);


            //EKSEMPEL PÅ METODE:
            //AddCatchAsync(new Catch(64, "Lars", "Laks", 1.2, "Norge", 51));
            //var catchesList2 = GetCatchesAsync().Result;
            //Console.WriteLine("Alle fangster\n" +
            //                  String.Join("\n", catchesList2));



            AddMovieAsync(new Movies(6, "Hobitten", 5));
            var filmlist2 = GetMoviesAsync().Result;
            Console.WriteLine("Alle film\n" +
                              String.Join("\n", filmlist2));
        }


        private async Task<IList<Movies>> GetMoviesAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                string content = await client.GetStringAsync(uri + "/movies");
                IList<Movies> cList = JsonConvert.DeserializeObject<IList<Movies>>(content);
                return cList;
            }
        }

        private async Task<Movies> GetOneMovieAsync(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                string content = await client.GetStringAsync(uri + "/movies/" + id);
                Movies c = JsonConvert.DeserializeObject<Movies>(content);
                return c;
            }
        }

        private async Task<Movies> DeleteMovieAsync(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                var content = await client.DeleteAsync(uri + "/movies?id=" + id);
                Movies c = JsonConvert.DeserializeObject<Movies>(content.Content.ReadAsStringAsync().Result);
                return c;
            }
        }

        private async void AddMovieAsync(Movies newMovie)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpContent content = new StringContent(JsonConvert.SerializeObject(newMovie));
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                var result = await client.PostAsync(uri + "/movies", content);

            }
        }

    }
}
