using HtmlAgilityPack;

using SeCoucherMoinsBeteApi.Core.Objects;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SeCoucherMoinsBeteApi
{
    public static class ScmbApi
    {
        /// <summary>
        /// Permet de récupérer des anecdotes aléatoires.
        /// Avec la version "gratuite", il n'est pas possible de filtrer par catégorie.
        /// </summary>
        /// <param name="max">Nombre maximum d'anecdotes à renvoyer (0 pour "infini")</param>
        /// <returns></returns>
        public static async Task<List<Anecdote>> AnecdotesAleatoires(int max = 0)
        {
            var anecdotes = new List<Anecdote>();

            try
            {
                
                using (var http = new HttpClient())
                {
                    var responseByte = await http.GetByteArrayAsync("http://secouchermoinsbete.fr/random");

                    var html = Encoding.UTF8.GetString(responseByte, 0, responseByte.Length - 1);

                    // Si le code html contient cette phrase, alors le site nous empêche de lire les anecdotes
                    // (sûrement car une catégorie a été spécifié)
                    if (html.Contains("Pour accéder au contenu premium"))
                        return max == 0 ? anecdotes : anecdotes.GetRange(0, max > anecdotes.Count ? anecdotes.Count - 1 : max);

                    if (string.IsNullOrEmpty(html))
                        return max == 0 ? anecdotes : anecdotes.GetRange(0, max > anecdotes.Count ? anecdotes.Count - 1 : max);

                    // On créé un nouveau document Html
                    var document = new HtmlDocument();

                    // On y injecte le code html
                    document.LoadHtml(html);

                    // On parcourt les nœuds
                    var articleNodes = document.DocumentNode.SelectNodes("//div[@class='anecdote-content-wrapper']/*/a");
                    if (articleNodes == null)
                        return max == 0 ? anecdotes : anecdotes.GetRange(0, max > anecdotes.Count ? anecdotes.Count - 1 : max);

                    // On ajoute toutes les anecdotes à la liste
                    anecdotes.AddRange(articleNodes.Nodes()
                        .Where(n => n.InnerText != "En savoir plus")
                        .Select(aNode => new
                        {
                            aNode,
                            id = aNode.ParentNode.ParentNode.ParentNode.ParentNode.Attributes["data-id"].Value
                        })
                        .Select(t => new {t, body = t.aNode.InnerText.Trim()})
                        .Select(t => new Anecdote(t.t.id, t.body)));

                    return max == 0 ? anecdotes : anecdotes.GetRange(0,max > anecdotes.Count ? anecdotes.Count - 1 : max);

                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"{DateTime.Now} [ERREUR] : {e.Message}\n{e.InnerException?.Message}");
                return max == 0 ? anecdotes : anecdotes.GetRange(0, max > anecdotes.Count ? anecdotes.Count - 1 : max);
            }
        }
    }
}
