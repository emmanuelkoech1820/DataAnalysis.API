//using System.Runtime.InteropServices;

//namespace DataAnalysis.Infrastructure
//{
//    public class ClientProxy
//    {
//        public async Task<IHttpActionResult> GetFinancialData()
//        {
//            using (HttpClient client = new HttpClient())
//            {
//                try
//                {
//                    HttpResponseMessage response = await client.GetAsync(DataUrl);
//                    response.EnsureSuccessStatusCode();
//                    string responseBody = await response.Content.ReadAsStringAsync();
//                    // Process and parse the financial data from the responseBody
//                    // ...

//                    // Return the financial data as the API response
//                    return Ok("Financial data retrieved successfully");
//                }
//                catch (HttpRequestException ex)
//                {
//                    // Handle exception if unable to retrieve data from the URL
//                    return InternalServerError(ex);
//                }
//            }
//        }
//    }
//}
//public class DataModel
//{

//}