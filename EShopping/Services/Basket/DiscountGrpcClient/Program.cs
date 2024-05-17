using System.Threading.Tasks;
using Grpc.Net.Client;
using Discount.Grpc.Protos;


using var channel = GrpcChannel.ForAddress("http://localhost:8084");
var discountProtoClient = new DiscountProtoService.DiscountProtoServiceClient(channel);
var discount = await discountProtoClient.GetDiscountAsync(new GetDiscountRequest { ProductName = "Adidas Quick Force Indoor Badminton Shoes" });
Console.WriteLine("Discount Amount:", discount.Amount);
Console.WriteLine("Discount Description:", discount.Description);


