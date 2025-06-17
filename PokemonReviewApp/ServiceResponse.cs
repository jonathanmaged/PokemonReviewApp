namespace PokemonReviewApp
{
    public class ServiceResponse
    {
        public int StatusCode {get; set;}
        public string StatusMessage {get; set;}
        public Object? Entity {get; set;}
        public ServiceResponse(int statusCode,string statusMessage,Object? entity = default)
        {
            StatusCode = statusCode;
            StatusMessage = statusMessage;
            Entity = entity;
        }

    }
}
