using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Commands
{
    public class CheckoutOrderCommand: IRequest<int>
    {
        public int Id { get; set; }

        //create constructor for all fields in Order domain entity
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal TotalPrice { get; set; }
        public string EmailAddress { get; set; }
        public string AddressLine { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string CardName { get; set; }
        public string CardNumber { get; set; }
        public string Expiration { get; set; }
        public string CVV { get; set; }
        public int PaymentMethod { get; set; }

        public CheckoutOrderCommand(int id, string userName, string firstName, string lastName, decimal totalPrice, string emailAddress, string addressLine, string country, string state, string zipCode, string cardName, string cardNumber, string expiration, string cvv, int paymentMethod)
        {
            Id = id;
            UserName = userName;
            FirstName = firstName;
            LastName = lastName;
            TotalPrice = totalPrice;
            EmailAddress = emailAddress;
            AddressLine = addressLine;
            Country = country;
            State = state;
            ZipCode = zipCode;
            CardName = cardName;
            CardNumber = cardNumber;
            Expiration = expiration;
            CVV = cvv;
            PaymentMethod = paymentMethod;
        }
    }
}
