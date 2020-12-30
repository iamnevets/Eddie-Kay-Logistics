using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusBookingApp.PayStackApi.Models
{
    public class TransactionVerificationResponseData
    {
        public int Id { get; set; }
        public string Domain { get; set; }
        public string Status { get; set; }
        public string Reference { get; set; }
        public int Amount { get; set; }
        public object Message { get; set; }
        public string Gateway_response { get; set; }
        public object Paid_at { get; set; }
        public DateTime Created_at { get; set; }
        public string Channel { get; set; }
        public string Currency { get; set; }
        public string Ip_address { get; set; }
        public string Metadata { get; set; }
        public Log Log { get; set; }
        public object Fees { get; set; }
        public object Fees_split { get; set; }
        public Authorization Authorization { get; set; }
        public Customer Customer { get; set; }
        public object Plan { get; set; }
        public Split Split { get; set; }
        public object Order_id { get; set; }
        public object PaidAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public int Requested_amount { get; set; }
        public object Pos_transaction_data { get; set; }
        public DateTime Transaction_date { get; set; }
        public Plan_Object Plan_object { get; set; }
        public Subaccount Subaccount { get; set; }
    }

    public class Log
    {
        public int Time_spent { get; set; }
        public int Attempts { get; set; }
        public object Authentication { get; set; }
        public int Errors { get; set; }
        public bool Success { get; set; }
        public bool Mobile { get; set; }
        public object[] Input { get; set; }
        public object Channel { get; set; }
        public History[] History { get; set; }
    }

    public class History
    {
        public string Type { get; set; }
        public string Message { get; set; }
        public int Time { get; set; }
    }

    public class Authorization
    {
        public string Authorization_code { get; set; }
        public string Card_type { get; set; }
        public string Last4 { get; set; }
        public string Exp_month { get; set; }
        public string Exp_year { get; set; }
        public string Bin { get; set; }
        public string Bank { get; set; }
        public string Channel { get; set; }
        public string Signature { get; set; }
        public bool Reusable { get; set; }
        public string Country_code { get; set; }
        public string Account_name { get; set; }
    }

    public class Customer
    {
        public int Id { get; set; }
        public object First_name { get; set; }
        public object Last_name { get; set; }
        public string Email { get; set; }
        public string Customer_code { get; set; }
        public object Phone { get; set; }
        public object Metadata { get; set; }
        public string Risk_action { get; set; }
        public object International_format_phone { get; set; }
    }

    public class Split
    {
    }

    public class Plan_Object
    {
    }

    public class Subaccount
    {
    }


}
