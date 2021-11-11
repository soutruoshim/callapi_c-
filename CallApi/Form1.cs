using System;
using System.Windows.Forms;
using System.IO;
using System.Net;
//using System.Text.Json;
using System.Data.SqlClient;
using System.Linq;
using System.Collections.Generic;
using RestSharp;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace CallApi
{
    public partial class Form1 : Form
    {
        public SqlConnection connection;
        private IList<Card> cards = new List<Card>();
        private IList<Card> cardsApi = new List<Card>();
        private IList<Card> cardNoneDublic = new List<Card>();
        CardRepo cardRepo = new CardRepo();
        OutRecordRepo outRecord = new OutRecordRepo();

        public Form1()
        {
            InitializeComponent();
            connection = new SqlConnection(@"Data Source = SRH\SQLEXPRESS; Initial Catalog = ParkWatch; Integrated Security = True");
            try
            {
                connection.Open();
                // MessageBox.Show("Connection success fully");
                connection.Close();
            }catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
            readDb();

            foreach(Card card in cards)
            {
                //Console.WriteLine("read card : " + card.CardNo);
            }
        }
        
        private async void btnCallApi_Click(object sender, EventArgs e) 
        {

            var url = "http://apartment.local/parking/cards";
            //var url = "https://eazy.daikou.asia/parking/cards.php";
            var httpRequest = (HttpWebRequest)WebRequest.Create(url);

            httpRequest.Accept = "*/*";


            var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
               
                var result = streamReader.ReadToEnd();
                /*var cardList = JsonSerializer.Deserialize<IList<Card>>(result);

                foreach (var card in cardList)
                {
                    //Console.WriteLine("Department Id is: {0}", card.CardNo);
    
                    Card cardObject = new Card(card.CardNo, card.CarNo, card.CardType, card.CardIndate, card.CardAmount, card.CarType, card.CarStyle, card.CarColor, card.MasterName, card.MasterID, card.MasterTel, card.MasterAddr, card.ParkNo, card.ParkPosition, card.PayAmount, card.MakeDateTime, card.OperatorName, card.Enable, card.Remark);

                    cardsApi.Add(cardObject);
                }*/
            }

            Console.WriteLine(httpResponse.StatusCode);
            
            foreach (Card card in cards)
            {
                //Console.WriteLine("Card data : " + card.CardNo);
                //addCardData(card);
            }
            foreach (Card card in cardsApi)
            {
                Console.WriteLine("Card api data : " + card.CardNo);
                //addCardData(card);
            }

            var results = cardsApi.Except(cards, new CardComparer());
         


            foreach (var card in results)
            {
                Console.WriteLine("None dup: " + card.CardNo);
                //addCardData(card);
                cardRepo.Add(card);
            }


        }
        private void addCardData(Card card)
        {
           
                Console.WriteLine(card.CardIndate + " " + card.MakeDateTime);
               
                //string sql = "INSERT INTO  CardIssue (CardNo, CarNo, CardType, CardIndate, CardAmount, CarType, CarStyle, CarColor, MasterName, MasterID, MasterTel, MasterAddr, ParkNo, ParkPosition, PayAmount, MakeDateTime, OperatorName, Enable,Remark) " + " VALUES ('" + card.CardNo + "', '" + card.CarNo + "', '" + card.CardType + "', '" + Convert.ToDateTime(card.CardIndate) + "', '" + Convert.ToDouble(card.CardAmount) + "', '" + card.CarType + "', '" + card.CarStyle + "', '" + card.CarColor + "', '" + card.MasterName + "', '" + card.MasterID + "', '" + card.MasterTel + "', '" + card.MasterAddr + "', '" + card.ParkNo + "', '" + card.ParkPosition + "', '" + Convert.ToDouble(card.PayAmount) + "', '" + Convert.ToDateTime(card.MakeDateTime) + "', '" + card.OperatorName + "', '" + Convert.ToInt32(card.Enable) + "', '" + card.Remark + "');";
                String sql = "SET ANSI_WARNINGS OFF INSERT into CardIssue (CardNo, CarNo, CardType, CardIndate, CardAmount, CarType, CarStyle, CarColor, MasterName, MasterID, MasterTel, MasterAddr, ParkNo, ParkPosition, PayAmount, MakeDateTime, OperatorName, Enable,Remark) VALUES (@CardNo, @CarNo, @CardType, @CardIndate, @CardAmount, @CarType, @CarStyle, @CarColor, @MasterName, @MasterID, @MasterTel, @MasterAddr, @ParkNo, @ParkPosition, @PayAmount, @MakeDateTime, @OperatorName, @Enable, @Remark) SET ANSI_WARNINGS ON";
               
         
                SqlCommand cmd = new SqlCommand(sql, connection);
         
                cmd.Parameters.AddWithValue("@CardNo", card.CardNo);
                cmd.Parameters.AddWithValue("@CarNo", card.CarNo);
                cmd.Parameters.AddWithValue("@CardType", card.CardType);
                cmd.Parameters.AddWithValue("@CardIndate", card.CardIndate);
                cmd.Parameters.AddWithValue("@CardAmount", card.CardAmount);
                cmd.Parameters.AddWithValue("@CarType", card.CarType);
                cmd.Parameters.AddWithValue("@CarStyle", card.CarStyle);
                cmd.Parameters.AddWithValue("@CarColor", card.CarColor);
                cmd.Parameters.AddWithValue("@MasterName", card.MasterName);
                cmd.Parameters.AddWithValue("@MasterID", card.MasterID);
                cmd.Parameters.AddWithValue("@MasterTel", card.MasterTel);
                cmd.Parameters.AddWithValue("@MasterAddr", card.MasterAddr);
                cmd.Parameters.AddWithValue("@ParkNo", card.ParkNo);
                cmd.Parameters.AddWithValue("@ParkPosition", card.ParkPosition);
                cmd.Parameters.AddWithValue("@PayAmount", card.PayAmount);
                cmd.Parameters.AddWithValue("@MakeDateTime", card.MakeDateTime);
                cmd.Parameters.AddWithValue("@OperatorName", card.OperatorName);
                cmd.Parameters.AddWithValue("@Enable", card.Enable);
                cmd.Parameters.AddWithValue("@Remark", card.Remark);

                try
                {
                    if(connection.State == System.Data.ConnectionState.Closed) connection.Open();
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    connection.Close();
                }catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }
             
           
        }
       
        private void readDb()
        {
            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("SELECT [CardNo],[CarNo],[CardType],[CardIndate],[CardAmount],[CarType],[CarStyle],[CarColor],[MasterName],[MasterID],[MasterTel],[MasterAddr],[ParkNo],[ParkPosition] ,[PayAmount],[MakeDateTime],[OperatorName],[Enable],[Remark] FROM CardIssue", connection);
                SqlDataReader dataReader = cmd.ExecuteReader();
                if (dataReader.HasRows)
                {
                    cards.Clear();
                   
                    while (dataReader.Read())
                    {
                        
                        String CardNo = dataReader.GetString(0);
                        String CarNo = dataReader.GetString(1);
                        String CardType = dataReader.GetString(2);
                        String CardIndate = dataReader.GetDateTime(3).ToString();
                        String CardAmount = dataReader.GetDecimal(4).ToString();
                        String CarType = dataReader.GetString(5);
                        String CarStyle = dataReader.GetString(6);
                        String CarColor = dataReader.GetString(7);
                        String MasterName = dataReader.GetString(8);
                        String MasterID = dataReader.GetString(9);
                        String MasterTel = dataReader.GetString(10);
                        String MasterAddr = dataReader.GetString(11);
                        String ParkNo = dataReader.GetString(12);
                        String ParkPosition = dataReader.GetString(13);
                        String PayAmount = dataReader.GetDecimal(14).ToString();
                        String MakeDateTime =  dataReader.GetDateTime(15).ToString();
                        String OperatorName = dataReader.GetString(16);
                        String Enable = dataReader.GetBoolean(17).ToString();
                        String Remark = dataReader.GetString(18);

                        Card card = new Card(CardNo, CarNo, CardType, CardIndate, CardAmount, CarType, CarStyle, CarColor, MasterName, MasterID, MasterTel, MasterAddr, ParkNo, ParkPosition, PayAmount, MakeDateTime, OperatorName, Enable, Remark);
                        //Console.WriteLine("data : " + card.CardNo);
                        cards.Add(card);
                    }
                }
                dataReader.Close();
                cmd.Dispose();
                connection.Close();
            
            }catch(Exception e)
            {
                MessageBox.Show("Error: " + e.Message);
            }
        }

        private void btnPostApi_Click(object sender, EventArgs e)
        {
            var url = "https://jsonplaceholder.typicode.com/posts";

            var httpRequest = (HttpWebRequest)WebRequest.Create(url);
            httpRequest.Method = "POST";

            httpRequest.Accept = "application/json";
            httpRequest.ContentType = "application/json";

            var data = @"{
                          ""Id"": 78912,
                          ""Customer"": ""Jason Sweet"",
                          ""Quantity"": 1,
                          ""Price"": 18.00
                          }";
            var data2 = @"{
                        ""userId"": 1,
                        ""id"": 10,
                        ""title"": ""sunt aut facere repellat provident occaecati excepturi optio reprehenderit"",
                        ""body"": ""quia et suscipit\nsuscipit recusandae consequuntur expedita et cum\nreprehenderit molestiae ut ut quas totam\nnostrum rerum est autem sunt rem eveniet architecto""
                        }";
            using (var streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
            {
                streamWriter.Write(data2);
            }

            var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                Console.WriteLine("result: " + result);
            }

            Console.WriteLine(httpResponse.StatusCode);
        }

        private void btnRepo_Click(object sender, EventArgs e)
        {
            foreach(var card in cardRepo.GetAll())
            {
                Console.WriteLine("Wh : " + card.CardNo);
            } 
        }

        private void btnOutRepo_Click(object sender, EventArgs e)
        {
            Console.WriteLine("----------------out repo----------------");
            foreach(var outrepo in outRecord.GetAll())
            {
                Console.WriteLine("Record: " + outrepo.RecordNo);
            }
        }

        private void btnUsingRest_Click(object sender, EventArgs e)
        {
            var client = new RestClient("https://eazy.daikou.asia/parking");
            var request = new RestRequest("cards.php");
            var respone = client.Execute(request);
            if(respone.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string rawRespone = respone.Content;
                //var cardList = JsonConvert.DeserializeObject<Card>(rawRespone);
                List<Card> cardList = JsonConvert.DeserializeObject<List<Card>>(rawRespone);
                foreach(var card in cardList)
                {
                    Console.WriteLine("api: " + card.CardNo);
                }
            }
        }

        private async void btnPost_Click(object sender, EventArgs e)
        {
         

            var client = new RestClient("http://dummy.restapiexample.com/api/v1/");
            //var request = new RestRequest("", Method.POST);
            var request = new RestRequest("create", Method.POST);
            
            request.AddParameter("name", "cvbnm");
            request.AddParameter("salary", "4123");
            request.AddParameter("age", "23");
         
            request.AddHeader("Content-Type", "application/json; chaset-utf-8");
            
            
            var respone =await client.ExecutePostTaskAsync(request);
           
           
            if (respone.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string rawRespone = respone.Content;
                Message message = JsonConvert.DeserializeObject<Message>(rawRespone);
                // Console.WriteLine("status" + message.status);
                // Console.WriteLine("id: " + message.data.id);
                
            }
            //Console.WriteLine(respone.Content);
        }

        private async void btnPostOutRecord_Click(object sender, EventArgs e)
        {
            Console.WriteLine("----------------out repo----------------");
            foreach (var outrepo in outRecord.GetUnsignLimit())
            {
                Console.WriteLine("Record: " + outrepo.RecordNo);
                postOutRecord(outrepo);
            }

        }

        private async void postOutRecord(OutReport outRecord)
        {
            var client = new RestClient("http://apartment.local/api/parking_register_outrecord.php");
            var request = new RestRequest("", Method.POST);
            //var request = new RestRequest("create", Method.POST);

            var inDateTime = Convert.ToDateTime(outRecord.InDateTime);
            var card_in_date = Convert.ToDateTime(outRecord.CardIndate);
            var outDateTime = Convert.ToDateTime(outRecord.OutDateTime);
            var payDateTime = Convert.ToDateTime(outRecord.PayDateTime);
            
            //Console.WriteLine("indate: " + inDate);
            //Console.WriteLine(inDate.ToString("yyyy-MM-dd HH:mm:ss"));

            var body = Newtonsoft.Json.JsonConvert.SerializeObject(new { 
                property_id = "5", 
                record_no = outRecord.RecordNo,
                compute_no = outRecord.ComputeNo,
                park_no = outRecord.ParkNo,
                card_no = outRecord.CardNo,
                car_no = outRecord.CarNo,
                card_type = outRecord.CardType,
                card_in_date = card_in_date.ToString("yyyy-MM-dd HH:mm:ss"),
                card_amount = outRecord.CardAmount,
                car_type = outRecord.CarType,
                car_style = outRecord.CarStyle,
                car_color = outRecord.CarColor,
                master_name = outRecord.MasterName,
                master_id = outRecord.MasterID,
                master_tel = outRecord.MasterTel,
                master_addr = outRecord.MasterAddr,
                park_position = outRecord.ParkPosition,
                in_track_name = outRecord.InTrackName,
                in_date_time = inDateTime.ToString("yyyy-MM-dd HH:mm:ss"),
                in_picture_name = outRecord.InPictureName,
                in_operator_name = outRecord.InOperatorName,
                in_style = outRecord.InStyle,
                out_track_name = outRecord.OutTrackName,
                out_date_time = outDateTime.ToString("yyyy-MM-dd HH:mm:ss"),
                out_picture_name = outRecord.OutPictureName,
                out_operator_name = outRecord.OutOperatorName,
                out_style = outRecord.OutStyle,
                car_free = outRecord.CarFee,
                pay_amount = outRecord.PayAmount,
                card_pay_amount = outRecord.CardPayAmount,
                pay_date_time = payDateTime.ToString("yyyy-MM-dd HH:mm:ss"),
                park_time = outRecord.ParkTime,
                pic_in_add = outRecord.PicInAdd,
                pic_out_add = outRecord.PicOutAdd,
                remark = outRecord.Remark
            });

          
            request.AddHeader("Accept", "application/json");
            request.Parameters.Clear();
            request.AddParameter("application/json", body, ParameterType.RequestBody);

            /*
            request.AddParameter("property_id", 12);
            request.AddParameter("recordNo", outRecord.RecordNo);
            request.AddParameter("computeNo", outRecord.ComputeNo);
            request.AddParameter("parkNo", outRecord.ParkNo);
            request.AddParameter("cardNo", outRecord.CardNo);
            request.AddParameter("carNo", outRecord.CarNo);
            request.AddParameter("cardType", outRecord.CardType);
            request.AddParameter("cardIndate", outRecord.CardIndate);
            request.AddParameter("cardAmount", outRecord.CardAmount);
            request.AddParameter("carType", outRecord.CarType);
            request.AddParameter("carStyle", outRecord.CarStyle);
            request.AddParameter("carColor", outRecord.CarColor);
            request.AddParameter("masterName", outRecord.MasterName);
            request.AddParameter("masterID", outRecord.MasterID);
            request.AddParameter("masterTel", outRecord.MasterTel);
            request.AddParameter("masterAddr", outRecord.MasterAddr);
            request.AddParameter("parkPosition", outRecord.ParkPosition);
            request.AddParameter("inTrackName", outRecord.InTrackName);
            request.AddParameter("inDateTime", outRecord.InDateTime);
            request.AddParameter("inPictureName", outRecord.InPictureName);
            request.AddParameter("inOperatorName", outRecord.InOperatorName);
            request.AddParameter("inStyle", outRecord.InStyle);
            request.AddParameter("outTrackName", outRecord.OutTrackName);
            request.AddParameter("outDateTime", outRecord.OutDateTime);
            request.AddParameter("outPictureName", outRecord.OutPictureName);
            request.AddParameter("outOperatorName", outRecord.OutOperatorName);
            request.AddParameter("outStyle", outRecord.OutStyle);
            request.AddParameter("carFee", outRecord.CarFee);
            request.AddParameter("payAmount", outRecord.PayAmount);
            request.AddParameter("cardPayAmount", outRecord.CardPayAmount);
            request.AddParameter("payDateTime", outRecord.PayDateTime);
            request.AddParameter("parkTime", outRecord.ParkTime);
            request.AddParameter("picInAdd", outRecord.PicInAdd);
            request.AddParameter("picOutAdd", outRecord.PicOutAdd);
            request.AddParameter("remark", outRecord.Remark); */

            //request.AddHeader("Content-Type", "application/json; chaset-utf-8");

            try
            {
                var respone = await client.ExecutePostTaskAsync(request);

                if (respone.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string rawRespone = respone.Content;
                    Message message = JsonConvert.DeserializeObject<Message>(rawRespone);
                    Console.WriteLine("status: " + message.success + "Message: " + message.msg );
                   
                    if(message.success == true)
                    {
                        OutRecordRepo outRecordRepo = new OutRecordRepo();
                        outRecordRepo.updateSign(outRecord.RecordNo);
                    }
                }
                Console.WriteLine(respone);
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
          
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
