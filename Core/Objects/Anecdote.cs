namespace SeCoucherMoinsBeteApi.Core.Objects
{
    public class Anecdote
    {
        public string Id { get; set; }    

        public string Body { get; set; }

        public Anecdote(string id, string body)
        {
            Id = id;
            Body = System.Net.WebUtility.HtmlDecode(body);
        }

        public override string ToString()
        {
            return $"[{Id}] [{Body}]";
        }
    }
}
