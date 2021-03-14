namespace Shared.Model.Response
{
    public class Error
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
    }
}

// 200	Ok
// 201	Created
// 304	Not Modified
// 400	Bad Request
// 401	Not Authorized
// 403	Forbidden
// 404	Page/ Resource Not Found
// 405	Method Not Allowed
// 415	Unsupported Media Type
// 500	Internal Server Error
// 1000 or plus -> Customs Errors
