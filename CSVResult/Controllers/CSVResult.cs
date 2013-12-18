using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;
using System.Text;
using System.Reflection;


namespace CSVResult.Controllers {

    public class CSVResult : ContentResult {

        public IEnumerable<object> Data { get; set; }
        public string Filename { get; set; }
        public string[] Columns { get; set; }
        public string[] Titles { get; set; }

        /// <summary>
        /// 任意のオブジェクトの一覧をCSV形式で出力する。
        /// </summary>
        /// <param name="list">任意の型のオブジェクトのリスト</param>
        /// <param name="columns">出力対象のプロパティ名の配列 (nullの場合は全パブリックプロパティが対象)</param>
        /// <param name="titles">出力対象のプロパティに対応する見出し文字列の配列 (nullの場合はプロパティ名をそのまま出力)</param>
        /// <param name="filename">出力ファイル名</param>
        public CSVResult(IEnumerable<object> data, string[] columns, string[] titles, string filename = null) {
            this.Data = data;
            this.Columns = columns;
            this.Titles = titles;
            this.Filename = filename;
        }

        public override void ExecuteResult(ControllerContext context) {

            if (context == null) {
                throw new ArgumentNullException("context");
            }

            HttpResponseBase response = context.HttpContext.Response;
            response.ClearHeaders();
            response.AppendHeader("Cache-Control", "private, no-cache, no-store, must-revalidate, max-stale=0, post-check=0, pre-check=0");
            response.AppendHeader("Pragma", "no-cache");
            response.AppendHeader("Expires", "-1");
            HttpCachePolicyBase cache = response.Cache;
            cache.SetCacheability(HttpCacheability.NoCache);

            if (string.IsNullOrEmpty(this.Filename)) {
                this.Filename = "data.csv";
            }
            response.ContentType = "application/octet-stream";
            response.AddHeader("Content-Disposition", "attachment;filename=" + this.Filename);

            //文字エンコーディングを指定。
            if (this.ContentEncoding == null) {
                this.ContentEncoding = System.Text.Encoding.GetEncoding("Shift_JIS");
                //this.ContentEncoding = System.Text.Encoding.UTF8;
            }
            response.ContentEncoding = this.ContentEncoding;

            //見出し行を出力。
            outputHeader(response);

            //各行のデータを出力。
            foreach (object item in this.Data) {
                outputLine(response, item);
            }
        }


        private void outputHeader(HttpResponseBase response) {
            if (this.Data.Count() == 0) return;

            var target = this.Data.ElementAt(0);
            string str = BuildCSVHeaderStringFromObject(target);
            str += Environment.NewLine;
            response.BinaryWrite(this.ContentEncoding.GetBytes(str));
        }

        private void outputLine(HttpResponseBase response, object item) {
            string str = BuildCSVStringFromObject(item);
            str += Environment.NewLine;
            response.BinaryWrite(this.ContentEncoding.GetBytes(str));
        }


        /// <summary>
        /// 見出し行の文字列を生成する。
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public string BuildCSVHeaderStringFromObject(object obj) {
            var sOut = new StringBuilder();

            PropertyInfo[] props = obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            int i = 0;
            foreach (var prop in props) {
                string title = prop.Name;

                if (this.Columns != null) {
                    title = null;
                    for (int ix = 0; ix < this.Columns.Length; ix++) {
                        if (string.Equals(this.Columns[ix], prop.Name)) {
                            title = prop.Name;
                            if (this.Titles != null && !string.IsNullOrWhiteSpace(this.Titles[ix])) {
                                title = this.Titles[ix];
                            }
                        }
                    }
                }

                if (title != null) {
                    if (i > 0) sOut.Append(", ");
                    sOut.Append(title);
                    i++;
                }
            }

            return sOut.ToString();
        }

        /// <summary>
        /// 1レコード分のCSVデータを生成する。
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="columns"></param>
        /// <returns></returns>
        public string BuildCSVStringFromObject(object obj) {
            var sOut = new StringBuilder();

            PropertyInfo[] props = obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            int i = 0;
            foreach (var prop in props) {

                if (this.Columns == null || this.Columns.Contains(prop.Name)) {
                    string str = "";

                    if (prop.PropertyType.Equals(typeof(DateTime)) || prop.PropertyType.Equals(typeof(DateTime?))) {
                        str = ConvertDateTimeToString(prop.GetValue(obj));
                    } else if (prop.PropertyType.Equals(typeof(string)) || prop.PropertyType.IsValueType) {
                        str = ConvertObjectToString(prop.GetValue(obj));
                    }

                    str = escapeString(str);

                    if (i++ > 0) sOut.Append(", ");
                    sOut.Append(str);
                }
            }

            return sOut.ToString();
        }

        /// <summary>
        /// CSV出力用にデータをエスケープする。
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        private string escapeString(string s) {

            //=== この部分は要件に合わせてカスタマイズして下さい。 ===
            if (s.IndexOfAny(",\"\r\n".ToCharArray()) >= 0) {
                s = s.Replace("\"", "\"\"");        //ダブルクォートを二重化。
                s = s.Replace(",", "，");           //カンマを全角カンマに変換。
                s = s.Replace("\r\n", "|");         //改行(\r\n)を"|"に変換。
                s = s.Replace("\r", "|");           //改行(\r)を"|"に変換。
                s = s.Replace("\n", "|");           //改行(\n)を"|"に変換。
                s = "\"" + s + "\"";                //全体をダブルクォートで囲む。
            }
            return s;
        }

        /// <summary>
        /// 日付型のオブジェクトを文字列に変換する。
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        private string ConvertDateTimeToString(object v) {
            string s = "";
            if (v == null || DBNull.Value.Equals(v)) return s;

            DateTime d;
            DateTime.TryParse(v.ToString(), out d);
            if (DateTime.MinValue.Equals(d)) return s;

            //=== この部分は要件に合わせてカスタマイズして下さい。 ===
            var format = "yyyy/MM/dd";
            if (d.Hour != 0 || d.Minute != 0 || d.Second != 0) {
                format += " HH:mm:ss";
            }
            s = d.ToString(format);
            return s;
        }

        /// <summary>
        /// 日付型以外のオブジェクトを文字列に変換する。
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private string ConvertObjectToString(Object value) {
            string s = (value == null) ? "" : value.ToString();
            return s.Trim();
        }

    }

}




