using Newtonsoft.Json;
using PeeringDB_Data_Import.Models;
using System.Net;

Console.WriteLine("MVT Data Service started");

string requestUri = "https://www.peeringdb.com/api/fac/1";
string responseJson = ""; 

HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(requestUri);
httpWebRequest.Method = WebRequestMethods.Http.Get;
httpWebRequest.Accept = "application/json";



var response = (HttpWebResponse)httpWebRequest.GetResponse();

using (var sr = new StreamReader(response.GetResponseStream()))
{
    responseJson = sr.ReadToEnd();
}

List<pdb_datacenters> pdbDatacenters_List = new List<pdb_datacenters>();
pdbDatacenters_List = JsonConvert.DeserializeObject<List<pdb_datacenters>>(responseJson);



Console.WriteLine(responseJson);





