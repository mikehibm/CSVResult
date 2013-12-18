using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web;
using System.Web.Mvc;
using CSVResult.Models;
using CSVResult.Controllers;

namespace CSVResultTest {

    [TestClass]
    public class CSVResultTest {

        private CSVResult.Controllers.CSVResult prepareCSVResult() {
            var data = Customer.getTestData();
            var columns = Customer.getCSVColumns();
            var titles = Customer.getCSVTitles();
            var csv = new CSVResult.Controllers.CSVResult(data, columns, titles, "");
            return csv;
        }

        [TestMethod]
        public void CSVHeader() {
            var csv = prepareCSVResult();
            var actual = csv.buildCSVHeaderStringFromObject(csv.Data.ElementAt(0));
            var expected = "Eメール, 姓, オーダー日, オーダー金額";

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CSVStringNormal() {

            var csv = prepareCSVResult();
            var actual = csv.buildCSVStringFromObject(csv.Data.ElementAt(0));
            var expected = "aaa@bbbbb.com, Test, 2013/12/01, 123.45";

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CSVStringWithJapanese() {

            var csv = prepareCSVResult();
            var actual = csv.buildCSVStringFromObject(csv.Data.ElementAt(1));
            var expected = "aaa@bbb111.com, てすと, 2013/12/12, 1234.56";

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CSVStringWithComma() {

            var csv = prepareCSVResult();
            var actual = csv.buildCSVStringFromObject(csv.Data.ElementAt(2));
            var expected = "bbbbbbb@bbb22222.com, \"テスト，カンマ入り\", 2013/12/03, 988.10";

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CSVStringWithNewLine() {

            var csv = prepareCSVResult();
            var actual = csv.buildCSVStringFromObject(csv.Data.ElementAt(3));
            var expected = "ccc@bbb333.com, \"テスト|データ\", 2013/11/10, 1256.99";

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CSVStringWithDoubleQuote() {

            var csv = prepareCSVResult();
            var actual = csv.buildCSVStringFromObject(csv.Data.ElementAt(7));
            var expected = "gggg@bbb777.com, \"河合(旧姓 \"\"花田\"\")\", 2013/12/31, 225.22";

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CSVStringWithNullDate() {

            var csv = prepareCSVResult();
            var actual = csv.buildCSVStringFromObject(csv.Data.ElementAt(5));
            var expected = "eeeee@bbb55.com, 本田, , 0.00";

            Assert.AreEqual(expected, actual);
        }


    }
}
