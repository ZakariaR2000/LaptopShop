namespace LapShop.Models
{
    public class ApiResponse
    {


        //data coming from API
        public object Data { get; set; }
        //List Of API Errors
        public object Errors { get; set; }
        //API status Code 200=sucess 400=failed
        public string StatusCode { get; set; }



    }
}
