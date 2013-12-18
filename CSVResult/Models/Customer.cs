using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSVResult.Models {

    /// <summary>
    /// サンプルデータクラス
    /// </summary>
    public class Customer {

        [Key()]
        public int CustId { get; set; }

        [Required]
        public string Email { get; set; }

        public string Lastname { get; set; }

        public string Firstname { get; set; }

        public DateTime? OrderDate { get; set; }

        public decimal OrderAmount { get; set; }




        /// <summary>
        /// CSV出力対象のプロパティを指定するための配列を返す。
        /// </summary>
        /// <returns></returns>
        public static string[] getCSVColumns() {
            var columns = new string[] { "Email", "Lastname", "OrderDate", "OrderAmount" };
            return columns;
        }

        /// <summary>
        /// 見出し用の文字列の配列を返す。
        /// </summary>
        /// <returns></returns>
        public static string[] getCSVTitles() {
            var titles = new string[] { "Eメール", "姓", "オーダー日", "オーダー金額" };
            return titles;
        }


        /// <summary>
        /// テストデータ
        /// </summary>
        /// <returns></returns>
        public static List<Customer> getTestData() {
            List<Customer> data = new List<Customer>() { 
                new Customer(){ CustId = 1,     Email = "aaa@bbbbb.com", Lastname = "Test", Firstname = "Data", OrderDate = new DateTime(2013, 12, 1), OrderAmount= 123.45M } 
                , new Customer(){ CustId = 3,   Email = "aaa@bbb111.com", Lastname = "てすと", Firstname = "でーた21232", OrderDate = new DateTime(2013, 12, 12), OrderAmount= 1234.56M } 
                , new Customer(){ CustId = 4,   Email = "bbbbbbb@bbb22222.com", Lastname = "テスト,カンマ入り", Firstname = "データ", OrderDate = new DateTime(2013, 12, 3), OrderAmount= 988.10M } 
                , new Customer(){ CustId = 7,   Email = "ccc@bbb333.com", Lastname = "テスト\nデータ", Firstname = "でーた", OrderDate = new DateTime(2013, 11, 10), OrderAmount= 1256.99M } 
                , new Customer(){ CustId = 9,   Email = "dddd@bbb444444.com", Lastname = "山田", Firstname = "華子", OrderDate = new DateTime(2012, 5, 10), OrderAmount= 100.00M } 
                , new Customer(){ CustId = 11,  Email = "eeeee@bbb55.com", Lastname = "本田", Firstname = "タロウ", OrderDate = null, OrderAmount= 0.00M } 
                , new Customer(){ CustId = 12,  Email = "ffff@bbb6666.com", Lastname = "鈴木", Firstname = "太郎", OrderDate = new DateTime(2013, 12, 24), OrderAmount= 19.02M } 
                , new Customer(){ CustId = 13,  Email = "gggg@bbb777.com", Lastname = "河合(旧姓 \"花田\")", Firstname = "京子", OrderDate = new DateTime(2013, 12, 31), OrderAmount= 225.22M } 
                , new Customer(){ CustId = 14,  Email = "hhhh@bbb88888.com", Lastname = "山下", Firstname = "一郎", OrderDate = new DateTime(2013, 8, 10), OrderAmount= 336.00M } 
                , new Customer(){ CustId = 15,  Email = "iiiii@bbb99.com", Lastname = "吉田", Firstname = "秀雄", OrderDate = new DateTime(2013, 11, 17), OrderAmount= 9652.50M } 
                , new Customer(){ CustId = 16,  Email = "jjjj@bbb10101.com", Lastname = "金田", Firstname = "勝", OrderDate = new DateTime(2013, 3, 17), OrderAmount= 12345.60M } 
                , new Customer(){ CustId = 19,  Email = "kkkk@ccccc.com", Lastname = "夏木", Firstname = "敬一郎", OrderDate = new DateTime(2013, 9, 28), OrderAmount= 12.34M } 
            };

            return data;
        }

    }
}