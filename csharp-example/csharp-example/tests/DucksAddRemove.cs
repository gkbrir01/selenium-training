using NUnit.Framework;

namespace csharp_example
{
    
    [TestFixture]
    public class DucksAddRemove : TestBase
    {
        int numberDucks = 3;
        [Test]
        public void DucksAddRemoveTest()
        {
            app.choiceAndAddDuckToCartAndCheckCart(numberDucks);
            app.RemoveDuckAndCheck(numberDucks);
        }
    }
}
