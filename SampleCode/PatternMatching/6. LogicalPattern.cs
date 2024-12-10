// 논리 패턴
// Logical Pattern
       
// 패턴과 패턴을 논리 연산자(and, or, not)을 조합하여
// 하나의 논리 패턴으로 만들 수 있다.


class OrderItem
{
  public int Amount { get; set; }
  public int Price  { get; set; }
}

static double GetPrice(OrderItem orderItem) => orderItem switch
{
  { Amount: 0 } or { Price: 0 } => 0.0,
  { Amount: >= 100 } and { Price: >= 10_000 } => orderItem.Amount * orderItem.Price * 0.8,
  not { Amount: < 100 } => orderItem.Amount * orderItem.Price * 0.9,
  _ => orderItem.Amount * orderItem.Price
};

static void Main(string[] args)
{
  Console.WriteLine(GetPrice(new OrderItem() {Amount = 0, Price = 10_000}));
  Console.WriteLine(GetPrice(new OrderItem() {Amount = 100, Price = 10_000}));
  Console.WriteLine(GetPrice(new OrderItem() {Amount = 100, Price = 9_000}));
  Console.WriteLine(GetPrice(new OrderItem() {Amount = 1, Price = 1_000}));
}
