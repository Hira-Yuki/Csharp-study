// 관계 패턴
// Relational Pattern
    
// >, >= , ==, !=, <, <= 와 같은 관계 연산자를 이용하여 입력받은 식을 상수와 비교

static string GetGrade(double score) => score switch
{
  < 60 => "F",
  >= 60 and < 70 => "D",
  >= 70 and < 80 => "C",
  >= 80 and < 90 => "B",
  _ => "A",
};
        
