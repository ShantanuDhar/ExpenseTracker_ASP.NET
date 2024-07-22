
//using System.ComponentModel.DataAnnotations;

//namespace ExpenseTracker.Models
//{
//    public class Expense
//    {

//        public Expense()
//        {
//        }


//        public int Id { get; set; }
//        public decimal Value { get; set; }

//        [Required]
//        public string? Description { get; set; }
//    }
//}
using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker.Models
{
    public class Expense
    {
        public int Id { get; set; }
        public decimal Value { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public int Curr_ID { get; set; }
    }

    public class Currency
    {
        public int Curr_ID { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
    }

    public class ExpenseView
    {
        public int Id { get; set; }
        public decimal Value { get; set; }
        public string Description { get; set; }
        public string CurrencyName { get; set; }
        public string CurrencyCode { get; set; }
    }
}
