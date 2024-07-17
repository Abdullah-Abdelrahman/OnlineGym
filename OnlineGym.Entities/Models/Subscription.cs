using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace OnlineGym.Entities.Models;

public  class Subscription
{
    public int SubscriptionId { get; set; }

    public string SubscriptionName { get; set; } = null!;

    public int Price { get; set; }

    public int DurationDays { get; set; }

    public ICollection<Benefit>? Benefits { get; set; }
    public List<SubscriptionBenefit>? SubscriptionBenefits { get; set; }
    [JsonIgnore]
    public virtual ICollection<ClientSubscription>? ClientSubscriptions { get; set; } = new List<ClientSubscription>();
    [JsonIgnore]
    public virtual ICollection<Client>? Clients { get; set; } = new List<Client>();
}
