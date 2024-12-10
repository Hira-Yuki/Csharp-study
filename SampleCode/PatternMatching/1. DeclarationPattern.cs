namespace ConsoleApp1 // Note: actual namespace depends on the project name.
{
    internal partial class MainApp
    {
        static void Main(string[] args)
        {
          object foo = 23;

          // 선언 패턴
          if (foo is int bar) // 1.foo가 int인 경우 2. foo를 int 형식으로 변환하여 bar에 할당.
          {
              Console.WriteLine(bar); // 23
          }
            
          // 이 코드에서 foo is int가 true를 반환하면
          // bar 변수가 if블록 안에 생성되고 23이 출력되지만,
          // false를 반환하는 경우에는 생성되지 않음.
          // 당연히 bar가 출력될 일도 없음.
        }
    }
}
