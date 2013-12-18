CSVResult
=========

An example of how to return a CSV file in ASP.NET MVC 4. 

The solution is created using Visual Studio 2012. 

CSVResult class can output a list(IEnumerable) of any object in CSV file format very easily. 
It uses reflection mechanism to access all public properties in the target class.

You can specify which properties you want to include by passing an array of property names. 
You can also customize headers for each property. 


How to use
==========
Please copy CSVResult\Controllers\CSVResult.cs into your project in order to use it. 

The escapeString() method defines how to convert data when it include comma, new line or double quotes. Please customize this method according to your needs. 


 
 

CSVResult
=========

ASP.NET MVC 4でCSV出力を行うサンプルです。 

Visual Studio 2012を使用しています。

CSVResultクラスを使うことによって任意のクラスのリスト（IEnumerable）を簡単にCSVフォーマットで出力する事が出来ます。
内部的にはリフレクションを使って対象クラスのパブリックプロパティのリストをループして処理しています。

どのパブリックプロパティを出力対象に含めるかはプロパティ名の配列をパラメータとして渡す事で指定出来ます。
また先頭の見出し行に出力するヘッダーの内容もパラメータで指定する事が可能です。


How to use
==========
CSVResult\Controllers\CSVResult.cs をプロジェクト内にコピーして下さい。

データにカンマ、改行文字、引用符（ダブルクォート）が含まれる場合の挙動は、escapeStringメソッドに定義されています。この部分は要件に合わせて適宜カスタマイズして下さい。


