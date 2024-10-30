using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace PrismCoreTemplate.Models;

public class CustomerSettings
{
    public string Name { get; set; }
    public double Version { get; set; }
}