using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net;
using System.Text;

namespace Nala.Core
{
    public class NewsService
    {
        public ItemNews[] GetNewsContent(string NewsParameters)
        {

            List<ItemNews> Details = new List<ItemNews>();

            // httpWebRequest with API URL
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create
            ("http://news.google.com/news?q=" + NewsParameters + "&output=rss");

            //Method GET
            request.Method = "GET";

            //HttpWebResponse for result
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();


            //Mapping of status code
            if (response.StatusCode == HttpStatusCode.OK)
            {
                Stream receiveStream = response.GetResponseStream();
                StreamReader readStream = null;

                if (response.CharacterSet == "")
                    readStream = new StreamReader(receiveStream);
                else
                    readStream = new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));

                //Get news data in json string

                string data = readStream.ReadToEnd();

                //Declare DataSet for putting data in it.
                DataSet ds = new DataSet();
                StringReader reader = new StringReader(data);
                ds.ReadXml(reader);
                DataTable dtGetNews = new DataTable();

                if (ds.Tables.Count > 3)
                {
                    dtGetNews = ds.Tables["item"];

                    foreach (DataRow dtRow in dtGetNews.Rows)
                    {
                        ItemNews DataObj = new ItemNews();
                        DataObj.title = dtRow["title"].ToString();
                        DataObj.link = dtRow["link"].ToString();
                        DataObj.item_id = dtRow["item_id"].ToString();
                        DataObj.PubDate = dtRow["pubDate"].ToString();
                        DataObj.Description = dtRow["description"].ToString();
                        Details.Add(DataObj);
                    }
                }
            }

            //Return News array 
            return Details.ToArray();
        }

        //Define Class to return news data
        public class ItemNews
        {
            public string title { get; set; }
            public string link { get; set; }
            public string item_id { get; set; }
            public string PubDate { get; set; }
            public string Description { get; set; }
        }
    }
}
