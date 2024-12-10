namespace ConsoleApp1 
{
  
    internal partial class MainApp
    {
        static void Main(string[] args)
        {
            // 상수 패턴 Constant Pattern
            // 식이 특정 상수와 일치하는지를 검사하며, 가장 많이 사용하는 패턴이기도 하다.
            // 정수 / 문자열 리터럴 뿐 아니라 null, enum등 모든 상수와 매칭할 수 있다.
            var GetCountryCode = (string nation) => nation switch
            {
                "KR" => 82,
                "US" => 1,
                "UK" => 44,
                _ => throw new AggregateException("Not Supported Code.")
            };
            
             Console.WriteLine(GetCountryCode("KR")); // 82
             Console.WriteLine(GetCountryCode("US")); // 1
             Console.WriteLine(GetCountryCode("UK")); // 44
             Console.WriteLine(GetCountryCode("GB")); // Unhandled exception. System.AggregateException: Not Supported Code.

             // ######## 단순한 null 체크 ########
             // if(obj is null) // obj == null 
             // if(obj is not null) // obj !=null
        }
    }
}
