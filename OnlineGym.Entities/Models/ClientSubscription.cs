using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace OnlineGym.Entities.Models;

public  class ClientSubscription
{
    public int ClientSubscriptionId { get; set; }
    public string ClientId { get; set; }



    public int SubscriptionId { get; set; }

    public int price { get; set; }

    public DateTime? orderDate { get; set; }

    //strip
    public string? SessionId { get; set; }

    public string? PaymentIntentId { get; set; }
    //



    public string Status { get; set; } = null!;


	public string? PaymentStatus { get; set; } 


	[ForeignKey("ClientId")]
    public virtual Client? Client { get; set; }

    [ForeignKey("SubscriptionId")]
   
    public  Subscription? Subscription { get; set; }

    [JsonIgnore]
    public ClientSubscriptionDetails? ClientSubscriptionDetails { get; set; }
}
