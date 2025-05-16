using Sqids;

namespace CommonTestUtilities.IdEncipterBuilder
{
    public class IdEncipterBuilder
    {
        public static SqidsEncoder<long> Build()
        {
            return new SqidsEncoder<long>(new()
            {
                MinLength = 3,
                Alphabet = "17i32546o" // é o mesmo que está no ambiente de teste 
            });
        }
    }
}