using System;
using Xunit;

namespace xUnit.DTable
{
    public class DTableTest
    {
        [Fact]
        public void Null_Model_Test()
        {
            Assert.Throws<NullReferenceException>(() => Clariant.DTable.Validation.DTables.GetTables(null));
        }

        [Fact]
        public void Invalid_ConnectionString_Test()
        {
            Assert.Throws<ApplicationException>(() => Clariant.DTable.Validation.DTables.GetTables(new Clariant.DTable.Validation.DtModel()));
        }
    }
}
