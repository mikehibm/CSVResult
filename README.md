CSVResult
=========

An example of how to return a CSV file in ASP.NET MVC 4. 

The solution is created using Visual Studio 2012. 

CSVResult class can output a list(IEnumerable) of any object in CSV file format. 
It uses reflection mechanism to access all public properties in the target class.

You can specify which properties you want to include by passing an array of property names. 
You can also customize headers for each property. 

Please copy CSVResult\Controllers\CSVResult.cs into your project in order to use it. 





CSVResult
=========

ASP.NET MVC 4でCSV出力を行うサンプルです。 

Visual Studio 2012を使用しています。

CSVResultクラスを使うことによって任意のクラスのリスト（IEnumerable）をCSVフォーマットで出力する事が出来ます。
内部的にはリフレクションを使って対象クラスのパブリックプロパティにアクセスする事で実現しています。

どのパブリックプロパティをCSV出力の対象に含めるかはプロパティ名の配列をパラメータとして渡す事で指定出来ます。
また先頭の見出し行に出力するヘッダーの内容もパラメータで指定する事が可能です。

CSVResult\Controllers\CSVResult.cs をコピーしてお使い下さい。


