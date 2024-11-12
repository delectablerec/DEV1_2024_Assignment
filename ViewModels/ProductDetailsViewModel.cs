public class DetailsViewModel
{
    public Product Product { get; set; }

    private Database _database;


    public DetailsViewModel(int id)
    {

        Products = _database.GetProductById(id);

    }
}
