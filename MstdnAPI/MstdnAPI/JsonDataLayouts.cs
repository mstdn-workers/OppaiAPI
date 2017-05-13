namespace MstdnAPI {
    class ClientAppJson {
        public string id { get; set; }
        public string redirect_uri { get; set; }
        public string client_id { get; set; }
        public string client_secret { get; set; }
    }

    class AccessTokenJson {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public string scope { get; set; }
        public int created_at { get; set; }
    }
}
