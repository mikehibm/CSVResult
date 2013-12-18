using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CSVResult.Models;

namespace CSVResult.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// テストデータを画面に表示する。
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var data = Customer.getTestData();
            return View(data);
        }

        /// <summary>
        /// テストデータをCSVファイルとして返す。
        /// </summary>
        /// <returns></returns>
        public ActionResult Csv() {
            var data = Customer.getTestData();
            var columns = Customer.getCSVColumns();
            var titles = Customer.getCSVTitles();
            return new CSVResult(data, columns, titles, "customers.csv");
        }


    }
}
