using BlockChain.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BlockChain.Test {
    [TestClass]
    public class Base56DecoderTest {

        [TestMethod]
        public void Address_1GhqLp4fmVH5yMp82rwcXSnsWV2E7kS1jN() {
            Assert.AreEqual("1GhqLp4fmVH5yMp82rwcXSnsWV2E7kS1jN", Base58Encoding.Encode("00AC42E0F55F4F26CB4D350ADD34AE8F15AA95382B8144F4B1".ToByteArray()));
        }

    }
}
