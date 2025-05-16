namespace TestProject
{
    public class ClientServiceTest
    {
        double ConvertToFahrenheit(double num)
        {
            return (num * (9.0/5.0)) + 32.00;
        }
        double ConvertToKelvin(double num)
        {
            return num+273.15;
        }
        int CalculateFactorial(int num)
        {
            for (int i = num-1; i > 1; i--)
            {
                num = num *i;
            }
            return num ;
        }
        [Theory(DisplayName ="Should veryfy if strings are polindic ")]
        [InlineData("arara")]
        [InlineData("reviverr")]
        [InlineData("teste")]
        public void Test1(string value)
        {
            var actual=new string(value.Reverse().ToArray());
            Assert.Equal(actual,value);
        }
        List<int> SortList() {
            List<int> numeros = new List<int>() { 5, 3, 2, 1, 6 };
            numeros.Sort();
            return numeros;
        }
        [Fact(DisplayName ="Should veryfy if List are Ordened ")]
        public void ShouldVeryfyIfListAreOrdened( )
        {
              var actual=SortList();

            Assert.Equal([1,2,3,5,6],actual);
        }
        [Fact(DisplayName = "Should veryfy if List is null ")]
        public void ShouldVeryfyIfListIsNull()
        {
            var actual = SortList();

            Assert.NotEmpty(actual);
        }
        [Fact(DisplayName = "Should veryfy if List is in reverse order ")]
        public void ShouldVeryfyIfListAreOrdenedReverse()
        {
            var actual = SortList();
            actual.Reverse();
           
            Assert.Equal([6,5,3,2,1],actual);
        }
        [Fact(DisplayName = "Should convert Celsius to Fahrenheith ")]
        public void ShouldConvertCelciusToFahrenheith()
        {
            var actual = ConvertToFahrenheit(3.00);

            Assert.Equal(37.4, actual);
        }
        [Fact(DisplayName = "Should convert Celsius to Kelvin ")]
        public void ShouldConvertCelciusToKelvin()
        {
            var actual = ConvertToKelvin(3.00);

            Assert.Equal(276.15, actual);
        }
        [Fact(DisplayName = "Should Return number factorial ")]
        public void ShouldReturnNumberFactorial()
        {
            var actual =CalculateFactorial(5);

            Assert.Equal(120,actual);
            
        }

    }
}