 
// Type: app.bsms.Models.Manage.Service.Accounts
 

using System;
using System.Collections.Generic;

namespace app.bsms.Models.Manage.Service
{
  public class Accounts
  {
    public string siteCode { get; set; }

    public string customerCode { get; set; }

    public DateTime transactionDate { get; set; }

    public string invoiceNumber { get; set; }

    public string transactionNumber { get; set; }

    public string treatmentParentCode { get; set; }

    public string treatmentCode { get; set; }

    public string treatCode { get; set; }

    public string course { get; set; }

    public string description { get; set; }

    public string type { get; set; }

    public Decimal payment { get; set; }

    public Decimal amount { get; set; }

    public Decimal balance { get; set; }

    public Decimal outstanding { get; set; }

        //Yoonus Add next 1 line
    //public Decimal topupAmount { get; set; }

    //Yoonus Add next 1 line
    public string itemCode { get; set; }
        //Yoonus Add next 1 line
        public string itemDesc { get; set; }

        public List<Accounts> details { get; set; }

    public List<Accounts> info { get; set; }
  }
}
