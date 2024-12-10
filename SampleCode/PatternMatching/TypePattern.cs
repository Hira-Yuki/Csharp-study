// 형식 패턴 예제 
// C# 9.0 이후 지원
// 변수 생성 없이 형식 일치 여부만 테스트함.

namespace ConsoleApp1 
{
    class Preschooler { }
    class Underage { }
    class Adult { }
    class Senior { }

    internal partial class MainApp
    {
        static int CalculateFee(object visitor)
        {
            return visitor switch
            {
                Underage => 100,
                Adult => 500,
                Senior => 200,
                _ => throw new ArgumentException($"Prohibited age: {visitor.GetType()}", nameof(visitor))
            };
        }
        
        static void Main(string[] args)
        {
            Console.WriteLine($"Fee for a senior : ${CalculateFee(new Senior())}");
            Console.WriteLine($"Fee for a Adult : ${CalculateFee(new Adult())}");
            Console.WriteLine($"Fee for a underage : ${CalculateFee(new Underage())}");
            Console.WriteLine($"Fee for a Preschooler : ${CalculateFee(new Preschooler())}");
        }
    }
}

// ######### 실행 결과 #########
// Fee for a senior : $200
// Fee for a Adult : $500
// Fee for a underage : $100
// Unhandled exception. System.ArgumentException: Prohibited age: ConsoleApp1.Preschooler (Parameter 'visitor')
// ###########################

// ########### 참조 ###########
// nameof : 식별자의 이름(예: 변수, 메서드, 클래스 이름 등)을 문자열로 반환
// 1. 기본 사용법
// `nameof(식별자)`

/* 예시
* string variableName = nameof(variableName); // "variableName"
* Console.WriteLine(variableName);
*/
// 출력 : variableName
