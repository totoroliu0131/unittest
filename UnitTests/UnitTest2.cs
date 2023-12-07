using System.Text;

namespace UnitTests;

[TestFixture]
public class OrderServiceTests
{
    [Test]
    public void Test_SyncBookOrders_3_Orders_Only_2_book_order()
    {
        // hard to isolate dependency to unit test

        var target = new OrderService();
        //target.SyncBookOrders();
        Assert.Fail();
    }
}

public class OrderService
{
    private string _filePath = @"C:\temp\testOrders.csv";

    public void SyncBookOrders()
    {
        var orders = GetOrders();

        // only get orders of book
        var ordersOfBook = orders.Where(x => x.Type == "Book");

        var bookDao = new BookDao();
        foreach (var order in ordersOfBook)
        {
            bookDao.Insert(order);
        }
    }

    private List<Order> GetOrders()
    {
        // parse csv file to get orders
        var result = new List<Order>();

        // directly depend on File I/O
        using (StreamReader sr = new StreamReader(this._filePath, Encoding.UTF8))
        {
            int rowCount = 0;

            while (sr.Peek() > -1)
            {
                rowCount++;

                var content = sr.ReadLine();

                // Skip CSV header line
                if (rowCount > 1)
                {
                    string[] line = content.Trim().Split(',');

                    result.Add(this.Mapping(line));
                }
            }
        }

        return result;
    }

    private Order Mapping(string[] line)
    {
        var result = new Order
        {
            ProductName = line[0],
            Type = line[1],
            Price = Convert.ToInt32(line[2]),
            CustomerName = line[3]
        };

        return result;
    }
}

public class Order
{
    public string Type { get; set; }

    public int Price { get; set; }

    public string ProductName { get; set; }

    public string CustomerName { get; set; }
}

public class BookDao
{
    internal void Insert(Order order)
    {
        throw new NotImplementedException();
    }
}

