using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Test.InlineData
{
    public class CultureInlineDataTests : IEnumerable<Object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        { // yield retorna todos os returns até o último
            yield return new object[] {"en"};
            yield return new object[] {"pt-PT"};
            yield return new object[] {"pt-BR"};
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator(); 
        }
    }
}
