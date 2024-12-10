internal partial class MainApp
{
  // 식의 속성이나 필드가 패턴과 일치하는지를 검사함
  // 입력된 식이 int, double 같은 기본 데이터 형식이 아닌 경우에 특히 유용함.
  class Car
  {
    public string Model { get; set; }
    public DateTime ProducedAt { get; set; }
  }
    
  static string GetNickname(Car car)
  {
    var GenerateMessage = (Car car, string nickname) => 
      $"{car.Model} produced in {car.ProducedAt.Year} is {nickname}";

    // if(car is Car {Model:"Mustang", ProducedAt.Year: 1967})
    //     return GenerateMessage(car, "Fastback");
    // else if (car is Car {Model:"Mustang", ProducedAt.Year: 1976})
    //     return GenerateMessage(car, "Cobra II");
    // else 
    //     return GenerateMessage(car, "Unknown");

    return car switch
      {
        Car {Model:"Mustang", ProducedAt.Year: 1967} => GenerateMessage(car, "Fastback"),
        Car {Model:"Mustang", ProducedAt.Year: 1976} => GenerateMessage(car, "Cobra II"),
        _ => GenerateMessage(car, "Unknown")
      };
    }

    static void Main(string[] args)
    {
      Console.WriteLine(
        GetNickname(
          new Car() { Model = "Mustang", ProducedAt = new DateTime(1967, 11, 23) }));
      Console.WriteLine(
        GetNickname(
          new Car() { Model = "Mustang", ProducedAt = new DateTime(1976, 6, 7) }));
      Console.WriteLine(
        GetNickname(
          new Car() { Model = "Mustang", ProducedAt = new DateTime(2099, 12, 25) }));
    }
}
