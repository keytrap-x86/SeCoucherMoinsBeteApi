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

        public static async Task<List<Anecdote>> GetAnecdotes(string category)
        {
            try
            {
                var anecdotes = new List<Anecdote>();
                using (var http = new HttpClient())
                {
                    var response = await http.GetByteArrayAsync("http://secouchermoinsbete.fr/" + category);
                    var resp = Encoding.UTF8.GetString(response, 0, response.Length - 1);


                    if (string.IsNullOrEmpty(resp))
                        return null;

                    var document = new HtmlDocument();
                    document.LoadHtml(resp);
                    var articleNodes = document.DocumentNode.SelectNodes("//div[@class='anecdote-content-wrapper']/*/a");
                    if (articleNodes == null)
                        return null;

                    anecdotes.AddRange(from aNode in articleNodes.Nodes()
                            .Where(n => n.InnerText != "En savoir plus")
                        let id = aNode.ParentNode.ParentNode.ParentNode.ParentNode.Attributes["data-id"]
                            .Value
                        let body = aNode.InnerText.Trim()
                        select new Anecdote(id,
                            body));

                    return anecdotes;

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }


            return null;
        }
    }
}
