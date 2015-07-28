<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Payment.aspx.cs" Inherits="ddac.Payment" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form action="<% = website %>" method="post">

    <input type="hidden" name="cmd" value="_xclick" />
    <input type="hidden" name="business" value="<%= email %>" />

    <input type="hidden" name="item_name" value="<%= item_name %>"/>
    <input type="hidden" name="amount" value="<%= total_price %>"/> 
    <input type="submit" value="Buy!" />

</form>
</body>
</html>
