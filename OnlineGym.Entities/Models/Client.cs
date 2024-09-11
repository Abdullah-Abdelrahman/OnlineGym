using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace OnlineGym.Entities.Models;

public  class Client:IdentityUser
{

  

    public string Name { get; set; }


    public string? ProfilePhoto { get; set; }

    [JsonIgnore]
    public virtual ICollection<ClientSubscription>? ClientSubscriptions { get; set; } = new List<ClientSubscription>();
    [JsonIgnore]
    public virtual ICollection<Subscription>? Subscription { get; set; }= new List<Subscription>();

}
