using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace MstdnAPI {
    class HttpRequest {
        private HttpClient client;

        public HttpRequest() {
            client = new HttpClient(new ModernHttpClient.NativeMessageHandler());
        }

        // アプリＩＤの取得
        public async Task<ClientAppJson> getClientAppJson(string host) {
            var data = new ClientAppJson();
            var postData = new List<KeyValuePair<string, string>>();
            postData.Add(new KeyValuePair<string, string>("client_name", "OppaiAPI"));
            postData.Add(new KeyValuePair<string, string>("redirect_uris", "urn:ietf:wg:oauth:2.0:oob"));
            postData.Add(new KeyValuePair<string, string>("scopes", DefaultValue.MSTDN_SCOPE));
            var content = new FormUrlEncodedContent(postData);

            var url = "https://" + host + DefaultValue.MSTDN_AUTHPATH;

            try {
                var response = await client.PostAsync(url, content);
                if (response.IsSuccessStatusCode) {
                    var json = await response.Content.ReadAsStringAsync();
                    data = JsonConvert.DeserializeObject<ClientAppJson>(json);
                }
            } catch (Exception ex) {
                throw ex;
            }
            return data;
        }

        // アクセストークンの取得
        public async Task<AccessTokenJson> getTokenJson(string host, string client_id, string client_sec, string user_id, string password) {
            var data = new AccessTokenJson();
            var postData = new List<KeyValuePair<string, string>>();
            postData.Add(new KeyValuePair<string, string>("client_id", client_id));
            postData.Add(new KeyValuePair<string, string>("client_secret", client_sec));
            postData.Add(new KeyValuePair<string, string>("grant_type", "password"));
            postData.Add(new KeyValuePair<string, string>("username", user_id));
            postData.Add(new KeyValuePair<string, string>("password", password));
            postData.Add(new KeyValuePair<string, string>("scope", DefaultValue.MSTDN_SCOPE));  // こっちはscope 上はscopes なんのトラップだ
            var content = new FormUrlEncodedContent(postData);
            var url = "https://" + host + DefaultValue.MSTDN_TOKENPATH;
            try {
                var response = await client.PostAsync(url, content);
                if (response.IsSuccessStatusCode) {
                    var json = await response.Content.ReadAsStringAsync();
                    data = JsonConvert.DeserializeObject<AccessTokenJson>(json);
                }
            } catch (Exception ex) {
                throw ex;
            }
            return data;
        }

        // トゥートする
        public async Task<HttpResponseMessage> tootMsg(string host, string token, string msg) {
            // msgのURLエンコーディング（いらないっぽい）
            //var encodingMsg = System.Net.WebUtility.UrlEncode(msg);
            var encodingMsg = msg;

            // リクエストデータの作成
            var url = "https://" + host + DefaultValue.MSTDN_TOOTPATH;
            var request = new HttpRequestMessage(HttpMethod.Post, url);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var postData = new List<KeyValuePair<string, string>>();
            postData.Add(new KeyValuePair<string, string>("status", encodingMsg));
            request.Content = new FormUrlEncodedContent(postData);

            try {
                var response = await client.SendAsync(request);
                return response;
            } catch (Exception ex) {
                throw ex;
            }
        }
    }
}
