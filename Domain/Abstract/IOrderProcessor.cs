using System.Collections.Generic;
using Domain.Concrete;
using Domain.Entities;



namespace Domain.Abstract
{
    public interface IOrderProcessor
    {
        void ProcessOrder(Cart cart, ShippingDatails shippingDatails, OrdersSummary os, string subject, string header, string[] contentManagersEmails);

        void EmailRecovery(User user);

        void EmailActivation(User user, string host);

        void FeedBackRequest(FeedBack feedBack);

        void FeedBackRequestForContentManagers(FeedBack feedBack, string[] emails);

        void MassMailingDelivery(string subject, string body, IEnumerable<string> emails);
    }
    

  
        
  
}