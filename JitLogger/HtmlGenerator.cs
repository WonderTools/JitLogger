using System;
using System.Collections.Generic;

namespace Nachiappan.JitLogger
{
    public static class HtmlGenerator
    {
        private static string GetTableRow(string moduleName, Log log)
        {
            var eventName = log.EventId.Name ?? string.Empty;
            var message = log.LogMessage ?? string.Empty;
            var date = log.DateTime.ToString("HH:mm:ss zz");
            return
$@"
                    <tr>
                        <td width=10%>{moduleName}</td>
                        <td width=7%>{log.LogId}</td>
                        <td width=7%>{date}</td>
                        <td width=7%>{log.EventId.Id}</td>
                        <td width=7%>{eventName}</td>
                        <td>{message}</td>
                     </tr>
";
        }


        private static string GetTableRows(string moduleName, List<Log> logs)
        {
            var tableRows = string.Empty;
            foreach (var log in logs)
            {
                var tableRow = GetTableRow(moduleName, log);
                tableRows = tableRows + tableRow;
            }
            return tableRows;
        }

        private static string GetTable(string moduleName, List<Log> logs)
        {
            var tableRows = GetTableRows(moduleName, logs);
            var tableContent =
$@"
<table class=""table-floating-header"">
                <thead>
                    <tr>
                        <th width =10%>Module </th>
                        <th width=7%>Log Id</th>
                        <th width =7%>Date </th> 
                         <th width=7%>EventID </th>
                         <th width=7%>Event Name</th>
                         <th>Message</th>
                     </tr>
                </thead>
                <tbody>
                    {tableRows}
                 </tbody>
             </table>
";
            return tableContent;
        }

        public static string GetHtml(string moduleName, List<Log> logs)
        {
            var tableContent = GetTable(moduleName, logs);
            var html =
                $@"
<!DOCTYPE html>
<html>
    <head>
        <title>{moduleName} - Jit Logger</title>
        <meta name='viewport' content='width=device-width, initial-scale=1'>
        <link rel='stylesheet' href='https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css'>
        <script src='https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js'></script>
        <script src='https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js'></script>
        <style>
            html {{
                height: 100%;
            }}

            body {{
                height: 100%;
                position: relative;
                overflow-y: hidden;
                margin: 0 10px 0 10px;
            }}

            header {{
                position: absolute;
                left: 0;
                right: 0;
                top: 0;
                background-color: skyblue;
                height: 90px;
            }}

            header div {{
                height: 30px; 
                background - color: white
            }}

            footer {{
                position: absolute;
                left: 0;
                right: 0;
                bottom: 0;
                background-color: skyblue;
                height: 20px;
            }}

            .main-content-div {{
                height: 100%;
                overflow-y : scroll;
                padding: 90px 0 30px 0;
            }}

            .table-floating-header {{
                display:block;
            }}

            .table-floating-header thead{{
                display:block;
                position: sticky;
                top:0px;
                background-color: grey;
                width: 100%;
                table-layout: fixed;
            }}

            .table-floating-header thead tr{{
                display: table;
                width: 100%;
                min-height: 30px;
                table-layout: fixed;
                height: 100%;
            }}

            .table-floating-header thead tr th{{
                border:1px solid;
                height: 100%;;
                vertical-align: bottom;
                overflow: hidden;
            }}

            .table-floating-header tbody {{
                display: table;
                width: 100%;
                table-layout: fixed;
            }}



            .table-floating-header tbody tr {{
                display: table;
                width: 100%;
                table-layout: fixed;
            }}

            .table-floating-header tbody tr td {{
                border:1px solid;
                overflow: hidden;
            }}


        </style>
    </head>

    <body>
        <header>
            <h2>MDS - Jit Logger</h2>
            <div>&nbsp;</div>
          </header>
          <div class=""main-content-div"">
            {tableContent}
         </div>
         <footer><p> Powered by Jit Logger</p></footer>
    </body>
</html>
";
            return html;
        }

    }
}