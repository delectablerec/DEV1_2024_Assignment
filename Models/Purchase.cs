namespace DEV1_2024_Assignment.Models;
public class Purchase
{
    public int Id {get;set;}
    public int CustomerId {get;set;}
    public AppUser Customer {get;set;}
    public int ProductId {get;set;}   
    public Product Product {get;set;} 
    public int Quantity {get;set;}  
    public string Date {get;set;}
}